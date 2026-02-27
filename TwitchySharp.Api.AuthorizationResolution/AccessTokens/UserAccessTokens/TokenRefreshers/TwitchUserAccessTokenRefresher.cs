using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Immutable;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.AuthorizationResolution;

/// <summary>
/// An <see cref="IRefreshAccessToken{TDetails}"/> implementation that uses an <see cref="ITwitchClient"/>
/// to refresh user access tokens by sending <see cref="AccessTokenRefreshRequest"/>s.
/// </summary>
/// <param name="TwitchClient">The Twitch client to use for making refresh requests.</param>
/// <param name="ClientSecretResolver">The resolver to use for obtaining the client secret needed to refresh the access token.</param>
public record TwitchUserAccessTokenRefresher(
    ITwitchClient TwitchClient,
    IResolveClientSecret ClientSecretResolver,
    ILogger<TwitchUserAccessTokenRefresher>? Logger = null
    ) : IRefreshAccessToken<UserAccessTokenDetails>
{
    private readonly ILogger<TwitchUserAccessTokenRefresher> _logger = Logger ?? NullLogger<TwitchUserAccessTokenRefresher>.Instance;

    /// <summary>
    /// Configure a fallback <see cref="ClientId"/> to be used if the user access token's associated <see cref="UserIdentity"/> client id is <see langword="null"/>.
    /// </summary>
    public ClientId? FallbackClientId { get; init; }

    public async ValueTask<AccessTokenRefreshResult> Refresh(AccessTokenDetailsResolutionResult.Expired<UserAccessTokenDetails> token, CancellationToken ct = default)
    {
        using IDisposable? _loggerScope = _logger.BeginScope("Refreshing user access token for {UserId}", token.AccessTokenDetails.Identity.UserId);

        if ((token.AccessTokenDetails.Identity.ClientId ?? FallbackClientId) is not ClientId clientId)
        {
            _logger.LogWarning("The token was unable to be refreshed becuase the identity of the token to refresh must have a non-null ClientId or the {FallbackProperty} must be set.", nameof(FallbackClientId));
            return new AccessTokenRefreshResult.Expired<UserAccessTokenDetails>(token.AccessTokenDetails);
        }

        if (token.AccessTokenDetails.RefreshToken is not RefreshToken refreshToken)
        {
            _logger.LogWarning("The token was unable to be refreshed becuase the user access token does not have an associated refresh token.");
            return new AccessTokenRefreshResult.Expired<UserAccessTokenDetails>(token.AccessTokenDetails);
        }

        if (await ClientSecretResolver.GetClientSecret(token.AccessTokenDetails.Identity.ClientId, ct) is not ClientSecret secret)
        {
            _logger.LogWarning("The token was unable to be refreshed becuase a client secret could not be resolved for {ClientId}.", token.AccessTokenDetails.Identity.ClientId);
            return new AccessTokenRefreshResult.Expired<UserAccessTokenDetails>(token.AccessTokenDetails);
        }

        AccessTokenRefreshRequest request = new()
        {
            ClientId = clientId,
            ClientSecret = secret,
            RefreshToken = refreshToken
        };

        try
        {
            ITwitchResponse<AccessTokenRefreshResponse> response = await TwitchClient.SendAsync(request, ct);
            _logger.LogInformation($"Successfully refreshed user access token.");
            return new AccessTokenRefreshResult.Refreshed<UserAccessTokenDetails>(token.AccessTokenDetails with
            {
                AccessToken = response.Content.AccessToken,
                RefreshToken = response.Content.RefreshToken,
                ExpiresAt = DateTimeOffset.UtcNow + response.Content.ExpiresIn,
                Scopes = response.Content.Scope?.ToImmutableHashSet() ?? []
            });
        }
        catch(TwitchApiException apiException)
        {
            _logger.LogWarning(apiException, "The token was unable to be refreshed because the API returned an HTTP error code {ErrorCode}.", apiException.StatusCode);
            return new AccessTokenRefreshResult.Expired<UserAccessTokenDetails>(token.AccessTokenDetails);
        }
    }
}

