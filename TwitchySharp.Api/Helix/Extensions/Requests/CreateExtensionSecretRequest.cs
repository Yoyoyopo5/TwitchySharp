using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Creates a shared secret used to sign and verify JWT tokens.
/// Creating a new secret removes the current secrets from service. 
/// Use this function only when you are ready to use the new secret it returns.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#create-extension-secret">create extension secret</see> for more information.
/// </summary>
/// <remarks>
/// Requires a signed JSON Web Token (JWT) created by an EBS. 
/// For signing requirements, see <see href="https://dev.twitch.tv/docs/extensions/building/#signing-the-jwt">Signing the JWT</see>. 
/// The signed JWT must include the role, user_id, and exp fields (see <see href="https://dev.twitch.tv/docs/extensions/reference/#jwt-schema">JWT Schema</see>). 
/// The role field must be set to external.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A signed JWT created by an EBS.</param>
/// <param name="extensionId">The id of the extension to apply the shared secret to.</param>
/// <param name="delay">
/// The amount of time, in <b>seconds</b>, to delay activating the secret. 
/// The delay should provide enough time for instances of the extension to gracefully switch over to the new secret. 
/// The minimum delay is 300 seconds (5 minutes). 
/// The default is 300 seconds.
/// </param>
public class CreateExtensionSecretRequest(string clientId, string accessToken, string extensionId, int? delay = null)
    : HelixApiRequest<CreateExtensionSecretResponse>(
        "/extensions/jwt/secrets" +
        new HttpQueryParameters()
            .Add("extension_id", extensionId)
            .Add("delay", delay?.ToString()),
        clientId,
        accessToken
        )
{
    public override HttpMethod Method => HttpMethod.Post;
}
