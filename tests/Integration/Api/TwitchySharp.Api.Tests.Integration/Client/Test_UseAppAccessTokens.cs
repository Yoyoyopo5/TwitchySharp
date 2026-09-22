using System.Net;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwitchySharp.Api.Authentication;
using TwitchySharp.Tests.Unit.Toolkit;

namespace TwitchySharp.Api.Tests.Integration.Client;

public class Test_UseAppAccessTokens(TwitchApiIntegrationTestFixture fixture) : IAsyncLifetime
{
    private const string FAKE_APP_AUTHENTICATED_ENDPOINT_PATH = "/stub-app-authenticated";
    private const string FAKE_UNAUTHENTICATED_ENDPOINT_PATH = "/stub-unauthenticated";
    private readonly ClientId _fakeClientId = new("12345");
    private readonly ClientSecret _fakeClientSecret = new("678910");

    private record StubAppAccessTokenRequest : TwitchRequest<BearerToken>,
         IAuthenticatedTwitchRequest<TwitchRequestAuthenticationContext<TwitchIdentity.Client>>
    {
        public override HttpMethod Method => HttpMethod.Get;
        public override Uri RequestUri => new($"http://localhost{FAKE_APP_AUTHENTICATED_ENDPOINT_PATH}");
        public TwitchRequestAuthenticationContext<TwitchIdentity.Client> AuthenticationContext
            => TwitchRequestAuthenticationContext.Default;
    }

    private class ClientCredentialsRequestFormData
    {
        [DataMember(Name = "client_id")]
        public string ClientId { get; set; } = string.Empty;
        [DataMember(Name = "client_secret")]
        public string ClientSecret { get; set; } = string.Empty;
        [DataMember(Name = "grant_type")]
        public string GrantType { get; set; } = string.Empty;
    }

