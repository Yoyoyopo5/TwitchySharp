namespace TwitchySharp.Api.AuthorizationResolution;

/// <summary>
/// Token resolution options specific to extension JWT resolution.
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/extensions/building/#signing-the-jwt">Signing the JWT</see> for more information.
/// </remarks>
public record ExtensionAccessTokenResolutionOptions : ITokenResolutionOptions<AccessTokenDetails.ExtensionJwt>
{
    /// <summary>
    /// A function returning a cached JWT for a <see cref="TwitchRequestAuthorizationContext"/>.
    /// </summary>
    /// <remarks>
    /// If this returns <see langword="null"/> or an expired token, <see cref="AcquireNewToken"/> will be called.
    /// </remarks>
    public AccessTokenDetailsResolver<AccessTokenDetails.ExtensionJwt>? GetCachedToken { get; init; }
    /// <summary>
    /// A side effect function that runs when a new token is acquired.
    /// </summary>
    /// <remarks>
    /// You can use this to add the new token to a cache.
    /// </remarks>
    public Func<AccessTokenDetails.ExtensionJwt, CancellationToken, ValueTask>? OnNewToken { get; init; }
    /// <summary>
    /// A function that returns a new signed JWT.
    /// </summary>
    /// <remarks>
    /// This function is evaluated if <see cref="GetCachedToken"/> returns <see langword="null"/> or an expired JWT.
    /// </remarks>
    public AccessTokenDetailsResolver<AccessTokenDetails.ExtensionJwt>? AcquireNewToken { get; init; }

    TokenResolutionOptions<AccessTokenDetails.ExtensionJwt> ITokenResolutionOptions<AccessTokenDetails.ExtensionJwt>.ToTokenResolutionOptions()
        => new()
        {
            GetCachedToken = GetCachedToken,
            OnNewToken = OnNewToken,
            AcquireNewToken = AcquireNewToken,
            RefreshToken = async (details, ct) // We use AcquireNewToken here because flow should be the same for both.
                => AcquireNewToken is null 
                ? new AccessTokenRefreshResult.Expired<AccessTokenDetails.ExtensionJwt>(details)
                : await AcquireNewToken(new() { Identity = details.Identity }, ct) switch
                {
                    AccessTokenDetails.ExtensionJwt jwt 
                        => new AccessTokenRefreshResult.Refreshed<AccessTokenDetails.ExtensionJwt>(jwt),
                    _ when details.ExpiresAt > DateTimeOffset.UtcNow
                        => new AccessTokenRefreshResult.Valid<AccessTokenDetails.ExtensionJwt>(details),
                    _ => new AccessTokenRefreshResult.Expired<AccessTokenDetails.ExtensionJwt>(details),
                }
        };
}
