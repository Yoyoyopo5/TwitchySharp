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
            .Configure<ClientConfiguration>(builder.Configuration.GetRequiredSection("Client"))
            .Configure<ExtensionConfiguration>(builder.Configuration.GetRequiredSection("Extension"))
            .Configure<UserConfiguration[]>(builder.Configuration.GetRequiredSection("Users"))
            .AddSingleton<TokenStore>(sp
                => new(sp.GetRequiredService<IOptions<UserConfiguration[]>>().Value.Select(user
                    => user.ToAccessTokenDetails(sp.GetRequiredService<IOptions<ClientConfiguration>>().Value.ClientId))))
            .AddHttpClient()
            .AddAppAccessTokens()
            .AddUserAccessTokens()
            .AddExtensionJwts()
            .AddTransient<ITwitchClient>(sp
                => TwitchClient.CreateDefault(sp.GetRequiredService<HttpClient>())
                    .WithRateLimiting()
                    .WithAuthentication(new TwitchAuthorizationResolutionOptions()
                        {
                            FallbackClientIdResolver = (_, _) => ValueTask.FromResult<ClientId?>(sp.GetRequiredService<IOptions<ClientConfiguration>>().Value.ClientId)
                        }
                        .AddTokens(sp)
                        )
                );

        return builder;
    }
}

public static class TwitchClientFixtureExtensions
{
    public static ClientConfiguration GetClientConfig(this TwitchClientFixture fixture)
        => fixture.ApplicationHost.Services.GetRequiredService<IOptions<ClientConfiguration>>().Value;

    public static UserConfiguration? GetUserConfigFor(this TwitchClientFixture fixture, EndpointName endpoint)
        => fixture.ApplicationHost.Services.GetService<IOptions<UserConfiguration[]>>()?.Value.WithEndpointName(endpoint);

    public static ITwitchClient GetTwitchApiClient(this TwitchClientFixture fixture)
        => fixture.ApplicationHost.Services.GetRequiredService<ITwitchClient>();

    public static TokenStore GetTokenStore(this TwitchClientFixture fixture)
        => fixture.ApplicationHost.Services.GetRequiredService<TokenStore>();
}
