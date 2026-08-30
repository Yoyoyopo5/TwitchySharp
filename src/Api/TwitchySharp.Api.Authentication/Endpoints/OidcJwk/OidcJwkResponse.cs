using Microsoft.IdentityModel.Tokens;

namespace TwitchySharp.Api.Authentication;

public record OidcJwkResponse
{
    /// <summary>
    /// An array of JWKs used to verify OIDC JWTs obtained as ID tokens during Twitch OIDC authorization flows.
    /// </summary>
    public required JsonWebKey[] Keys { get; init; }
}
