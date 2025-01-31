using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Gets information about an extension.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-extensions">get extensions</see> for more information.
/// </summary>
/// <remarks>
/// Requires a signed JSON Web Token (JWT) created by an EBS. 
/// For signing requirements, see <see href="https://dev.twitch.tv/docs/extensions/building/#signing-the-jwt">Signing the JWT</see>. 
/// The signed JWT must include the role field (see <see href="https://dev.twitch.tv/docs/extensions/reference/#jwt-schema">JWT Schema</see>). 
/// The role field must be set to external.
/// </remarks>
/// <param name="clientId">The client id of the extension.</param>
/// <param name="jwt">A signed JWT created by an EBS.</param>
/// <param name="extensionId">The id of the extension to get.</param>
/// <param name="extensionVersion">
/// The version of the extension to get. 
/// If not specified, it returns the latest, released version. 
/// If the extension doesn't have a released version, you must specify a version; otherwise, <see cref="GetExtensionsResponse.Data"/> is empty.
/// </param>
public class GetExtensionsRequest(string clientId, string jwt, string extensionId, string? extensionVersion = null)
    : HelixApiRequest<GetExtensionsResponse>(
        "/extensions" + 
        new HttpQueryParameters()
            .Add("extension_id", extensionId)
            .Add("extension_version", extensionVersion),
        clientId,
        jwt
        );
