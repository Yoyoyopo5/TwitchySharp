using System.Collections.Immutable;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.AuthorizationResolution;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.AuthorizationResolution.Tests.Integration.Fixtures;

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
    public static readonly UserAccessToken AccessToken = new(TestAccessToken);
    public static readonly RefreshToken RefreshToken = new(TestRefreshToken);

    public ITwitchClient CreateTestClient()
    {

    }

    private record TestClient : ITwitchClient
    {
        public required TwitchRequestHandler RequestHandler { get; init; }
        public ValueTask<TwitchResponse> SendAsync(TwitchRequest request, CancellationToken ct = default)
            => RequestHandler(request, ct);

        public ValueTask<TwitchResponse<TResponseContent>> SendAsync<TResponseContent>(TwitchRequest<TResponseContent> request, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }

    public record TestClientBuilder : ITwitchClientBuilder
    {
        private readonly MiddlewarePipelineBuilder<TwitchRequestHandler> _handlerBuilder = new();
        public ITwitchClientBuilder Use(Func<TwitchRequestHandler, TwitchRequestHandler> func)
        {
            _handlerBuilder.Use(func);
            return this;
        }
        public ITwitchClient Build()
            => new TestClient { RequestHandler = _handlerBuilder.Finally(CreateTerminalHandler(HttpClient)) };
    }
}
