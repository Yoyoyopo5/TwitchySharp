using Microsoft.IdentityModel.Tokens;

namespace TwitchySharp.Api.Authentication;

/// <summary>
/// Response containing the OIDC JWKs.
/// </summary>
public record OidcJwkResponseContent
{
    /// <summary>
    /// An array of JWKs used to verify OIDC JWTs obtained as ID tokens during Twitch OIDC authorization flows.
    /// </summary>
    public required JsonWebKey[] Keys { get; init; }
}
