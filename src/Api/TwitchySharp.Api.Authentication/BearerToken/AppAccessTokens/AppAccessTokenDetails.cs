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

        /// <inheritdoc cref="AccessTokenDetails.ExpiresAt"/>
        public new DateTimeOffset ExpiresAt { get; init; }
        protected override DateTimeOffset BaseExpiresAt => ExpiresAt;
    }
}

/// <summary>
/// Contains custom LINQ extensions for filtering enumerables of AccessTokenDetails (useful for filtering caches).
/// </summary>
public static partial class AccessTokenDetailsEnumerableExtensions
{
    public static IEnumerable<AccessTokenDetails.App> WhereTokenMeetsRequirements(
        this IEnumerable<AccessTokenDetails.App> tokens,
        TwitchRequestAuthorizationContext context)
        => context.Identity switch
        {
            TwitchIdentity.Client identity
                => tokens.Where(t => t.Identity == identity),
            _ => []
        };
}