    private readonly AccumulatingDisposable _endpoints = new();
    public ValueTask InitializeAsync()
    {
        _endpoints
        .Add(fixture.TestServer.Map(HttpMethod.Get, FAKE_UNAUTHENTICATED_ENDPOINT_PATH, (HttpContext ctx) =>
        {
            return ctx.Request.Headers.Authorization.Count != 0
                ? Results.BadRequest("A token was used.")
                : Results.Ok();
        }))
        .Add(fixture.TestServer.Map(HttpMethod.Get, FAKE_APP_AUTHENTICATED_ENDPOINT_PATH, (HttpContext ctx) =>
        {
            return ctx.Request.Headers.Authorization.FirstOrDefault() is not string token
                ? Results.BadRequest("No token was used.")
                : Results.Ok(token.Replace("Bearer ", string.Empty).ToString());
        }))
        .Add(fixture.TestServer.Map(HttpMethod.Post, "/oauth2/token", ([FromForm] ClientCredentialsRequestFormData formData) =>
        {
            return formData.ClientId != _fakeClientId.Value
                ? Results.BadRequest("ClientId did not match.")
                : formData.ClientSecret != _fakeClientSecret.Value
                ? Results.BadRequest("ClientSecret did not match.")
                : formData.GrantType != "client_credentials"
                ? Results.BadRequest("Grant type was not \"client_crendentials\"")
                : Results.Ok(new ClientCredentialsResponseContent()
                {
                    AccessToken = new(Guid.NewGuid().ToString()),
                    ExpiresIn = TimeSpan.FromDays(1),
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

    [Fact]
    public async Task SendAsync_RequestUsingAppAccessTokens_NoCachedToken_AcquiresNewTokenAndCachesToken()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;

        InMemoryConcurrentCache<ClientId, AccessTokenDetails.App> cache = new();

        TwitchClient client = fixture.TestServer.GetDefaultTwitchClient()
            .UseAppAccessTokens(options => options with { TokenCache = cache })
            .WithClient(_fakeClientId, _fakeClientSecret);

        TwitchResponse<BearerToken> response = await client.SendAsync(new StubAppAccessTokenRequest(), ct);

        AccessTokenDetails.App? cached = await cache.GetOrDefault(_fakeClientId, ct);

        Assert.NotNull(cached);
        Assert.Equal(cached.BearerToken, response.Content);
    }

    [Fact]
    public async Task SendAsync_RequestUsingAppAccessTokens_CachedValidToken_DoesNotAcquireNewTokenAndUsesCachedToken()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        AppAccessToken fakeAccessToken = new("219471709");
        DateTimeOffset fakeExpiry = DateTime.MinValue + TimeSpan.FromDays(2);
        DateTimeOffset fakeNow = DateTime.MinValue + TimeSpan.FromDays(1);
        Assert.True(fakeExpiry > fakeNow);

        IRequestDependencyCache<ClientId, AccessTokenDetails.App> cache = await new InMemoryConcurrentCache<ClientId, AccessTokenDetails.App>()
            .Set(_fakeClientId, new()
            {
                AccessToken = fakeAccessToken,
                ExpiresAt = fakeExpiry,
                Identity = new(_fakeClientId)
            }, ct);

        TwitchClient client = fixture.TestServer.GetDefaultTwitchClient()
            .UseAppAccessTokens(options => options with
            {
                TokenCache = cache,
                GetNow = () => fakeNow
            })
            .WithClient(_fakeClientId, _fakeClientSecret);

        TwitchResponse<BearerToken> response = await client.SendAsync(new StubAppAccessTokenRequest(), ct);

        Assert.Equal(fakeAccessToken.Value, response.Content.Value);
    }

    [Fact]
    public async Task SendAsync_RequestUsingAppAccessTokens_CachedInvalidToken_AcquiresNewTokenAndCachesNewToken()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        AppAccessToken fakeExpiredAccessToken = new("219471709");

        IRequestDependencyCache<ClientId, AccessTokenDetails.App> cache = await new InMemoryConcurrentCache<ClientId, AccessTokenDetails.App>()
            .Set(_fakeClientId, new()
            {
                AccessToken = fakeExpiredAccessToken,
                ExpiresAt = DateTime.MinValue + TimeSpan.FromDays(1),
                Identity = new(_fakeClientId)
            }, ct);

        TwitchClient client = fixture.TestServer.GetDefaultTwitchClient()
            .UseAppAccessTokens(options => options with
            {
                TokenCache = cache,
                GetNow = () => DateTime.MinValue + TimeSpan.FromDays(2)
            })
            .WithClient(_fakeClientId, _fakeClientSecret);

        TwitchResponse<BearerToken> response = await client.SendAsync(new StubAppAccessTokenRequest(), ct);

        AccessTokenDetails.App? cached = await cache.GetOrDefault(_fakeClientId, ct);

        Assert.NotNull(cached);
        Assert.NotEqual(fakeExpiredAccessToken.Value, response.Content.Value);
        Assert.NotEqual(fakeExpiredAccessToken, cached.AccessToken);
        Assert.Equal(cached.BearerToken, response.Content);
    }

    [Fact]
    public async Task SendAsync_RequestNotUsingAppAccessTokens_DoesNotAcquireNewToken()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;

        TwitchClient client = fixture.TestServer.GetDefaultTwitchClient()
            .UseAppAccessTokens()
            .WithClient(_fakeClientId, _fakeClientSecret);

        TwitchResponse<object> response = await client.SendAsync(new StubTwitchRequest(FAKE_UNAUTHENTICATED_ENDPOINT_PATH), ct);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task SendAsync_ConcurrentRequests_NoCachedToken_AcquiresNewTokenOnceAndBothRequestUseNewToken()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;

        InMemoryConcurrentCache<ClientId, AccessTokenDetails.App> cache = new();

        TwitchClient client = fixture.TestServer.GetDefaultTwitchClient()
            .UseAppAccessTokens(options => options with { TokenCache = cache })
            .WithClient(_fakeClientId, _fakeClientSecret);

        BearerToken[] tokens = await Concurrency.RunConcurrently(2, async i =>
        {
            TwitchResponse<BearerToken> response = await client.SendAsync(new StubAppAccessTokenRequest(), ct);
            return response.Content;
        }, ct);

        AccessTokenDetails.App? cached = await cache.GetOrDefault(_fakeClientId, ct);

        Assert.NotNull(cached);
        Assert.Equal(cached.BearerToken, tokens[0]);
        Assert.Collection(tokens, tokens.Select<BearerToken, Action<BearerToken>>(_ => token => Assert.Equal(tokens.First(), token)).ToArray());
    }
}
