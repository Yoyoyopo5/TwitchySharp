using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Gets the specified configuration segment from the specified extension.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-extension-configuration-segment">get extension configuration segment</see> for more information.
/// </summary>
/// <remarks>
/// <b>Rate Limits:</b> You may retrieve each segment a maximum of 20 times per minute.
/// Requires a signed JSON Web Token (JWT) created by an EBS. 
/// For signing requirements, see <see href="https://dev.twitch.tv/docs/extensions/building/#signing-the-jwt">Signing the JWT</see>. 
/// The signed JWT must include the role, user_id, and exp fields (see <see href="https://dev.twitch.tv/docs/extensions/reference/#jwt-schema">JWT Schema</see>). 
/// Set the role field to external and the user_id field to the user id of the user that owns the extension..
/// </remarks>
/// <param name="clientId">The client id of the extension.</param>
/// <param name="jwt">
/// The signed JWT created by an Extension Backend Service.
/// For signing requirements, see <see href="https://dev.twitch.tv/docs/extensions/building/#signing-the-jwt">Signing the JWT</see>.
/// The signed JWT must include the role, user_id, and exp fields (see <see href="https://dev.twitch.tv/docs/extensions/reference/#jwt-schema">JWT Schema</see>). The role field must be set to external.
/// </param>
/// <param name="broadcasterId">
/// The user id of the broadcaster that installed the extension. 
/// This parameter is required if you set the <paramref name="segments"/> parameter to <see cref="ExtensionConfigurationSegmentType.Broadcaster"/> or <see cref="ExtensionConfigurationSegmentType.Developer"/>. 
/// Do not specify this parameter if you set segment to <see cref="ExtensionConfigurationSegmentType.Global"/>.
/// </param>
/// <param name="extensionId">
/// The extension id of the extension that contains the configuration segment you want to get.
/// </param>
/// <param name="segments">
/// The type of configuration segment to get.
/// You may specify one or more segments. Duplicate segments are ignored.
/// </param>
public class GetExtensionConfigurationSegmentRequest(string clientId, string jwt, string extensionId, IEnumerable<ExtensionConfigurationSegmentType> segments, string? broadcasterId = null)
    : HelixApiRequest<GetExtensionConfigurationSegmentResponse>(
        "/extensions/configurations" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("extension_id", extensionId)
            .Add("segment", segments.Select<ExtensionConfigurationSegmentType, string>(x => x)),
        clientId,
        jwt
        );