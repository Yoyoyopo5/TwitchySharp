using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using TwitchySharp.Api.Tests.Integration;
using TwitchySharp.Infrastructure.Functional;

[assembly:AssemblyFixture(typeof(TwitchApiIntegrationTestFixture))]

namespace TwitchySharp.Api.Tests.Integration;

public class TestEndpointDataSource(IServiceProvider serviceProvider) : EndpointDataSource
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private sealed class DelegateDisposable(Action dispose) : IDisposable
    {
        public void Dispose()
            => dispose();
    }

    private readonly Lock _lock = new();

    private readonly Dictionary<string, Endpoint> _endpoints = [];
    public override IReadOnlyList<Endpoint> Endpoints
    {
        get
        {
            lock (_lock)
                return [.. _endpoints.Values];
        }
    }
    public override IChangeToken GetChangeToken()
    {
        lock (_lock)
            return new CancellationChangeToken(_cts.Token);
    }
    private CancellationTokenSource _cts = new();
    private void NotifyChanged()
    {
        CancellationTokenSource oldCts;
        lock (_lock)
        {
            oldCts = _cts;
            _cts = new CancellationTokenSource();
        }
        oldCts.Cancel();
        oldCts.Dispose();
    }
    public IDisposable Map(HttpMethod method, string pattern, Delegate handler)
    {
        RouteEndpointBuilder endpointBuilder = new(
            RequestDelegateFactory.Create(handler).RequestDelegate,
            RoutePatternFactory.Parse(pattern),
            0)
        {
            ApplicationServices = _serviceProvider
        };
        endpointBuilder.Metadata.Add(new HttpMethodMetadata([method.Method]));
        endpointBuilder.Metadata.Add(new RequireAntiforgeryTokenAttribute(false));

        Endpoint endpoint = endpointBuilder.Build();

        lock (_lock)
        {
            if (!_endpoints.TryAdd(pattern, endpoint))
                throw new InvalidOperationException("Cannot add an endpoint that is already in use.");
        }

        NotifyChanged();

        return new DelegateDisposable(() =>
        {
            bool removed;

            lock (_lock)
            {
                removed = _endpoints.Remove(pattern);
            }

            if (removed)
                NotifyChanged();
        });
    }
}

public class KestrelTwitchApiTestServer
{
    private WebApplication WebApplication { get; }
    private TestEndpointDataSource Endpoints { get; }
    public TwitchClient GetDefaultTwitchClient()
        => WebApplication.Services.GetRequiredService<TwitchClient>();
    public KestrelTwitchApiTestServer()
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder();
        builder.WebHost.ConfigureKestrel(o => o.Listen(System.Net.IPAddress.Loopback, 0));
        builder.Services
            .AddRouting()
            .AddAntiforgery()
            .Configure<JsonOptions>(options =>
            {
                options.SerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.SnakeCaseLower;
                options.SerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            })
            .AddHttpClient()
            .ConfigureHttpClientDefaults(clientBuilder => clientBuilder
            .AddHttpMessageHandler(sp => new ConfigureRequestDelegatingHandler(request =>
            {
                Uri kestrelUri = new(sp.GetRequiredService<IServer>().Features.Get<IServerAddressesFeature>()?.Addresses?.SingleOrDefault() ?? throw new InvalidOperationException("No server address was discovered"));
                request.RequestUri = new UriBuilder()
                {
                    Host = kestrelUri.Host,
                    Port = kestrelUri.Port,
                    Path = request.RequestUri?.AbsolutePath,
                    Query = request.RequestUri?.Query
                }.Uri;
            })))
            .AddTransient<TwitchClient>(sp => new TwitchClient()
                .WithHttpClient(sp.GetRequiredService<HttpClient>())
                .UseMiddleware(new ErrorLoggingMiddleware())
            );

        WebApplication app = builder.Build();
        WebApplication = app;
        Endpoints = new TestEndpointDataSource(app.Services);

        app
            .UseRouting()
            .UseDeveloperExceptionPage()
            .UseAntiforgery()
#pragma warning disable ASP0014
            .UseEndpoints(routeBuilder => routeBuilder.DataSources.Add(Endpoints));
#pragma warning restore ASP0014
    }

    public async Task<KestrelTwitchApiTestServer> StartAsync(CancellationToken ct)
    {
        await WebApplication.StartAsync(ct);
        return this;
    }

    public async Task<KestrelTwitchApiTestServer> StopAsync(CancellationToken ct)
    {
        await WebApplication.StopAsync(ct);
        return this;
    }

    public IDisposable Map(HttpMethod method, string pattern, Delegate handler)
        => Endpoints.Map(method, pattern, handler);

    private class ConfigureRequestDelegatingHandler(Action<HttpRequestMessage> configure) : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken ct
            )
        {
            configure(request);
            return base.SendAsync(request, ct);
        }
    }
}

public sealed class TwitchApiIntegrationTestFixture : IAsyncLifetime
{
    public KestrelTwitchApiTestServer TestServer { get; } = new();
    public async ValueTask DisposeAsync() => await TestServer.StopAsync(default);
    public async ValueTask InitializeAsync() => await TestServer.StartAsync(default);
}

public class ErrorLoggingMiddleware : TwitchClient.Middleware
{
    protected override async ValueTask<TwitchResponse<TResponseContent>> SendAsync<TResponseContent>(
        Func<ITwitchRequestDependencyScope, CancellationToken, ValueTask<TwitchResponse<TResponseContent>>> send,
        ITwitchRequestDependencyScope scope,
        CancellationToken ct)
    {
        TwitchResponse<TResponseContent> response = await send(scope, ct);
        if (response.Error is not null)
        {
            string id = Guid.CreateVersion7().ToString();
            TestContext.Current.AddAttachment($"TwitchClientError-{id}",
                $"""
                Type: {response.Error.GetType()}
                Message: {response.Error.Message}
                """);
        }
        return response;
    }
}
