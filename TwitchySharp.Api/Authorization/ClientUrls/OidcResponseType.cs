using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchySharp.Api.Authorization.ClientUrls;
/// <summary>
/// A flags enum used with the <see cref="ImplicitGrantUrl"/>.
/// </summary>
[Flags]
public enum OidcResponseType
{
    /// <summary>
    /// Requests an access token used to authorize Twitch API requests on behalf of the user.
    /// </summary>
    Token = 1,
    /// <summary>
    /// Requests an OpenID id token that contains claims information about the authorizing user.
    /// </summary>
    IdToken = 2
}

internal static class OidcResponseTypeExtensions
{
    public static string ToResponseType(this OidcResponseType responseType)
        => (int)responseType switch
        {
            1 => "token",
            2 => "id_token",
            3 => "token+id_token",
            _ => "token"
        };
}
