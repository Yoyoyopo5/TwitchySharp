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
    private sealed class ChangeToken : IChangeToken
    {
        private readonly HashSet<(object?, Action<object?>)> _callbacks = [];
        public bool ActiveChangeCallbacks => true;
        public bool HasChanged { get; private set; }
        public IDisposable RegisterChangeCallback(Action<object?> callback, object? state)
        {
            (object?, Action<object?>) registrant = (state, callback);
            _callbacks.Add(registrant);
            return new DelegateDisposable(() => _callbacks.Remove(registrant));
        }
        public void NotifyChanged()
        {
            HasChanged = true;
            foreach ((object? state, Action<object?> callback) in _callbacks)
            {
                callback(state);
            }
        }
    }

    private readonly Dictionary<string, Endpoint> _endpoints = [];
    public override IReadOnlyList<Endpoint> Endpoints => [.. _endpoints.Values];
    private readonly ChangeToken _changeToken = new();
    public override IChangeToken GetChangeToken() => _changeToken;

    private readonly SemaphoreSlim _sempahore = new(1, 1);
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

        _sempahore.Wait();
        try
        {
            if (!_endpoints.TryAdd(pattern, endpoint))
                throw new InvalidOperationException("Cannot add an endpoint that is already in use.");

            _changeToken.NotifyChanged();
            return new DelegateDisposable(() =>
            {
                _sempahore.Wait();
                try
                {
                    _endpoints.Remove(pattern, out _);
                    _changeToken.NotifyChanged();
                }
                finally
                {
                    _sempahore.Release();
                }
            });
        }
        finally
        {
            _sempahore.Release();
        }
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
                .Configure<TwitchClient, HttpResponseMessage?>(next => (scope, ct) =>
                    next(scope, ct).MatchAsync<HttpResponseMessage?, Validation<HttpResponseMessage?>>(
                        e =>
                        {
                            if (e is ExceptionError { Exception: TwitchApiException ex })
                            {
                                TestContext.Current.AddAttachment("ApiExceptionStatusCode", ex.StatusCode.ToString());
                                TestContext.Current.AddAttachment("ApiExceptionContent", ex.Content);
                            }
                            return e;
                        },
                        v => v
                        ))
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
