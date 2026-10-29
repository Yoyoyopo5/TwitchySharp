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
        /// <inheritdoc/>
        public BearerToken BearerToken => AccessToken;
        /// <summary>
        /// The refresh token that can be used to obtain a new access token when this one expires, if any.
        /// </summary>
        public RefreshToken? RefreshToken { get; init; }
        /// <summary>
        /// The scopes present on this access token.
        /// </summary>
        public IReadOnlySet<Scope> Scopes { get; init; } = ImmutableHashSet<Scope>.Empty;
        /// <inheritdoc/>
        public required DateTimeOffset ExpiresAt { get; init; }
    }

    /// <summary>
    /// Obtain user access token details from an <see cref="AccessTokenRefreshResponseContent"/>.
    /// </summary>
    /// <param name="refreshResponse">The refresh response to get token details from.</param>
    /// <param name="clientId">The client id used to refresh the token.</param>
    /// <param name="userId">The id of the user the token belongs to.</param>
    /// <param name="now">The current time. Used for determining token expiry.</param>
    /// <returns>User access token details for the refreshed token.</returns>
    public static AccessTokenDetails.User ToAccessTokenDetails(
        this AccessTokenRefreshResponseContent refreshResponse,
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

    /// <summary>
    /// Obtain user access token details from an <see cref="AuthorizationCodeResponseContent"/>.
    /// </summary>
    /// <param name="authorizationCodeResponse">The response to get details from.</param>
    /// <param name="clientId">The client id used to make the <see cref="AuthorizationCodeRequest"/>.</param>
    /// <param name="userId"><inheritdoc cref="ToAccessTokenDetails(AccessTokenRefreshResponseContent, ClientId, UserId, DateTimeOffset)" path="/param[@name = 'userId']"/></param>
    /// <param name="now"><inheritdoc cref="ToAccessTokenDetails(AccessTokenRefreshResponseContent, ClientId, UserId, DateTimeOffset)" path="/param[@name = 'now']"/></param>
    /// <returns>User access token details for the new token.</returns>
    public static AccessTokenDetails.User ToAccessTokenDetails(
        this AuthorizationCodeResponseContent authorizationCodeResponse,
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

    /// <summary>
    /// Obtain user access token details from a <see cref="DeviceCodeTokenResponseContent"/>.
    /// </summary>
    /// <param name="deviceCodeTokenResponse">The device code response to get token details from.</param>
    /// <param name="clientId">The client id used to make the <see cref="DeviceCodeTokenRequest"/>.</param>
    /// <param name="userId"><inheritdoc cref="ToAccessTokenDetails(AccessTokenRefreshResponseContent, ClientId, UserId, DateTimeOffset)" path="/param[@name = 'userId']"/></param>
    /// <param name="now"><inheritdoc cref="ToAccessTokenDetails(AccessTokenRefreshResponseContent, ClientId, UserId, DateTimeOffset)" path="/param[@name = 'now']"/></param>
    /// <returns>User access token details for the new token.</returns>
    public static AccessTokenDetails.User ToAccessTokenDetails(
        this DeviceCodeTokenResponseContent deviceCodeTokenResponse,
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
