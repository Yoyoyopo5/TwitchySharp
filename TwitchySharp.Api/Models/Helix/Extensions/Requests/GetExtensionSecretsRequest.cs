using System.Net.Http;
using TwitchySharp.Api.Models.Helix.Extensions.Responses;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Extensions.Requests;
/// <summary>
/// Gets an extension's list of shared secrets.
/// </summary>
/// <remarks>
/// <br/>
/// Requires a signed JSON Web Token (JWT) created by an EBS. 
/// For signing requirements, see <see href="https://dev.twitch.tv/docs/extensions/building/#signing-the-jwt">Signing the JWT</see>. 
/// The signed JWT must include the role, user_id, and exp fields (see <see href="https://dev.twitch.tv/docs/extensions/reference/#jwt-schema">JWT Schema</see>). 
/// The role field must be set to external.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-extension-secrets">Get Extension Secrets</see> for more information.
/// </remarks>
public record GetExtensionSecretsRequest
    : TwitchHelixRequest<GetExtensionSecretsResponse>
{
    /// <param name="clientId">The client id of the extension.</param>
    /// <param name="jwt">A signed JWT created by an EBS.</param>
    /// <param name="extensionId">The id of the extension whose shared secrets you want to get.</param>
    public GetExtensionSecretsRequest(
        string clientId,
        string jwt,
        string extensionId
        ) : base(
            "/extensions/jwt/secrets",
            clientId,
            jwt,
            new HttpQueryParameters()
                .Add("extension_id", extensionId)
            )
    {
        Method = HttpMethod.Get;
    }
}
