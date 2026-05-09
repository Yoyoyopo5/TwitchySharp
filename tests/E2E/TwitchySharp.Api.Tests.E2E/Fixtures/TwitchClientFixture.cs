using System.Collections.Concurrent;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwitchySharp.Api.Authorization;

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

public static class ExtensionJwtExtensions
{
    public static ValueTask<AccessTokenDetails.ExtensionJwt> SignNewJwt(
        this TwitchIdentity.Extension identity,
        ExtensionSecret extensionSecret
        )
        => ValueTask.FromResult(new AccessTokenDetails.ExtensionJwt()
        {
            Identity = identity,
            AccessToken = new ExtensionJwtPayload()
            {
                UserId = identity.OwnerId,
                ChannelId = identity.BroadcasterId
            }.Sign(new(extensionSecret))
        });
}

public static class AppAccessTokenExtensions
{
    private static AccessTokenDetails.App ToAppAccessTokenDetails(
        this TwitchResponse<ClientCredentialsResponse> response,
        ClientId clientId
        )
        => new()
        {
            AccessToken = response.Content.AccessToken,
            ExpiresAt = DateTimeOffset.UtcNow + response.Content.ExpiresIn,
            Identity = new(clientId)
        };

    public static async ValueTask<AccessTokenDetails.App?> GetNewAppAccessToken(
        this ITwitchClient client,
        ClientId? clientId,
        ClientSecret clientSecret,
        CancellationToken ct
        )
    {
        if (clientId is not ClientId)
        {
            TestContext.Current.AddWarning("Failed to get an app access token because the request context client id was null.");
            return null;
        }

        try
        {
            return (await client.SendAsync(new ClientCredentialsRequest()
            {
                ClientId = clientId.Value,
                ClientSecret = clientSecret
            }, ct)).ToAppAccessTokenDetails(clientId.Value);
        }
        catch (TwitchApiException ex)
        {
            TestContext.Current.AddWarning($"""
                Failed to acquire a new app access token.
                {ex.StatusCode} response from Twitch:
                {Encoding.UTF8.GetString(ex.Content)}
                """);
            return null;
        }
    }
}

public static class TokenRefreshExtensions
{
    private static AccessTokenRefreshResult.Refreshed<AccessTokenDetails.User> ToRefreshResult(
        this TwitchResponse<AccessTokenRefreshResponse> refreshResponse,
        UserId userId,
        ClientId clientId
        )
        => new(new AccessTokenDetails.User()
        {
            Identity = new TwitchIdentity.User(userId, clientId),
            AccessToken = new UserAccessToken(refreshResponse.Content.AccessToken),
            RefreshToken = new RefreshToken(refreshResponse.Content.RefreshToken),
            Scopes = refreshResponse.Content.Scope?.Select(s => new Scope(s)).ToHashSet() ?? []
        });

    public static async ValueTask<AccessTokenRefreshResult> RefreshUserAccessToken(
        this ITwitchClient client,
        AccessTokenDetails.User tokenDetails,
        ClientSecret clientSecret,
        CancellationToken ct
        )
    {
        if (tokenDetails is not { Identity.ClientId: not null, RefreshToken: not null } validTokenDetails)
        {
            TestContext.Current.AddWarning("Failed to refresh a user access token because the token details are missing required information (client id or refresh token).");
            return new AccessTokenRefreshResult.Expired<AccessTokenDetails.User>(tokenDetails);
        }

        try
        {
            return (await client.SendAsync(new AccessTokenRefreshRequest()
            {
                ClientId = tokenDetails.Identity.ClientId.Value,
                ClientSecret = clientSecret,
                RefreshToken = tokenDetails.RefreshToken.Value
            }, ct)).ToRefreshResult(tokenDetails.Identity.UserId, tokenDetails.Identity.ClientId.Value);
        }
        catch (TwitchApiException ex)
        {
            TestContext.Current.AddWarning($"""
                Failed to refresh a user access token.
                {ex.StatusCode} response from Twitch:
                {Encoding.UTF8.GetString(ex.Content)}
                """);
            return new AccessTokenRefreshResult.Expired<AccessTokenDetails.User>(tokenDetails);
        }
    }
}
