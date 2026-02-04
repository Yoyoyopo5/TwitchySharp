using System.Net;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.AuthorizationResolution;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.Unit.Authorization;

public class Test_DefaultUserAccessTokenRefresher
{
    private static readonly ClientId TestClientId = new("test_client_id");
    private static readonly ClientSecret TestClientSecret = new("test_client_secret");
    private static readonly ClientIdentity TestClientIdentity = new(TestClientId);
    private static readonly RefreshToken TestRefreshToken = new("test_refresh_token");
    private static readonly UserAccessToken NewAccessToken = new("new_access_token");
    private static readonly RefreshToken NewRefreshToken = new("new_refresh_token");

    [Fact]
    public async Task RefreshUserAccessToken_ValidRequest_ReturnsRefreshResponse()
    {
        // Arrange
        var twitchClient = new MockTwitchClient(CreateSuccessResponse());
        var secretResolver = new MockSecretResolver(TestClientSecret);
        var refresher = new DefaultUserAccessTokenRefresher(twitchClient, secretResolver);

        // Act
        var result = await refresher.RefreshUserAccessToken(TestClientIdentity, TestRefreshToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(NewAccessToken.Value, result.AccessToken.Value);
        Assert.Equal(NewRefreshToken.Value, result.RefreshToken.Value);
    }

    [Fact]
    public async Task RefreshUserAccessToken_SecretNotFound_ThrowsInvalidOperationException()
    {
        // Arrange
        var twitchClient = new MockTwitchClient(CreateSuccessResponse());
        var secretResolver = new MockSecretResolver(null); // No secret configured
        var refresher = new DefaultUserAccessTokenRefresher(twitchClient, secretResolver);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            refresher.RefreshUserAccessToken(TestClientIdentity, TestRefreshToken).AsTask());

        Assert.Contains("Unable to resolve client secret", exception.Message);
        Assert.Contains(TestClientId.Value, exception.Message);
    }

    [Fact]
    public async Task RefreshUserAccessToken_ResolvesCorrectSecret()
    {
        // Arrange
        var twitchClient = new MockTwitchClient(CreateSuccessResponse());
        var secretResolver = new MockTrackingSecretResolver(TestClientSecret);
        var refresher = new DefaultUserAccessTokenRefresher(twitchClient, secretResolver);

        // Act
        await refresher.RefreshUserAccessToken(TestClientIdentity, TestRefreshToken);

        // Assert
        Assert.True(secretResolver.WasCalled);
        Assert.Equal(TestClientId, secretResolver.RequestedClientId);
    }

    [Fact]
    public async Task RefreshUserAccessToken_ConstructsCorrectRequest()
    {
        // Arrange
        var twitchClient = new MockTrackingTwitchClient(CreateSuccessResponse());
        var secretResolver = new MockSecretResolver(TestClientSecret);
        var refresher = new DefaultUserAccessTokenRefresher(twitchClient, secretResolver);

        // Act
        await refresher.RefreshUserAccessToken(TestClientIdentity, TestRefreshToken);

        // Assert
        Assert.NotNull(twitchClient.ReceivedRequest);
        var request = Assert.IsType<AccessTokenRefreshRequest>(twitchClient.ReceivedRequest);
        Assert.Equal(TestClientId.Value, request.ClientId.Value);
        Assert.Equal(TestClientSecret.Value, request.ClientSecret.Value);
        Assert.Equal(TestRefreshToken.Value, request.RefreshToken.Value);
    }

    [Fact]
    public async Task RefreshUserAccessToken_PropagatesCancellationToken()
    {
        // Arrange
        var twitchClient = new MockTrackingTwitchClient(CreateSuccessResponse());
        var secretResolver = new MockSecretResolver(TestClientSecret);
        var refresher = new DefaultUserAccessTokenRefresher(twitchClient, secretResolver);
        using var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;

        // Act
        await refresher.RefreshUserAccessToken(TestClientIdentity, TestRefreshToken, cancellationToken);

        // Assert
        Assert.Equal(cancellationToken, twitchClient.ReceivedCancellationToken);
    }

