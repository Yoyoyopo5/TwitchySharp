using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Gets an extension's list of shared secrets.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-extension-secrets">get extension secrets</see> for more information.
/// </summary>
/// <remarks>
/// Requires a signed JSON Web Token (JWT) created by an EBS. 
/// For signing requirements, see <see href="https://dev.twitch.tv/docs/extensions/building/#signing-the-jwt">Signing the JWT</see>. 
/// The signed JWT must include the role, user_id, and exp fields (see <see href="https://dev.twitch.tv/docs/extensions/reference/#jwt-schema">JWT Schema</see>). 
/// The role field must be set to external.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A signed JWT created by an EBS.</param>
/// <param name="extensionId">The id of the extension whose shared secrets you want to get.</param>
public class GetExtensionSecretsRequest(string clientId, string accessToken, string extensionId)
    : HelixApiRequest<GetExtensionSecretsResponse>(
        "/extensions/jwt/secrets" +
        new HttpQueryParameters()
            .Add("extension_id", extensionId),
        clientId,
        accessToken
        );
