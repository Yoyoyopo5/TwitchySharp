namespace TwitchySharp.Api.AuthorizationResolution;

public abstract partial record AccessTokenDetails
{
    public sealed record ExtensionJwt : AccessTokenDetails
    {
        /// <summary>
        /// The extension owner user id and client id associated with the JWT.
        /// </summary>
        public new required TwitchIdentity.Extension Identity { get; init; }
        protected override TwitchIdentity BaseIdentity => Identity;
        /// <summary>
        /// The extension JWT signed by an EBS.
        /// </summary>
        public new required ExtensionJsonWebToken AccessToken { get; init; }
        protected override IAccessToken BaseAccessToken => AccessToken;
    }
}

public static partial class AccessTokenDetailsEnumerableExtensions
{
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
