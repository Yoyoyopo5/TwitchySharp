using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.AuthorizationResolution.Tests.Integration;

/// <summary>
/// Test fixture providing test data and helpers for authorization resolution integration tests.
/// </summary>
public class TokenResolutionTestFixture
{
    // Test constants
    public const string TestClientId = "test_client_id";
    public const string TestClientSecret = "test_client_secret";
    public const string TestAccessToken = "test_access_token";
    public const string TestRefreshToken = "test_refresh_token";
    public const string TestUserId = "123456789";
    public const string TestNewAccessToken = "new_access_token";
    public const string TestNewRefreshToken = "new_refresh_token";

    public static readonly ClientId ClientId = new(TestClientId);
    public static readonly ClientSecret ClientSecret = new(TestClientSecret);
    public static readonly TwitchIdentity.Client ClientIdentity = new(ClientId);
    public static readonly TwitchIdentity.User TestUserIdentity = new(new UserId(TestUserId)) { ClientId = ClientId };
    public static readonly UserAccessToken UserAccessToken = new(TestAccessToken);
    public static readonly RefreshToken RefreshToken = new(TestRefreshToken);
    public static readonly AppAccessToken AppAccessToken = new(TestAccessToken);
    public static readonly TimeSpan AccessTokenExpiry = TimeSpan.FromSeconds(60);
    public static readonly TwitchRateLimitDetails RateLimitDetails = new()
    {
        Limit = 100,
        Remaining = 99,
        Reset = DateTimeOffset.UtcNow + TimeSpan.FromSeconds(5)
    };

    private readonly TwitchRequestHandler AuthenticationHandler = (context, ct) => ValueTask.FromResult<TwitchResponse>(context.Request switch
    {
        ClientCredentialsRequest appTokenRequest => new TwitchResponse<ClientCredentialsResponse>()
        {
            Content = new()
            {
                AccessToken = AppAccessToken,
                ExpiresIn = AccessTokenExpiry,
                TokenType = "bearer"
            },
            StatusCode = System.Net.HttpStatusCode.OK,
            Request = appTokenRequest,
            RateLimitDetails = RateLimitDetails
        },
        AccessTokenRefreshRequest refreshRequest => new TwitchResponse<AccessTokenRefreshResponse>()
        {
            Content = new()
            {
                AccessToken = UserAccessToken,
                Scope = [],
                ExpiresIn = AccessTokenExpiry,
                RefreshToken = RefreshToken,
                TokenType = "bearer"
            },
            StatusCode = System.Net.HttpStatusCode.OK,
            Request = refreshRequest,
            RateLimitDetails = RateLimitDetails
        },
        _ => throw new NotSupportedException("Request type not configured.")
    });
    public ITwitchClient CreateTestAuthenticationClient()
        => new TestClientBuilder(AuthenticationHandler).Build();

    private readonly TwitchRequestHandler TestHandler = (context, ct) => ValueTask.FromResult<TwitchResponse>(new TwitchResponse<TestTwitchResponseData>()
    {
        Request = context.Request,
        StatusCode = System.Net.HttpStatusCode.OK,
        Content = new() { RequestAuthorizationHeaders = context.AuthorizationHeaders }
    });
    public ITwitchClient CreateTestClient(TwitchAuthorizationResolutionOptions options)
        => new TestClientBuilder(TestHandler).UseAuthorizationResolution(options).Build();

    private record TestClient : ITwitchClient
    {
        public required TwitchRequestHandler RequestHandler { get; init; }
        public ValueTask<TwitchResponse> SendAsync(TwitchRequest request, CancellationToken ct = default)
            => RequestHandler(request, ct);

        public async ValueTask<TwitchResponse<TResponseContent>> SendAsync<TResponseContent>(TwitchRequest<TResponseContent> request, CancellationToken ct = default)
            => (TwitchResponse<TResponseContent>)await RequestHandler(request, ct);
    }

    private record TestClientBuilder(TwitchRequestHandler TerminalHandler) : ITwitchClientBuilder
    {
        private readonly MiddlewarePipelineBuilder<TwitchRequestHandler> _handlerBuilder = new();
        public ITwitchClientBuilder Use(Func<TwitchRequestHandler, TwitchRequestHandler> func)
        {
            _handlerBuilder.Use(func);
            return this;
        }
        public ITwitchClient Build()
            => new TestClient
            {
                RequestHandler = _handlerBuilder.Finally(TerminalHandler)
            };
    }
}
