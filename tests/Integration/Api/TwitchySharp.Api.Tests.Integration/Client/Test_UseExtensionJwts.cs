using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using TwitchySharp.Api.Authentication;
using TwitchySharp.Serialization;
using TwitchySharp.Tests.Unit.Toolkit;

namespace TwitchySharp.Api.Tests.Integration.Client;

public class Test_UseExtensionJwts(TwitchApiIntegrationTestFixture fixture) : IAsyncLifetime
{
    private const string STUB_EXTENSION_AUTHENTICATED_ENDPOINT_PATH = "/extension-authenticated";

    private static readonly ExtensionId _fakeExtensionId = new("12381249");
    private static readonly ExtensionOwnerId _fakeExtensionOwnerId = new(new("1294871359"));
    private static readonly ExtensionSecret _fakeExtensionSecret = new("ZmFrZV9zZWNyZXRfb2ZfYXRfbGVhc3RfMTI4X2J5dGVz");

    private record StubExtensionAuthenticatedRequest : TwitchRequest<BearerToken>,
        IAuthenticatedTwitchRequest<TwitchRequestAuthenticationContext<TwitchIdentity.Extension>>
    {
        public override HttpMethod Method => HttpMethod.Get;
        public override Uri RequestUri => new($"http://127.0.0.1{STUB_EXTENSION_AUTHENTICATED_ENDPOINT_PATH}");

        public TwitchRequestAuthenticationContext<TwitchIdentity.Extension> AuthenticationContext
        {
            get => field ??= new()
            {
                Identity = new(ExtensionId)
            };
            init;
        }

        public required ExtensionId ExtensionId { get; init; }
    }

    private readonly AccumulatingDisposable _endpoints = new();
    public ValueTask InitializeAsync()
    {
        _endpoints.Add(fixture.TestServer.Map(HttpMethod.Get, STUB_EXTENSION_AUTHENTICATED_ENDPOINT_PATH, (HttpContext context) =>
        {
            return context.Request.Headers.Authorization.FirstOrDefault() is not string token
                ? Results.BadRequest("No token was used.")
                : Results.Ok(token.Replace("Bearer ", string.Empty).ToString());
        }));
        return ValueTask.CompletedTask;
    }
    public ValueTask DisposeAsync()
    {
        _endpoints.Dispose();
        return ValueTask.CompletedTask;
    }

    private static ValueTask<IRequestDependencyCache<TwitchIdentity.Extension, AccessTokenDetails.ExtensionJwt>> CreateCache(
        params AccessTokenDetails.ExtensionJwt[] tokens
        )
        => RequestDependencyCache.Create(tokens.Select(token => KeyValuePair.Create(token.Identity, token)));

    private static AccessTokenDetails.ExtensionJwt CreateFakeJwt(
        TwitchIdentity.Extension identity,
        DateTimeOffset expiry
        )
        => new(identity, new ExtensionJwtPayload()
        {
            UserId = _fakeExtensionOwnerId,
            ExpiresAt = expiry
        }.Sign(_fakeExtensionSecret));

    [Fact]
    public async Task SendAsync_NoCachedToken_SignsAndCachesNewJwt()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;

        IRequestDependencyCache<TwitchIdentity.Extension, AccessTokenDetails.ExtensionJwt> cache
            = await CreateCache();

        TwitchClient client = fixture.TestServer.GetDefaultTwitchClient()
            .WithExtension(_fakeExtensionId, _fakeExtensionOwnerId, _fakeExtensionSecret)
            .UseExtensionJwts(options => options with
            {
                JwtCache = cache
            });

        TwitchResponse<BearerToken> response = await client.SendAsync(new StubExtensionAuthenticatedRequest() { ExtensionId = _fakeExtensionId }, ct);

        AccessTokenDetails.ExtensionJwt? cached = await cache.GetOrDefault(new(_fakeExtensionId), ct);