    [Fact]
    public async Task RefreshUserAccessToken_TwitchClientThrows_PropagatesException()
    {
        // Arrange
        var mockRequest = new MockTwitchRequest();
        var expectedException = new TwitchApiException("Token revoked")
        {
            Request = mockRequest,
            StatusCode = HttpStatusCode.BadRequest,
            Headers = new Dictionary<string, IEnumerable<string>>(),
            ContentHeaders = new Dictionary<string, IEnumerable<string>>(),
            Content = []
        };
        var twitchClient = new MockThrowingTwitchClient(expectedException);
        var secretResolver = new MockSecretResolver(TestClientSecret);
        var refresher = new DefaultUserAccessTokenRefresher(twitchClient, secretResolver);

        // Act & Assert
        var actualException = await Assert.ThrowsAsync<TwitchApiException>(() =>
            refresher.RefreshUserAccessToken(TestClientIdentity, TestRefreshToken).AsTask());

        Assert.Same(expectedException, actualException);
    }

    private record MockTwitchRequest() : ITwitchRequest
    {
        public HttpRequestMessage ToHttpRequestMessage(System.Text.Json.JsonSerializerOptions serializerOptions) => new();
    }

    #region Helper Methods

    private static AccessTokenRefreshResponse CreateSuccessResponse()
    {
        return new AccessTokenRefreshResponse
        {
            AccessToken = NewAccessToken,
            RefreshToken = NewRefreshToken,
            ExpiresIn = TimeSpan.FromHours(4),
            TokenType = "bearer",
            Scope = [Scope.ChannelModerate]
        };
    }

    #endregion

    #region Mock Types

    private class MockSecretResolver(ClientSecret? secret) : IResolveClientSecret
    {
        public ValueTask<ClientSecret?> GetClientSecret(ClientId clientId, CancellationToken ct = default)
            => ValueTask.FromResult(secret);
    }

    private class MockTrackingSecretResolver(ClientSecret? secret) : IResolveClientSecret
    {
        public bool WasCalled { get; private set; }
        public ClientId? RequestedClientId { get; private set; }

        public ValueTask<ClientSecret?> GetClientSecret(ClientId clientId, CancellationToken ct = default)
        {
            WasCalled = true;
            RequestedClientId = clientId;
            return ValueTask.FromResult(secret);
        }
    }

    private class MockTwitchClient(AccessTokenRefreshResponse response) : ITwitchClient
    {
        public ValueTask<ITwitchResponse> SendAsync(ITwitchRequest request, CancellationToken ct = default)
        {
            throw new NotImplementedException("Untyped SendAsync not needed for this test");
        }

        public ValueTask<ITwitchResponse<TResponseContent>> SendAsync<TResponseContent>(
            ITwitchRequest<TResponseContent> request,
            CancellationToken ct = default)
        {
            var twitchResponse = new MockTwitchResponse<TResponseContent>(request, (TResponseContent)(object)response);
            return ValueTask.FromResult<ITwitchResponse<TResponseContent>>(twitchResponse);
        }
    }

    private class MockTrackingTwitchClient(AccessTokenRefreshResponse response) : ITwitchClient
    {
        public ITwitchRequest? ReceivedRequest { get; private set; }
        public CancellationToken ReceivedCancellationToken { get; private set; }

        public ValueTask<ITwitchResponse> SendAsync(ITwitchRequest request, CancellationToken ct = default)
        {
            throw new NotImplementedException("Untyped SendAsync not needed for this test");
        }

        public ValueTask<ITwitchResponse<TResponseContent>> SendAsync<TResponseContent>(
            ITwitchRequest<TResponseContent> request,
            CancellationToken ct = default)
        {
            ReceivedRequest = request;
            ReceivedCancellationToken = ct;
            var twitchResponse = new MockTwitchResponse<TResponseContent>(request, (TResponseContent)(object)response);
            return ValueTask.FromResult<ITwitchResponse<TResponseContent>>(twitchResponse);
        }
    }

    private class MockThrowingTwitchClient(Exception exception) : ITwitchClient
    {
        public ValueTask<ITwitchResponse> SendAsync(ITwitchRequest request, CancellationToken ct = default)
        {
            throw exception;
        }

        public ValueTask<ITwitchResponse<TResponseContent>> SendAsync<TResponseContent>(
            ITwitchRequest<TResponseContent> request,
            CancellationToken ct = default)
        {
            throw exception;
        }
    }

    private class MockTwitchResponse<TResponseContent>(ITwitchRequest request, TResponseContent content) : ITwitchResponse<TResponseContent>
    {
        public TResponseContent Content => content;
        public ITwitchRequest Request => request;
        public HttpStatusCode StatusCode => HttpStatusCode.OK;
        public TwitchRateLimitDetails? RateLimitDetails => null;
    }

    #endregion
}
