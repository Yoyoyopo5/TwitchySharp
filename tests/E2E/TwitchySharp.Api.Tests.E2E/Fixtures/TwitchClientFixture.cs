using System.Collections.Concurrent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E;

public static class TestServiceProvider
{
    private static readonly Lazy<IServiceProvider> _serviceProvider = new(() => new ServiceCollection()
        .AddHttpClient()
        .AddSingleton<ConcurrentBag<AccessTokenDetails>>()
        .BuildServiceProvider()
    );
    public static IServiceProvider Instance => _serviceProvider.Value;
}

public class TestConfiguration
{
    private static readonly Lazy<IConfiguration> _config = new(() => new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true)
        .AddUserSecrets<TestConfiguration>()
        .AddEnvironmentVariables()
        .Build());

    public static IConfiguration Instance => _config.Value;
}

public sealed class TokenStoreAcquisition(SemaphoreSlim semaphore, ConcurrentDictionary<TwitchIdentity, AccessTokenDetails> store) : IDisposable
{
    public ConcurrentDictionary<TwitchIdentity, AccessTokenDetails> Store { get; } = store;
    private readonly SemaphoreSlim _semaphore = semaphore;
    private int _isDisposed;

    public void Dispose()
    {
        if (Interlocked.Exchange(ref _isDisposed, 1) == 0)
            _semaphore.Release();
    }
}

public class TwitchClientFixture
{
    private static readonly IConfiguration _configuration = TestConfiguration.Instance;
    private static readonly IServiceProvider _serviceProvider = TestServiceProvider.Instance;

    private static readonly SemaphoreSlim _tokenStoreSemaphore = new(1, 1);
    public static async Task<TokenStoreAcquisition> AcquireTokenStore(CancellationToken ct)
    {
        await _tokenStoreSemaphore.WaitAsync(ct);
        return new(_tokenStoreSemaphore, TokenStore);
    }
    public TwitchIdentity.User UserIdentity { get; } = TokenStore.Values
        .OfType<AccessTokenDetails.User>()
        .FirstOrDefault()?
        .Identity ?? throw new InvalidOperationException("A user access token must be configured.");

    private static ConcurrentDictionary<TwitchIdentity, AccessTokenDetails> TokenStore { get; } = _configuration
        .GetRequiredSection("Authorization")
        .GetRequiredSection("UserAccessTokenDetails")
        .Get<UserTokenConfiguration>()?
        .ToAccessTokenDetails() switch
    {
        { } token => new ConcurrentDictionary<TwitchIdentity, AccessTokenDetails>(
            [
                new KeyValuePair<TwitchIdentity, AccessTokenDetails>(token.Identity, token)
            ]),
        _ => []
    };
    private static ValueTask<TDetails?> GetCachedDetails<TDetails>(TwitchRequestAuthorizationContext context, CancellationToken ct = default)
        where TDetails : AccessTokenDetails
        => ValueTask.FromResult(TokenStore.GetValueOrDefault(context.Identity) as TDetails);
    private static ValueTask SetCachedDetails<TDetails>(TDetails details, CancellationToken ct)
        where TDetails : AccessTokenDetails
    {
        TokenStore.AddOrUpdate(details.Identity, details, (_, newDetails) => newDetails);
        return ValueTask.CompletedTask;
    }

    public static ClientConfiguration ClientConfig { get; } = _configuration
        .GetRequiredSection("Authorization")
        .GetRequiredSection("Client")
        .Get<ClientConfiguration>()!;

    public static ExtensionConfiguration ExtensionConfig { get; } = _configuration
        .GetRequiredSection("Authorization")
        .GetRequiredSection("Extension")
        .Get<ExtensionConfiguration>()!;

    public TwitchIdentity.Extension ExtensionIdentity => new(UserIdentity.UserId, ExtensionId: ExtensionConfig.ExtensionId);

    private static readonly ITwitchClient AuthenticationClient = new TwitchClientBuilder() { HttpClient = _serviceProvider.GetRequiredService<HttpClient>() }.Build();

    private static readonly TwitchAuthorizationResolutionOptions authorizationOptions = new TwitchAuthorizationResolutionOptions()
    {
        FallbackClientIdResolver = (_, _) => ValueTask.FromResult<ClientId?>(ClientConfig.ClientId)
    }
        .ConfigureIdentity<TwitchIdentity.User, AccessTokenDetails.User>(new TokenResolutionOptions<AccessTokenDetails.User>()
        {
            AcquireNewToken = null,
            GetCachedToken = GetCachedDetails<AccessTokenDetails.User>,
            RefreshToken = async (expiredToken, ct) => await AuthenticationClient.RefreshUserAccessToken(expiredToken, ClientConfig.ClientSecret, ct),
            OnNewToken = SetCachedDetails
        })
        .ConfigureIdentity<TwitchIdentity.Client, AccessTokenDetails.App>(new TokenResolutionOptions<AccessTokenDetails.App>()
        {
            AcquireNewToken = async (context, ct) => await AuthenticationClient.GetNewAppAccessToken(context.Identity.ClientId, ClientConfig.ClientSecret, ct),
            GetCachedToken = GetCachedDetails<AccessTokenDetails.App>,
            RefreshToken = async (expiredToken, ct) => await AuthenticationClient.GetNewAppAccessToken(expiredToken.Identity.ClientId, ClientConfig.ClientSecret, ct) is { } newToken
                ? new AccessTokenRefreshResult.Refreshed<AccessTokenDetails.App>(newToken)
                : new AccessTokenRefreshResult.Expired<AccessTokenDetails.App>(expiredToken),
            OnNewToken = SetCachedDetails
        })
        .ConfigureIdentity<TwitchIdentity.Extension, AccessTokenDetails.ExtensionJwt>(new TokenResolutionOptions<AccessTokenDetails.ExtensionJwt>()
        {
            // Not sure how we sign these just yet.
            AcquireNewToken = async (context, ct) => context.Identity switch
            {
                TwitchIdentity.Extension identity => await identity.SignNewJwt(ExtensionConfig.Secret),
                _ => null
            },
            GetCachedToken = GetCachedDetails<AccessTokenDetails.ExtensionJwt>,
            RefreshToken = async (expired, ct) => new AccessTokenRefreshResult.Refreshed<AccessTokenDetails.ExtensionJwt>(await expired.Identity.SignNewJwt(ExtensionConfig.Secret)),
            OnNewToken = SetCachedDetails
        });

    private static readonly ITwitchClientBuilder _clientBuilder = new TwitchClientBuilder()
    {
        HttpClient = _serviceProvider.GetRequiredService<HttpClient>()
    }
        .WithAuthorizationResolution(authorizationOptions)
        .WithRateLimiting();
    public static ITwitchClient Client { get; } = _clientBuilder.Build();
}

[CollectionDefinition("twitch")]
public class TwitchClientCollection : ICollectionFixture<TwitchClientFixture> { }
