using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using TwitchySharp.Api;
using TwitchySharp.Api.Authentication;
using Xunit;

namespace TwitchySharp.Tests.E2E;

public class TwitchTestApplication
{
    public IHost ApplicationHost { get; }

    public TwitchTestApplication()
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder();
        ApplicationHost = ConfigureDefaultTwitchApplication(ConfigureTwitchApplication(builder)).Build();
    }

    private static HostApplicationBuilder ConfigureDefaultTwitchApplication(HostApplicationBuilder builder)
    {
        builder.Configuration
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables();

        builder.Services
            .Configure<List<ClientConfiguration>>(builder.Configuration.GetRequiredSection("Clients"))
            .Configure<List<ExtensionConfiguration>>(builder.Configuration.GetRequiredSection("Extensions"))
            .Configure<List<UserConfiguration>>(builder.Configuration.GetRequiredSection("Users"))
            .Configure<List<OrganizationConfiguration>>(builder.Configuration.GetRequiredSection("Organizations"));

        builder.Services
            .AddSingleton(sp => Options.Create<List<ITestIdentity<TwitchIdentity>>>([
                .. sp.GetService<IOptions<List<ClientConfiguration>>>()?.Value ?? [],
                .. sp.GetService<IOptions<List<ExtensionConfiguration>>>()?.Value ?? [],
                .. sp.GetService<IOptions<List<UserConfiguration>>>()?.Value ?? [],
                .. sp.GetService<IOptions<List<OrganizationConfiguration>>>()?.Value ?? []
                ]
                ))
            .AddSingleton<TokenStore>(sp => new TokenStore(sp.GetRequiredService<IOptions<List<UserConfiguration>>>().Value.Select(user
                    => user.ToAccessTokenDetails())))
            .AddSingleton<TwitchRateLimitQueueOptions>();

        builder.Services
            .AddTransient(sp => new TwitchClient()
                    .WithHttpClient(sp.GetRequiredService<HttpClient>())
                    .AddClientConfiguration(sp)
                    .AddExtensionConfiguration(sp)
                    .UseAppAccessTokens(sp.GetService<TokenStore>())
                    .UseUserAccessTokens(sp.GetRequiredService<TokenStore>())
                    .UseExtensionJwts(sp.GetRequiredService<TokenStore>()))
            .AddTransient<TestingTwitchClient>(sp => new(sp.GetRequiredService<TwitchClient>()))
            .AddTransient<ITwitchClient>(sp => sp.GetRequiredService<TwitchClient>());

        return builder;
    }

    protected virtual HostApplicationBuilder ConfigureTwitchApplication(HostApplicationBuilder builder)
        => builder;
}

public record TestingTwitchClient(TwitchClient Client)
{
    public async Task<TwitchResponse<TResponseContent>> SendAsync<TResponseContent>(TwitchRequest<TResponseContent> request, TestName testName, CancellationToken ct)
    {
        try
        {
            return await Client.SetResolver<TestName>((scope, _) => ValueTask.FromResult(new RequestDependencyResult<TestName>(testName, scope))).SendAsync(request, ct);
        }
        catch (TwitchApiException ex)
        {
            TestContext.Current.AddAttachment(nameof(TwitchApiException), ex.ToReportString());
            throw;
        }
        catch (Exception ex)
        {
            TestContext.Current.AddAttachment(ex.GetType().Name, ex.Message);
            throw;
        }
    }
}

public static class TwitchTestApplicationExtensions
{
    public static T? GetAuthorizingConfigForEndpoint<T>(this IServiceProvider sp, TestName testName)
        where T : ITestIdentity<TwitchIdentity>
        => sp.GetRequiredService<IOptions<List<T>>>().Value.WithTestName(testName);

    public static T? GetAuthorizingConfigForEndpoint<T>(this TwitchTestApplication fixture, TestName endpointName)
        where T : ITestIdentity<TwitchIdentity>
        => fixture.ApplicationHost.Services.GetAuthorizingConfigForEndpoint<T>(endpointName);

    public static T GetAuthorizingConfigForTestOrSkip<T>(this TwitchTestApplication fixture, TestName endpointName)
        where T : ITestIdentity<TwitchIdentity>
    {
        if (fixture.GetAuthorizingConfigForEndpoint<T>(endpointName) is T config)
            return config;
        Assert.Skip($"No {typeof(T).Name} found for endpoint {endpointName}.");
        return default;
    }

    public static ITwitchClient GetTwitchApiClient(this TwitchTestApplication fixture)
        => fixture.ApplicationHost.Services.GetRequiredService<ITwitchClient>();

    public static TConfig? GetConfig<TConfig>(this IServiceProvider sp, Func<TConfig, bool> predicate)
        where TConfig : class, ITestIdentity<TwitchIdentity>
        => sp.GetService<IOptions<List<TConfig>>>()?.Value.FirstOrDefault(predicate);

    public static ClientConfiguration? GetClientConfig(this IServiceProvider sp, ClientId clientId)
        => sp.GetConfig<ClientConfiguration>(config => config.ClientId == clientId);

    public static ClientConfiguration? GetClientConfig(this TwitchTestApplication fixture, ClientId clientId)
        => fixture.ApplicationHost.Services.GetClientConfig(clientId);

    public static ClientConfiguration? GetClientConfig(this TwitchTestApplication fixture, UserConfiguration userConfig)
        => fixture.GetClientConfig(userConfig.Token.ClientId);

    public static TokenStore GetTokenStore(this TwitchTestApplication fixture)
        => fixture.ApplicationHost.Services.GetRequiredService<TokenStore>();
}
