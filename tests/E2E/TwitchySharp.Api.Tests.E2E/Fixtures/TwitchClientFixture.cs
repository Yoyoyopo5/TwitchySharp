using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using TwitchySharp.Api.Tests.E2E;
using TwitchySharp.Tests.E2E;

[assembly: AssemblyFixture(typeof(TwitchClientFixture))]

namespace TwitchySharp.Api.Tests.E2E;

public class TwitchClientFixture
{
    public IHost ApplicationHost { get; } = ConfigureApplication(Host.CreateApplicationBuilder()).Build();
    private static HostApplicationBuilder ConfigureApplication(HostApplicationBuilder builder)
    {
        builder.Configuration
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<TwitchClientFixture>()
            .AddEnvironmentVariables();

        builder.Services
            .Configure<List<ClientConfiguration>>(builder.Configuration.GetRequiredSection("Clients"))
            .Configure<List<ExtensionConfiguration>>(builder.Configuration.GetRequiredSection("Extensions"))
            .Configure<List<UserConfiguration>>(builder.Configuration.GetRequiredSection("Users"))
            .Configure<List<OrganizationConfiguration>>(builder.Configuration.GetRequiredSection("Organizations"));

        builder.Services
            .AddAccessTokens(sp => sp.GetRequiredService<IOptions<List<UserConfiguration>>>().Value.Select(user
                    => user.ToAccessTokenDetails()))
            .AddSingleton<TwitchRateLimitQueueOptions>();

        builder.Services
            .AddTransient<ResponseRecorder>()
            .AddHttpClient<TwitchClient>()
            .AddHttpMessageHandler<ResponseRecorder>();

        builder.Services
            .AddTransient<ITwitchClient>(sp
                => TwitchClient.CreateDefault(sp.GetRequiredService<IHttpClientFactory>().CreateClient(nameof(TwitchClient)))
                    .With(next => async (request, ct) =>
                    {
                        try
                        {
                            return await next(request, ct);
                        }
                        catch (TwitchApiException apiEx)
                        {
                            TestContext.Current.AddAttachment("twitch-api-exception", apiEx.ToReportString());
                            throw;
                        }
                    })
                    .WithRateLimiting(sp.GetRequiredService<TwitchRateLimitQueueOptions>())
                    .WithAuthentication(new TwitchAuthorizationResolutionOptions()
                        {
                            FallbackClientIdResolver = (ctx, _) => ValueTask.FromResult(ctx.Identity switch
                            {
                                TwitchIdentity.User userIdentity => sp.GetRequiredService<IOptions<List<UserConfiguration>>>().Value.FirstOrDefault(u => u.UserId == userIdentity.UserId)?.Token.ClientId,
                                _ => sp.GetRequiredService<IOptions<List<ClientConfiguration>>>().Value.FirstOrDefault()?.ClientId ?? null
                            })
                        }
                        .AddTokens(sp)
                        )
                );

        return builder;
    }
}

public static class TwitchClientFixtureExtensions
{
    public static T? GetAuthorizingConfigForEndpoint<T>(this TwitchClientFixture fixture, TestName endpointName)
        where T : ITestIdentity
        => fixture.ApplicationHost.Services.GetRequiredService<IOptions<List<T>>>().Value.WithTestName(endpointName);

    public static T GetAuthorizingConfigForTestOrSkip<T>(this TwitchClientFixture fixture, TestName endpointName)
        where T : ITestIdentity
    {
        if (fixture.GetAuthorizingConfigForEndpoint<T>(endpointName) is T config)
            return config;
        Assert.Skip($"No {typeof(T).Name} found for endpoint {endpointName}.");
        return default;
    }

    public static ITwitchClient GetTwitchApiClient(this TwitchClientFixture fixture)
        => fixture.ApplicationHost.Services.GetRequiredService<ITwitchClient>();

    public static ClientConfiguration? GetClientConfig(this TwitchClientFixture fixture, ClientId clientId)
        => fixture.ApplicationHost.Services.GetRequiredService<IOptions<List<ClientConfiguration>>>().Value.FirstOrDefault(c => c.ClientId == clientId);

    public static ClientConfiguration? GetClientConfig(this TwitchClientFixture fixture, UserConfiguration userConfig)
        => fixture.GetClientConfig(userConfig.Token.ClientId);

    public static TokenStore GetTokenStore(this TwitchClientFixture fixture)
        => fixture.ApplicationHost.Services.GetRequiredService<TokenStore>();
}
