using TwitchySharp.Api.Helix.Moderation;

namespace TwitchySharp.Api.AuthorizationResolution;

public abstract partial record AccessTokenDetails
{
    /// <summary>
    /// Details associated with a specific <see cref="AppAccessToken"/>.
    /// </summary>
    public sealed record App : AccessTokenDetails
    {
        /// <summary>
        /// The client identity associated with the app access token.
        /// </summary>
        public new required TwitchIdentity.Client Identity { get; init; }
        protected override TwitchIdentity BaseIdentity => Identity;
        /// <summary>
        /// The app access token.
        /// </summary>
        public new required AppAccessToken AccessToken { get; init; }
        protected override IAccessToken BaseAccessToken => AccessToken;
    }
}

/// <summary>
/// Contains custom LINQ extensions for filtering enumerables of AccessTokenDetails (useful for filtering caches).
/// </summary>
public static class AccessTokenDetailsEnumerableExtensions
{
    public static IEnumerable<AccessTokenDetails> WhereTokenMeetsRequirements(
        this IEnumerable<AccessTokenDetails> tokens,
        TwitchRequestAuthorizationContext context)
        => tokens
            .Where(t => t.Identity.GetType() == context.Identity.GetType())
            .Where(t => t switch
            {
                AccessTokenDetails.User userToken => context.ValidScopes.Any(scope => userToken.Scopes.Contains(scope)),
                _ => true
            });

    public static IEnumerable<AccessTokenDetails.App> WhereTokenMeetsRequirements(
        this IEnumerable<AccessTokenDetails.App> tokens,
        TwitchRequestAuthorizationContext context)
        => context.Identity switch
        {
            TwitchIdentity.Client identity => tokens.Where(t => t.Identity == identity),
            _ => []
        };

    public static IEnumerable<AccessTokenDetails.User> WhereTokenMeetsRequirements(
        this IEnumerable<AccessTokenDetails.User> tokens,
        TwitchRequestAuthorizationContext context)
        => context.Identity switch
        {
            TwitchIdentity.User identity
                => tokens
                    .Where(t => t.Identity == identity)
                    .Where(t => context.ValidScopes.Any(scope => t.Scopes.Contains(scope))),
            _ => []
        };

    public static IEnumerable<AccessTokenDetails.ExtensionJwt> WhereTokenMeetsRequirements(
        this IEnumerable<AccessTokenDetails.ExtensionJwt> tokens,
        TwitchRequestAuthorizationContext context)
        => context.Identity switch
        {
            TwitchIdentity.Extension identity
                => tokens.Where(t => t.Identity == identity),
            _ => []
        };
}
