using System.Collections.Immutable;

namespace TwitchySharp.Api.Authentication;

public static partial class AccessTokenDetails
{
    /// <summary>
    /// Contains information about a specific <see cref="UserAccessToken"/>,
    /// including its associated user identity, expiration, refresh token, and scopes. 
    /// </summary>
    public sealed record User : IAccessTokenDetails<TwitchIdentity.User>
    {
        /// <summary>
        /// The user and client pair that this access token is associated with.
        /// </summary>
        public required TwitchIdentity.User Identity { get; init; }
        /// <summary>
        /// The access token.
        /// </summary>
        public required UserAccessToken AccessToken { get; init; }
        public BearerToken BearerToken => AccessToken;
        /// <summary>
        /// The refresh token that can be used to obtain a new access token when this one expires, if any.
        /// </summary>
        public RefreshToken? RefreshToken { get; init; }
        /// <summary>
        /// The scopes present on this access token.
        /// </summary>
        public IReadOnlySet<Scope> Scopes { get; init; } = ImmutableHashSet<Scope>.Empty;
        public required DateTimeOffset ExpiresAt { get; init; }
    }

    public static AccessTokenDetails.User ToAccessTokenDetails(
        this AccessTokenRefreshResponse refreshResponse,
        ClientId clientId,
        UserId userId,
        DateTimeOffset now
        )
        => new()
        {
            Identity = new(userId, clientId),
            AccessToken = refreshResponse.AccessToken,
            RefreshToken = refreshResponse.RefreshToken,
            Scopes = refreshResponse.Scope?.ToHashSet() ?? [],
            ExpiresAt = now + refreshResponse.ExpiresIn
        };

    public static AccessTokenDetails.User ToAccessTokenDetails(
        this AuthorizationCodeResponse authorizationCodeResponse,
        ClientId clientId,
        UserId userId,
        DateTimeOffset now
        )
        => new()
        {
            Identity = new(userId, clientId),
            AccessToken = authorizationCodeResponse.AccessToken,
            RefreshToken = authorizationCodeResponse.RefreshToken,
            Scopes = authorizationCodeResponse.Scope?.ToHashSet() ?? [],
            ExpiresAt = now + authorizationCodeResponse.ExpiresIn
        };

    public static AccessTokenDetails.User ToAccessTokenDetails(
        this DeviceCodeTokenResponse deviceCodeTokenResponse,
        ClientId clientId,
        UserId userId,
        DateTimeOffset now
        )
        => new()
        {
            Identity = new(userId, clientId),
            AccessToken = deviceCodeTokenResponse.AccessToken,
            RefreshToken = deviceCodeTokenResponse.RefreshToken,
            Scopes = deviceCodeTokenResponse.Scope?.ToHashSet() ?? [],
            ExpiresAt = now + deviceCodeTokenResponse.ExpiresIn
        };
}
