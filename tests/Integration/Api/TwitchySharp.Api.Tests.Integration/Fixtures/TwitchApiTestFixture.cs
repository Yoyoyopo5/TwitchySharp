using System.Collections.Immutable;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Tests.Integration.Controllers;

namespace TwitchySharp.Api.Tests.Integration.Fixtures;

public class TwitchApiTestFixture
{

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

    private readonly static IWebHost Host = ConfigureWebHost(new WebHostBuilder()).Start();
    private readonly static HttpClient Client = Host.GetTestClient();

    protected static IWebHostBuilder ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseTestServer();

        builder.ConfigureServices(services =>
        {
            services.AddScoped(sp => new HelixControllerOptions()
            {
                ValidClientId = TestClientId,
                ValidBearerToken = new(TEST_ACCESS_TOKEN),
                RateLimitDetails = new()
                {
                    Limit = 1000,
                    Remaining = 999,
                    Reset = DateTimeOffset.UtcNow + TimeSpan.FromSeconds(2)
                }
            });
            services.AddScoped(sp => new AuthorizationControllerOptions()
            {
                ValidClientId = TestClientId,
                ValidClientSecret = TestClientSecret,
                ValidAuthorizationCode = TEST_AUTHORIZATION_CODE,
                ValidRedirectUri = TestRedirectUri,
                ValidRefreshToken = new(TEST_REFRESH_TOKEN)
            });
            services.AddControllers()
                .AddApplicationPart(typeof(MockAuthorizationController).Assembly);
        });

        builder.Configure(app =>
        {
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        });

        return builder;
    }

    public TwitchAuthorizationResolutionOptions HelixAuthorizationOptions { get; } = new TwitchAuthorizationResolutionOptions()
    {
        FallbackClientIdResolver = (_, ct) => ValueTask.FromResult<ClientId?>(TestClientId)
    }.ConfigureIdentity<TwitchIdentity.Client, AccessTokenDetails.App>(new()
    {
        GetCachedToken = (ctx, ct) => ValueTask.FromResult(TestTokens.WhereTokenMeetsRequirements<AccessTokenDetails.App>(ctx).FirstOrDefault())
    }).ConfigureIdentity<TwitchIdentity.User, AccessTokenDetails.User>(new()
    {
        GetCachedToken = (ctx, ct) => ValueTask.FromResult(TestTokens.WhereTokenMeetsRequirements<AccessTokenDetails.User>(ctx).FirstOrDefault())
    });

    public ITwitchClient CreateTwitchClient()
        => TwitchClient.CreateDefault(Client)
            .WithAuthentication(HelixAuthorizationOptions)
            .WithRateLimiting()
            .With(next => async (request, ct) =>
            {
                try
                {
                    return await next(request, ct);
                }
                catch (TwitchApiException ex)
                {
                    TestContext.Current.AddAttachment("ApiExceptionStatusCode", ex.StatusCode.ToString());
                    TestContext.Current.AddAttachment("ApiExceptionContent", Encoding.UTF8.GetString(ex.Content));
                    throw;
                }
            });
}
