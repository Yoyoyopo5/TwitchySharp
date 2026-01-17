using System.Net.Http;
using TwitchySharp.Api.Models.Helix.Extensions.Responses;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Extensions.Requests;
/// <summary>
/// Gets information about an extension.
/// </summary>
/// <remarks>
/// <br/>
/// Requires a signed JSON Web Token (JWT) created by an EBS. 
/// For signing requirements, see <see href="https://dev.twitch.tv/docs/extensions/building/#signing-the-jwt">Signing the JWT</see>. 
/// The signed JWT must include the role field (see <see href="https://dev.twitch.tv/docs/extensions/reference/#jwt-schema">JWT Schema</see>). 
/// The role field must be set to external.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-extensions">Get Extensions</see> for more information.
/// </remarks>
public record GetExtensionsRequest : TwitchHelixRequest<GetExtensionsResponse>
{
    /// <param name="clientId">The client id of the extension.</param>
    /// <param name="jwt">A signed JWT created by an EBS.</param>
    /// <param name="extensionId">The id of the extension to get.</param>
    /// <param name="extensionVersion">
    /// The version of the extension to get. 
    /// If not specified, it returns the latest, released version. 
    /// If the extension doesn't have a released version, you must specify a version; otherwise, <see cref="GetExtensionsResponse.Data"/> is empty.
    /// </param>
    public GetExtensionsRequest(
        string clientId,
        string jwt,
        string extensionId,
        string? extensionVersion = null
        ) : base(
            "/extensions",
            clientId,
            jwt,
            new HttpQueryParameters()
                .Add("extension_id", extensionId)
                .Add("extension_version", extensionVersion)
            )
    {
        Method = HttpMethod.Get;
    }
}
