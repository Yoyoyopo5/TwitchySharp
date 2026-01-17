using System.Net.Http;
using TwitchySharp.Api.Models.Helix.Extensions.Responses;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Extensions.Requests;
/// <summary>
/// Creates a shared secret used to sign and verify JWT tokens.
/// </summary>
/// <remarks>
/// <para>
/// Creating a new secret removes the current secrets from service. 
/// Use this function only when you are ready to use the new secret it returns.
/// </para>
/// Requires a signed JSON Web Token (JWT) created by an EBS. 
/// For signing requirements, see <see href="https://dev.twitch.tv/docs/extensions/building/#signing-the-jwt">Signing the JWT</see>. 
/// The signed JWT must include the role, user_id, and exp fields (see <see href="https://dev.twitch.tv/docs/extensions/reference/#jwt-schema">JWT Schema</see>). 
/// The role field must be set to external.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#create-extension-secret">Create Extension Secret</see> for more information.
/// </remarks>
public record CreateExtensionSecretRequest
    : TwitchHelixRequest<CreateExtensionSecretResponse>
{
    /// <param name="clientId">The client id of the extension.</param>
    /// <param name="jwt">A signed JWT created by an EBS.</param>
    /// <param name="extensionId">The id of the extension to apply the shared secret to.</param>
    /// <param name="delay">
    /// The amount of time, in <b>seconds</b>, to delay activating the secret. 
    /// The delay should provide enough time for instances of the extension to gracefully switch over to the new secret. 
    /// The minimum delay is 300 seconds (5 minutes). 
    /// The default is 300 seconds.
    /// </param>
    public CreateExtensionSecretRequest(
        string clientId,
        string jwt,
        string extensionId,
        int? delay = null
        ) : base(
            "/extensions/jwt/secrets",
            clientId,
            jwt,
            new HttpQueryParameters()
                .Add("extension_id", extensionId)
                .Add("delay", delay?.ToString())
            )
    {
        Method = HttpMethod.Post;
    }
}
