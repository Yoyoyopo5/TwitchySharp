using System.Collections.Immutable;

namespace TwitchySharp.Api.AuthorizationResolution;

public abstract partial record AccessTokenDetails
{
    /// <summary>
    /// Contains information about a specific <see cref="UserAccessToken"/>,
    /// including its associated user identity, expiration, refresh token, and scopes. 
    /// </summary>
    public sealed record User : AccessTokenDetails
    {
        /// <summary>
        /// The user and client pair that this access token is associated with.
        /// </summary>
        public new required TwitchIdentity.User Identity { get; init; }
        protected override TwitchIdentity BaseIdentity => Identity;
        /// <summary>
        /// The access token.
        /// </summary>
        public new required UserAccessToken AccessToken { get; init; }
        protected override IAccessToken BaseAccessToken => AccessToken;
        /// <summary>
        /// The refresh token that can be used to obtain a new access token when this one expires, if any.
        /// </summary>
        public RefreshToken? RefreshToken { get; init; }
        /// <summary>
        /// The scopes present on this access token.
        /// </summary>
        public IReadOnlySet<Scope> Scopes { get; init; } = ImmutableHashSet<Scope>.Empty;

        /// <inheritdoc cref="AccessTokenDetails.ExpiresAt"/>
        public new DateTimeOffset ExpiresAt { get; init; }
        protected override DateTimeOffset BaseExpiresAt => ExpiresAt;
    }
}

public static partial class AccessTokenDetailsEnumerableExtensions
{
    /// <summary>
    /// Filters user access token details by a specific <see cref="TwitchRequestAuthorizationContext"/>
    /// so that the matching tokens meet the client id, user id, and scope requirements for the context.
    /// </summary>
    /// <param name="tokens">The token enumerable to filter.</param>
    /// <param name="context">
    /// The authorization context. 
    /// This must use a <see cref="TwitchIdentity.User"/> identity or the resulting <see cref="IEnumerable{T}"/> will be empty.
    /// </param>
    /// <returns>A filtered <see cref="IEnumerable{T}"/> with <see cref="AccessTokenDetails.User"/> that meet the authorization requirement.</returns>
    public static IEnumerable<AccessTokenDetails.User> WhereTokenMeetsRequirements(
        this IEnumerable<AccessTokenDetails.User> tokens,
        TwitchRequestAuthorizationContext context)
        => context.Identity switch
        {
            TwitchIdentity.User identity
                => tokens
                    .Where(t => t.Identity == identity)
                    .Where(t => context.ValidScopes.Any() switch
                    {
                        true => context.ValidScopes.Any(scope => t.Scopes.Contains(scope)),
                        false => true
                    }),
            _ => []
        };
}
