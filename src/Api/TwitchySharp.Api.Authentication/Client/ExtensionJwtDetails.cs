namespace TwitchySharp.Api.Authentication;

public static partial class AccessTokenDetails
{
    /// <summary>
    /// Details for an Extension Json Web Token.
    /// </summary>
    /// <param name="Identity">The extension owner user id and client id associated with the JWT.</param>
    /// <param name="JsonWebToken">The extension JWT signed by an EBS.</param>
    public sealed record ExtensionJwt(TwitchIdentity.Extension Identity, ExtensionJsonWebToken JsonWebToken) : IAccessTokenDetails<TwitchIdentity.Extension>
    {
        public BearerToken BearerToken => JsonWebToken;
        public DateTimeOffset ExpiresAt { get; init; } = JsonWebToken.ToJsonWebToken().ValidTo;
    }
}
