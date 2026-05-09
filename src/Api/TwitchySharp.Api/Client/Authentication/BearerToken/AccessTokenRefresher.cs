namespace TwitchySharp.Api;

public delegate ValueTask<AccessTokenRefreshResult> AccessTokenRefresher<TDetails>(
    TDetails tokenDetails, CancellationToken ct = default)
    where TDetails : AccessTokenDetails;
