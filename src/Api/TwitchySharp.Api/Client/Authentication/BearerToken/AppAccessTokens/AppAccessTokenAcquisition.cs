using Microsoft.Extensions.Logging;

namespace TwitchySharp.Api;

internal static class AppAccessTokenAcquisition
{
    // TODO: We need to remove these concrete pipeline steps from the Api project.

    //public static AccessTokenDetailsResolver<AccessTokenDetails.App> CreateNewAppAccessTokenRequester(
    //    ITwitchClient authenticationClient,
    //    ClientSecretResolver clientSecretResolver,
    //    ILoggerFactory? loggerFactory = null
    //    )
    //    => (context, ct) => context.Identity switch
    //    {
    //        TwitchIdentity.Client ci => AcquireToken(
    //            ci,
    //            authenticationClient,
    //            clientSecretResolver,
    //            loggerFactory?.CreateLogger(nameof(AppAccessTokenAcquisition)),
    //            ct),
    //        _ => throw new InvalidOperationException("Cannot acquire an app access token for a non-client identity type.")
    //    };

    //// There is no app access token refresh, we just acquire a new token when old one expires.
    //public static AccessTokenRefresher<AccessTokenDetails.App> CreateNewAppAccessTokenRefresher(
    //    ITwitchClient authenticationClient,
    //    ClientSecretResolver clientSecretResolver,
    //    ILoggerFactory? loggerFactory = null
    //    )
    //    => async (details, ct) => await AcquireToken(
    //        details.Identity,
    //        authenticationClient,
    //        clientSecretResolver,
    //        loggerFactory?.CreateLogger(nameof(AppAccessTokenAcquisition)),
    //        ct
    //        ) switch
    //    {
    //        AccessTokenDetails.App refreshed => new AccessTokenRefreshResult.Refreshed<AccessTokenDetails.App>(refreshed),
    //        _ => new AccessTokenRefreshResult.Expired<AccessTokenDetails.App>(details),
    //    };


    //private static async ValueTask<AccessTokenDetails.App?> AcquireToken(
    //    TwitchIdentity.Client clientIdentity,
    //    ITwitchClient authenticationClient,
    //    ClientSecretResolver clientSecretResolver,
    //    ILogger? logger = null,
    //    CancellationToken ct = default
    //    )
    //{
    //    using IDisposable? loggerScope = logger?.BeginScope("Acquiring new access token for client id: {ClientId}", clientIdentity.ClientId?.Value);

    //    if (await clientSecretResolver(clientIdentity.ClientId, ct) is not ClientSecret secret)
    //    {
    //        logger?.LogWarning("Client secret resolved to null. Returning unavailable token result.");
    //        return null;
    //    }

    //    if (clientIdentity.ClientId is null)
    //    {
    //        logger?.LogWarning("Client id is null. Returning unavailable token result.");
    //        return null;
    //    }

    //    try
    //    {
    //        ClientCredentialsResponse authResponse = (await authenticationClient.SendAsync(new ClientCredentialsRequest()
    //        {
    //            ClientId = clientIdentity.ClientId.Value,
    //            ClientSecret = secret
    //        }, ct)).Content;
    //        logger?.LogInformation("Acquired new app access token.");
    //        return new AccessTokenDetails.App()
    //        {
    //            Identity = clientIdentity,
    //            AccessToken = authResponse.AccessToken,
    //            ExpiresAt = DateTimeOffset.UtcNow + authResponse.ExpiresIn
    //        };
    //    }
    //    catch (TwitchApiException ex)
    //    {
    //        logger?.LogWarning(
    //            ex,
    //            "Twitch Authentication API returned error code {StatusCode}. Returning unavailable token result.",
    //            ex.StatusCode
    //            );
    //        return null;
    //    }
    //}
}
