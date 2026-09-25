using System.Net;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using TwitchySharp.Api.Authentication;
using TwitchySharp.Infrastructure.Functional;
using TwitchySharp.Tests.Unit.Toolkit;

namespace TwitchySharp.Api.Tests.Integration.Client;

public class Test_UseUserAccessTokens(TwitchApiIntegrationTestFixture fixture)
    : IAsyncLifetime
{
    private const string STUB_USER_AUTHENTICATED_ENDPOINT_PATH = "/stub-user-authenticated";

    private static readonly ClientId _fakeClientId = new("124374");
    private static readonly ClientSecret _fakeClientSecret = new("278932947");
    private static readonly RefreshToken _fakeRefreshToken = new("24419238047");
    private static readonly TimeSpan _newTokensExpireIn = TimeSpan.FromDays(1);
    private static readonly Scope[] _tokenScopes = [Scope.AnalyticsReadExtensions];

    private record StubUserAuthenticatedRequest : TwitchRequest<BearerToken>,
        IAuthenticatedTwitchRequest<UserWithScopesAuthenticationContext>
    {
        public override HttpMethod Method => HttpMethod.Get;
        public override Uri RequestUri => new($"http://127.0.0.1{STUB_USER_AUTHENTICATED_ENDPOINT_PATH}");

        public UserWithScopesAuthenticationContext AuthenticationContext
        {
            get => field ??= new()
            {
                Identity = new TwitchIdentity.User(UserId),
                ValidScopes = _tokenScopes.ToHashSet()
            };
            init;
        }

        public required UserId UserId { get; init; }
    }

    private class UserAccessTokenRefreshFormData
    {
        [DataMember(Name = "client_id")]
        public string ClientId { get; set; } = string.Empty;
        [DataMember(Name = "client_secret")]
        public string ClientSecret { get; set; } = string.Empty;
        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; } = string.Empty;
        [DataMember(Name = "grant_type")]
        public string GrantType { get; set; } = string.Empty;
    }

    private readonly AccumulatingDisposable _endpoints = new();
    public ValueTask InitializeAsync()
    {
        _endpoints.Add(fixture.TestServer.Map(HttpMethod.Get, STUB_USER_AUTHENTICATED_ENDPOINT_PATH, (HttpContext ctx) =>
            {
                return !ctx.Request.Headers.TryGetValue("Client-Id", out StringValues clientId)
                    ? Results.BadRequest("Client-Id header was not set.")
                    : ctx.Request.Headers.Authorization.FirstOrDefault() is not string authorization
                    ? Results.BadRequest("Authorization header was not set.")
                    : Results.Ok(authorization.Replace("Bearer ", string.Empty));
            }))
            .Add(fixture.TestServer.Map(HttpMethod.Post, "/user-access-tokens/oauth2/token", ([FromForm] UserAccessTokenRefreshFormData formData) =>
            {
                return formData.GrantType != "refresh_token"
                    ? Results.BadRequest("Grant type must be \"refresh_token\"")
                    : formData.ClientId != _fakeClientId.Value
                    ? Results.BadRequest("Invalid client_id")
                    : formData.ClientSecret != _fakeClientSecret.Value
                    ? Results.BadRequest("Invalid client_secret")
                    : Results.Ok(new AccessTokenRefreshResponseContent()
                    {
                        AccessToken = new(Guid.CreateVersion7().ToString()),
                        ExpiresIn = _newTokensExpireIn,
                        Scope = _tokenScopes,
                        RefreshToken = _fakeRefreshToken,
                        TokenType = "bearer"
                    });
            }));

        return ValueTask.CompletedTask;
    }
    public ValueTask DisposeAsync()
    {
        _endpoints.Dispose();
        return ValueTask.CompletedTask;
    }

    private TwitchClient CreateTestClient()
        => fixture.TestServer.GetDefaultTwitchClient()
            .When(scope => scope.Request is AccessTokenRefreshRequest)
            .Configure<RequestDependencyConditionalConfiguration<TwitchClient>, HttpRequestMessage?>(next => (scope, ct) =>
            {
                return next(scope, ct).MapAsync(requestMessage =>
                {
                    if (requestMessage is not { RequestUri: not null })
                        return requestMessage;

                    requestMessage.RequestUri = new UriBuilder(requestMessage.RequestUri)
                    {
                        Path = "/user-access-tokens/oauth2/token"
                    }.Uri;
                    return requestMessage;
                });
            })
            .EndWhen();

    private static ValueTask<IRequestDependencyCache<TwitchIdentity.User, AccessTokenDetails.User>> CreateCache(
        params AccessTokenDetails.User[] tokens
        )
        => tokens.Aggregate(
            ValueTask.FromResult<IRequestDependencyCache<TwitchIdentity.User, AccessTokenDetails.User>>(new InMemoryConcurrentCache<TwitchIdentity.User, AccessTokenDetails.User>()),
            async (current, next) => await (await current).Set(
                next.Identity,
                next,
                TestContext.Current.CancellationToken
                ));

    [Fact]
    public async Task SendAsync_WithNoCachedToken_ReturnsBadRequest()
    {
        IRequestDependencyCache<TwitchIdentity.User, AccessTokenDetails.User> cache
            = await CreateCache();

        TwitchClient client = CreateTestClient()
            .WithClient(_fakeClientId, _fakeClientSecret)
            .UseUserAccessTokens(cache);

        StubUserAuthenticatedRequest request = new() { UserId = new("12345") };

        TwitchApiException exception = await Assert.ThrowsAsync<TwitchApiException>(async () => await client.SendAsync(request, TestContext.Current.CancellationToken));
        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
    }

    [Fact]
    public async Task SendAsync_WithValidCachedToken_ReturnsCachedToken()
    {
        UserId fakeUserId = new("1249017848");
        UserAccessToken expectedAccessToken = new("489128149");

        AccessTokenDetails.User validToken = new()
        {
            Identity = new(fakeUserId, _fakeClientId),
            AccessToken = expectedAccessToken,
            ExpiresAt = DateTimeOffset.MaxValue,
            Scopes = _tokenScopes.ToHashSet(),
            RefreshToken = _fakeRefreshToken
        };

        IRequestDependencyCache<TwitchIdentity.User, AccessTokenDetails.User> cache
            = await CreateCache(validToken);

        TwitchClient client = CreateTestClient()
            .WithClient(_fakeClientId, _fakeClientSecret)
            .UseUserAccessTokens(cache);

        StubUserAuthenticatedRequest request = new() { UserId = fakeUserId };

        TwitchResponse<BearerToken> response = await client.SendAsync(request, TestContext.Current.CancellationToken);

        Assert.Equal(expectedAccessToken, response.Content);
    }

    [Fact]
    public async Task SendAsync_WithInvalidCachedToken_RefreshesToken_CachesRefreshedTokenAndReturnsRefreshedToken()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;

        UserId fakeUserId = new("1249017848");
        UserAccessToken expiredAccessToken = new("489128149");
        TwitchIdentity.User userIdentity = new(fakeUserId, _fakeClientId);

        AccessTokenDetails.User validToken = new()
        {
            Identity = userIdentity,
            AccessToken = expiredAccessToken,
            ExpiresAt = DateTimeOffset.MinValue,
            Scopes = _tokenScopes.ToHashSet(),
            RefreshToken = _fakeRefreshToken
        };

        IRequestDependencyCache<TwitchIdentity.User, AccessTokenDetails.User> cache
            = await CreateCache(validToken);

        TwitchClient client = CreateTestClient()
            .WithClient(_fakeClientId, _fakeClientSecret)
            .UseUserAccessTokens(cache, options => options with
            {
                GetNow = () => DateTimeOffset.MinValue + TimeSpan.FromDays(1)
            });

        StubUserAuthenticatedRequest request = new() { UserId = fakeUserId };

        TwitchResponse<BearerToken> response = await client.SendAsync(request, TestContext.Current.CancellationToken);

        Assert.NotEqual(expiredAccessToken, response.Content);

        AccessTokenDetails.User? cached = await cache.GetOrDefault(userIdentity, ct);

        Assert.NotNull(cached);
        Assert.Equal(response.Content, cached.BearerToken);
    }

    [Fact]
    public async Task SendAsyncConcurrent_WithInvalidCachedToken_RefreshesTokenOnce_BothRequestsUseRefreshedToken()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;

        UserId fakeUserId = new("1249017848");
        UserAccessToken expiredAccessToken = new("489128149");
        TwitchIdentity.User userIdentity = new(fakeUserId, _fakeClientId);

        AccessTokenDetails.User validToken = new()
        {
            Identity = userIdentity,
            AccessToken = expiredAccessToken,
            ExpiresAt = DateTimeOffset.MinValue,
            Scopes = _tokenScopes.ToHashSet(),
            RefreshToken = _fakeRefreshToken
        };

        IRequestDependencyCache<TwitchIdentity.User, AccessTokenDetails.User> cache
            = await CreateCache(validToken);

        TwitchClient client = CreateTestClient()
            .WithClient(_fakeClientId, _fakeClientSecret)
            .UseUserAccessTokens(cache, options => options with
            {
                GetNow = () => DateTimeOffset.MinValue + TimeSpan.FromDays(1)
            });

        StubUserAuthenticatedRequest request = new() { UserId = fakeUserId };

        BearerToken[] results = await Concurrency.RunConcurrently(2, async i =>
            (await client.SendAsync(request, ct)).Content, ct);

        AccessTokenDetails.User? cached = await cache.GetOrDefault(userIdentity, ct);

        Assert.NotNull(cached);
        Assert.Equal(cached.BearerToken, results[0]);
        Assert.Collection(results, results.Select<BearerToken, Action<BearerToken>>(_ => token => Assert.Equal(results.First(), token)).ToArray());
    }

    [Fact]
    public async Task SendAsync_WithInvalidCachedTokenWithNoRefreshToken_ThrowsInvalidOperationException()
    {
        UserId fakeUserId = new("1249017848");
        UserAccessToken expiredAccessToken = new("489128149");
        TwitchIdentity.User userIdentity = new(fakeUserId, _fakeClientId);

        AccessTokenDetails.User validToken = new()
        {
            Identity = userIdentity,
            AccessToken = expiredAccessToken,
            ExpiresAt = DateTimeOffset.MinValue,
            Scopes = _tokenScopes.ToHashSet(),
            RefreshToken = null
        };

        IRequestDependencyCache<TwitchIdentity.User, AccessTokenDetails.User> cache
            = await CreateCache(validToken);

        TwitchClient client = CreateTestClient()
            .WithClient(_fakeClientId, _fakeClientSecret)
            .UseUserAccessTokens(cache, options => options with
            {
                GetNow = () => DateTimeOffset.MinValue + TimeSpan.FromDays(1)
            });

        StubUserAuthenticatedRequest request = new() { UserId = fakeUserId };

        await Assert.ThrowsAsync<InvalidOperationException>(() => client.SendAsync(request, TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task SendAsync_WithCachedToken_DifferentIdentity_DoesNotUseCachedToken()
    {
        TwitchIdentity.User tokenUserIdentity = new(new("1249017848"), _fakeClientId);
        TwitchIdentity.User requestUserIdentity = new(new("w34189724"), _fakeClientId);

        AccessTokenDetails.User validToken = new()
        {
            Identity = tokenUserIdentity,
            AccessToken = new("219478127"),
            ExpiresAt = DateTimeOffset.MaxValue,
            Scopes = _tokenScopes.ToHashSet(),
            RefreshToken = null
        };

        IRequestDependencyCache<TwitchIdentity.User, AccessTokenDetails.User> cache
            = await CreateCache(validToken);

        TwitchClient client = CreateTestClient()
            .WithClient(_fakeClientId, _fakeClientSecret)
            .UseUserAccessTokens(cache);

        StubUserAuthenticatedRequest request = new() { UserId = requestUserIdentity.UserId };

        TwitchApiException ex = await Assert.ThrowsAsync<TwitchApiException>(() => client.SendAsync(request, TestContext.Current.CancellationToken));
        Assert.Equal(HttpStatusCode.BadRequest, ex.StatusCode);
    }
}
