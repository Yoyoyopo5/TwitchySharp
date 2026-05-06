using System.Collections.Immutable;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.AuthorizationResolution;
using TwitchySharp.Api.Tests.Integration.Controllers;
using TwitchySharp.Api.Tests.Integration.Models;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.Integration.Fixtures;

/// <summary>
/// Stub program class for WebApplicationFactory.
/// </summary>
public class Program { }

/// <summary>
/// Test fixture providing a mock Twitch API server for integration tests.
/// </summary>
public class TwitchApiTestFixture : WebApplicationFactory<Program>
{
    /// <summary>
    /// Allows tests to configure mock responses.
    /// </summary>
    public MockResponseConfigurator ResponseConfig { get; } = new();

    // Test constants
    public const string TEST_CLIENT_ID = "test_client_id";
    public const string TEST_CLIENT_SECRET = "test_client_secret";
    public const string TEST_ACCESS_TOKEN = "test_access_token";
    public const string TEST_REFRESH_TOKEN = "test_refresh_token";
    public const string TEST_AUTHORIZATION_CODE = "test_auth_code";
    public const string TEST_REDIRECT_URI = "http://localhost:3000";
    public const string TEST_USER_ID = "test_user_id";

    public static ClientId TestClientId { get; } = new(TEST_CLIENT_ID);
    public static ClientSecret TestClientSecret { get; } = new(TEST_CLIENT_SECRET);
    public static AppAccessToken TestAppAccessToken { get; } = new(TEST_ACCESS_TOKEN);
    public static UserAccessToken TestUserAccessToken { get; } = new(TEST_ACCESS_TOKEN);
    public static RedirectUri TestRedirectUri { get; } = new(TEST_REDIRECT_URI);
    public static UserId TestUserId { get; } = new(TEST_USER_ID);

    public static TwitchIdentity.Client TestClientIdentity { get; } = new(TestClientId);
    public static TwitchIdentity.User TestUserIdentity { get; } = new(TestUserId, TestClientId);

    private static IEnumerable<AccessTokenDetails> TestTokens { get; } =
    [
        new AccessTokenDetails.App()
        {
            AccessToken = TestAppAccessToken,
            ExpiresAt = DateTimeOffset.MaxValue,
            Identity = TestClientIdentity
        },
        new AccessTokenDetails.User()
        {
            AccessToken = TestUserAccessToken,
            ExpiresAt = DateTimeOffset.MaxValue,
            Identity = TestUserIdentity,
            Scopes = ImmutableHashSet.Create(Scope.ChannelManageVips, Scope.ModeratorManageWarnings)
        }
    ];

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddSingleton(ResponseConfig);
            services.AddControllers()
                .AddApplicationPart(typeof(MockAuthorizationController).Assembly);
        });

        builder.Configure(app =>
        {
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        });
    }

    protected override IHostBuilder? CreateHostBuilder() =>
        Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseTestServer());

    public ITwitchClientBuilder CreateTwitchClientBuilder()
    {
        ITwitchClient authClient = new TwitchClientBuilder() { HttpClient = CreateClient() }.Build();
        return new TwitchClientBuilder()
        {
            HttpClient = CreateClient()
        }.WithAuthorizationResolution(
            new TwitchAuthorizationResolutionOptions()
            {
                FallbackClientIdResolver = (_, _) => ValueTask.FromResult<ClientId?>(TestClientId)
            }
            .ConfigureIdentityTokenResolution(new AppAccessTokenResolutionOptions()
            {
                AuthenticationClient = authClient,
                ClientSecretResolver = (_, _) => ValueTask.FromResult<ClientSecret?>(TestClientSecret),
                GetCachedToken = (context, _) => ValueTask.FromResult(TestTokens.WhereTokenMeetsRequirements<AccessTokenDetails.App>(context).FirstOrDefault())
            })
            .ConfigureIdentityTokenResolution(new UserAccessTokenResolutionOptions()
            {
                AuthenticationClient = authClient,
                ClientSecretResolver = (_, _) => ValueTask.FromResult<ClientSecret?>(TestClientSecret),
                GetCachedToken = (context, _) => ValueTask.FromResult(TestTokens.WhereTokenMeetsRequirements<AccessTokenDetails.User>(context).FirstOrDefault()),
                ResolveFallbackClientId = (_, _) => ValueTask.FromResult<ClientId?>(TestClientId)
            })
            );
    }

    /// <summary>
    /// Creates an HttpClient configured to use the test server directly.
    /// </summary>
    /// <returns>An HttpClient that sends requests to the mock server.</returns>
    public new HttpClient CreateClient() =>
        CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
}
