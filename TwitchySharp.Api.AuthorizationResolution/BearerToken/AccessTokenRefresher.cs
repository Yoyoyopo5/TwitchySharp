namespace TwitchySharp.Api.AuthorizationResolution;

internal delegate ValueTask<AccessTokenRefreshResult> AccessTokenRefresher<TDetails>(
    TDetails tokenDetails, CancellationToken ct = default)
    where TDetails : AccessTokenDetails;