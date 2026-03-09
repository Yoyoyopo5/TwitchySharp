using Microsoft.Extensions.Logging;
using System.Collections.Immutable;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.AuthorizationResolution;

public delegate ValueTask<ClientSecret?> ClientSecretResolver(ClientId? clientId, CancellationToken ct = default);

public delegate ValueTask<AccessTokenRefreshResult> AccessTokenRefresher<TDetails>(TDetails tokenDetails, CancellationToken ct = default);

internal static partial class TokenRefreshing
{ 
    public static AccessTokenRefresher<UserAccessTokenDetails> CreateUserAccessTokenRefresher(
        ClientSecretResolver resolveClientSecret,
        ITwitchClient twitchClient,
        Func<UserAccessTokenDetails, CancellationToken, ValueTask<ClientId?>>? resolveFallbackClientId = null,
        ILoggerFactory? loggerFactory = null
        )
        => (details, ct) => RefreshUserAccessToken(details, resolveClientSecret, twitchClient, resolveFallbackClientId, loggerFactory?.CreateLogger(nameof(RefreshUserAccessToken)), ct);

    private static async ValueTask<AccessTokenRefreshResult> RefreshUserAccessToken(
        UserAccessTokenDetails accessTokenDetails,
        ClientSecretResolver resolveClientSecret,
        ITwitchClient twitchClient,
        Func<UserAccessTokenDetails, CancellationToken, ValueTask<ClientId?>>? resolveFallbackClientId = null,
        ILogger? logger = null,
        CancellationToken ct = default
        )
    {
        using IDisposable? loggerScope = logger?.BeginScope("Refreshing user access token for {UserId}", accessTokenDetails.Identity.UserId);

        if ((accessTokenDetails.Identity.ClientId ?? (resolveFallbackClientId is not null ? await resolveFallbackClientId(accessTokenDetails, ct) : null)) is not ClientId clientId)
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