        Assert.NotNull(cached);
        Assert.Equal(cached.BearerToken, response.Content);
    }

    [Fact]
    public async Task SendAsync_CachedToken_UseCachedToken()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;

        AccessTokenDetails.ExtensionJwt fakeTokenDetails = CreateFakeJwt(
            new(_fakeExtensionId),
            DateTimeOffset.MaxValue);

        IRequestDependencyCache<TwitchIdentity.Extension, AccessTokenDetails.ExtensionJwt> cache
            = await CreateCache(fakeTokenDetails);

        TwitchClient client = fixture.TestServer.GetDefaultTwitchClient()
            .WithExtension(_fakeExtensionId, _fakeExtensionOwnerId, _fakeExtensionSecret)
            .UseExtensionJwts(options => options with
            {
                JwtCache = cache
            });

        TwitchResponse<BearerToken> response = await client.SendAsync(new StubExtensionAuthenticatedRequest() { ExtensionId = _fakeExtensionId }, ct);

        Assert.Equal(fakeTokenDetails.BearerToken, response.Content);
    }

    [Fact]
    public async Task SendAsync_InvalidCachedToken_SignsNewJwt()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;

        AccessTokenDetails.ExtensionJwt fakeTokenDetails = CreateFakeJwt(
            new(_fakeExtensionId),
            DateTimeOffset.UnixEpoch + TimeSpan.FromDays(1));

        IRequestDependencyCache<TwitchIdentity.Extension, AccessTokenDetails.ExtensionJwt> cache
            = await CreateCache(fakeTokenDetails);

        TwitchClient client = fixture.TestServer.GetDefaultTwitchClient()
            .WithExtension(_fakeExtensionId, _fakeExtensionOwnerId, _fakeExtensionSecret)
            .UseExtensionJwts(options => options with
            {
                JwtCache = cache,
                GetNow = () => DateTimeOffset.UnixEpoch + TimeSpan.FromDays(2)
            });

        TwitchResponse<BearerToken> response = await client.SendAsync(new StubExtensionAuthenticatedRequest() { ExtensionId = _fakeExtensionId }, ct);

        AccessTokenDetails.ExtensionJwt? cached = await cache.GetOrDefault(new(_fakeExtensionId), ct);

        Assert.NotNull(cached);
        Assert.NotEqual(fakeTokenDetails, cached);
        Assert.Equal(cached.BearerToken, response.Content);
    }

    [Fact]
    public async Task SendAsync_ConcurrentRequests_NoCachedToken_SignsNewJwt_BothRequestsAndCacheUseSameJwt()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;

        IRequestDependencyCache<TwitchIdentity.Extension, AccessTokenDetails.ExtensionJwt> cache
            = await CreateCache();

        TwitchClient client = fixture.TestServer.GetDefaultTwitchClient()
            .WithExtension(_fakeExtensionId, _fakeExtensionOwnerId, _fakeExtensionSecret)
            .UseExtensionJwts(options => options with
            {
                JwtCache = cache
            });

        BearerToken[] results = await Concurrency.RunConcurrently(2, async i =>
            (await client.SendAsync(new StubExtensionAuthenticatedRequest() { ExtensionId = _fakeExtensionId }, ct)).Content, ct);

        AccessTokenDetails.ExtensionJwt? cached = await cache.GetOrDefault(new(_fakeExtensionId), ct);

        Assert.NotNull(cached);
        Assert.Equal(cached.BearerToken, results[0]);
        Assert.Collection(results, results.Select<BearerToken, Action<BearerToken>>(_ => token => Assert.Equal(results.First(), token)).ToArray());
    }

    [Fact]
    public async Task SendAsync_NoCachedTokenAndNoExtensionSecret_ThrowInvalidOperationException()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;

        IRequestDependencyCache<TwitchIdentity.Extension, AccessTokenDetails.ExtensionJwt> cache
            = await CreateCache();

        TwitchClient client = fixture.TestServer.GetDefaultTwitchClient()
            .SetFixed<TwitchClient, ExtensionId?>(_fakeExtensionId)
            .SetFixed<TwitchClient, ExtensionOwnerId?>(_fakeExtensionOwnerId)
            .UseExtensionJwts(options => options with
            {
                JwtCache = cache
            });

        await Assert.ThrowsAsync<InvalidOperationException>(() => client.SendAsync(new StubExtensionAuthenticatedRequest() { ExtensionId = _fakeExtensionId }, ct));
    }

    [Fact]
    public async Task SendAsync_NoCachedTokenAndNoExtensionOwnerId_ThrowInvalidOperationException()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;

        IRequestDependencyCache<TwitchIdentity.Extension, AccessTokenDetails.ExtensionJwt> cache
            = await CreateCache();

        TwitchClient client = fixture.TestServer.GetDefaultTwitchClient()
            .SetFixed<TwitchClient, ExtensionId?>(_fakeExtensionId)
            .SetFixed<TwitchClient, ExtensionSecret?>(_fakeExtensionSecret)
            .UseExtensionJwts(options => options with
            {
                JwtCache = cache
            });

        await Assert.ThrowsAsync<InvalidOperationException>(() => client.SendAsync(new StubExtensionAuthenticatedRequest() { ExtensionId = _fakeExtensionId }, ct));
    }
}
