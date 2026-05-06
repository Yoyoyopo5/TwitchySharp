using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.AuthorizationResolution;
using TwitchySharp.Shared.Models;

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

public class TwitchClientFixture : IDisposable
{
    private static readonly IConfiguration _configuration = TestConfiguration.Instance;
    private static readonly IServiceProvider _serviceProvider = TestServiceProvider.Instance;

    private static readonly SemaphoreSlim _tokenStoreSemaphore = new(1, 1);
    public async Task<TokenStoreAcquisition> AcquireTokenStore(CancellationToken ct)
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

    private readonly static ClientConfiguration _clientConfig = _configuration
        .GetRequiredSection("Authorization")
        .GetRequiredSection("Client")
        .Get<ClientConfiguration>()!;

    private static ValueTask<ClientSecret?> GetClientSecret(ClientId? clientId, CancellationToken ct)
        => ValueTask.FromResult<ClientSecret?>(new ClientSecret(_clientConfig.ClientSecret));
    public Client Client { get; } = _clientConfig.ToClient();

    private readonly static ExtensionConfiguration _extensionConfig = _configuration
        .GetRequiredSection("Authorization")
        .GetRequiredSection("Extension")
        .Get<ExtensionConfiguration>()!;

    public Extension Extension { get; } = _extensionConfig.ToExtension();
    public TwitchIdentity.Extension ExtensionIdentity => new(UserIdentity.UserId, ExtensionId: Extension.Id);

    private static readonly ITwitchClient AuthenticationClient = new TwitchClientBuilder() { HttpClient = _serviceProvider.GetRequiredService<HttpClient>() }.Build();

    private static readonly TwitchAuthorizationResolutionOptions authorizationOptions = new TwitchAuthorizationResolutionOptions()
    {
        FallbackClientIdResolver = (_, _) => ValueTask.FromResult<ClientId?>(new ClientId(_clientConfig.ClientId))
    }
        .ConfigureIdentityTokenResolution(new UserAccessTokenResolutionOptions()
        {
            AuthenticationClient = AuthenticationClient,
            ClientSecretResolver = GetClientSecret,
            GetCachedToken = GetCachedDetails<AccessTokenDetails.User>,
            OnNewToken = SetCachedDetails,
            ResolveFallbackClientId = (_, _) => ValueTask.FromResult<ClientId?>(new ClientId(_clientConfig.ClientId))
        })
        .ConfigureIdentityTokenResolution(new AppAccessTokenResolutionOptions()
        {
            AuthenticationClient = AuthenticationClient,
            ClientSecretResolver = GetClientSecret,
            GetCachedToken = GetCachedDetails<AccessTokenDetails.App>,
            OnNewToken = SetCachedDetails
        })
        .ConfigureIdentityTokenResolution(new ExtensionAccessTokenResolutionOptions()
        {
            // Not sure how we sign these just yet.
            GetCachedToken = GetCachedDetails<AccessTokenDetails.ExtensionJwt>,
            AcquireNewToken = (context, ct) => ValueTask.FromResult(context.Identity switch
            {
                TwitchIdentity.Extension identity => new AccessTokenDetails.ExtensionJwt()
                {
                    Identity = identity,
                    AccessToken = new ExtensionJwtPayload()
                    {
                        UserId = identity.OwnerId,
                        ChannelId = identity.BroadcasterId
                    }.Sign(new(_extensionConfig.Secret))
                },
                _ => null
            }),
            OnNewToken = SetCachedDetails
        });

    private static readonly ITwitchClientBuilder _clientBuilder = new TwitchClientBuilder()
    {
        HttpClient = _serviceProvider.GetRequiredService<HttpClient>()
    }
        .WithAuthorizationResolution(authorizationOptions)
        .WithRateLimiting();

    private static readonly ITwitchClient _client = _clientBuilder.Build();

    public ITwitchClient CreateClient() => _client;

    public void Dispose() { }
}

[CollectionDefinition("twitch")]
public class TwitchClientCollection : ICollectionFixture<TwitchClientFixture> { }