public delegate ValueTask<ClientSecret?> ClientSecretResolver(ClientId? clientId, CancellationToken ct = default);

public delegate ValueTask<AccessTokenRefreshResult> AccessTokenRefresher<TDetails>(TDetails tokenDetails, CancellationToken ct = default);

internal static partial class TokenRefreshing
{
    public static Func<AccessTokenRefresher<TDetails>, AccessTokenRefresher<TDetails>> UseOnlyExpiredTokens<TDetails>()
        where TDetails : IAccessTokenDetails
        => next => async (details, ct) => details switch
        {
            _ when details.ExpiresAt > DateTimeOffset.UtcNow => new AccessTokenRefreshResult.Valid<TDetails>(details),
            _ => await next(details, ct)
        };

    public static AccessTokenRefresher<UserAccessTokenDetails> CreateUserAccessTokenRefresher<TKey>(
        ClientSecretResolver resolveClientSecret,
        ITwitchClient twitchClient,
        Func<UserAccessTokenDetails, ValueTask<ClientId?>>? resolveFallbackClientId = null,
        ILogger? logger = null
        )
        => (details, ct) => RefreshUserAccessToken(details, resolveClientSecret, twitchClient, resolveFallbackClientId, logger, ct);

    private static async ValueTask<AccessTokenRefreshResult> RefreshUserAccessToken(
        UserAccessTokenDetails accessTokenDetails,
        ClientSecretResolver resolveClientSecret,
        ITwitchClient twitchClient,
        Func<UserAccessTokenDetails, ValueTask<ClientId?>>? resolveFallbackClientId = null,
        ILogger? logger = null,
        CancellationToken ct = default
        )
    {
        using IDisposable? loggerScope = logger?.BeginScope("Refreshing user access token for {UserId}", accessTokenDetails.Identity.UserId);

        if ((accessTokenDetails.Identity.ClientId ?? (resolveFallbackClientId is not null ? await resolveFallbackClientId(accessTokenDetails) : null)) is not ClientId clientId)
        {
            logger?.LogWarning("The token was unable to be refreshed becuase the identity of the token to refresh must have a non-null ClientId or a fallback must be configured.");
            return new AccessTokenRefreshResult.Expired<UserAccessTokenDetails>(accessTokenDetails);
        }

        if (accessTokenDetails.RefreshToken is not RefreshToken refreshToken)
        {
            logger?.LogWarning("The token was unable to be refreshed becuase the user access token does not have an associated refresh token.");
            return new AccessTokenRefreshResult.Expired<UserAccessTokenDetails>(accessTokenDetails);
        }

        if (await resolveClientSecret(accessTokenDetails.Identity.ClientId, ct) is not ClientSecret secret)
        {
            logger?.LogWarning("The token was unable to be refreshed becuase a client secret could not be resolved for {ClientId}.", token.AccessTokenDetails.Identity.ClientId);
            return new AccessTokenRefreshResult.Expired<UserAccessTokenDetails>(accessTokenDetails);
        }

        AccessTokenRefreshRequest request = new()
        {
            ClientId = clientId,
            ClientSecret = secret,
            RefreshToken = refreshToken
        };

        try
        {
            ITwitchResponse<AccessTokenRefreshResponse> response = await twitchClient.SendAsync(request, ct);
            logger?.LogInformation($"Successfully refreshed user access token.");
            return new AccessTokenRefreshResult.Refreshed<UserAccessTokenDetails>(accessTokenDetails with
            {
                AccessToken = response.Content.AccessToken,
                RefreshToken = response.Content.RefreshToken,
                ExpiresAt = DateTimeOffset.UtcNow + response.Content.ExpiresIn,
                Scopes = response.Content.Scope?.ToImmutableHashSet() ?? []
            });
        }
        catch (TwitchApiException apiException)
        {
            logger?.LogWarning(apiException, "The token was unable to be refreshed because the API returned an HTTP error code {ErrorCode}.", apiException.StatusCode);
            return new AccessTokenRefreshResult.Expired<UserAccessTokenDetails>(accessTokenDetails);
        }
    }
}