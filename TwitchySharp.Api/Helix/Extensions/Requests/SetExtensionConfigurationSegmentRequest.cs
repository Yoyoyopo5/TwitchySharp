using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Updates a configuration segment. 
/// The segment is limited to 5 KB. 
/// Extensions that are active on a channel do not receive the updated configuration.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#set-extension-configuration-segment">set extension configuration segment</see> for more information.
/// </summary>
/// <remarks>
/// <b>Rate Limits:</b> You may update the configuration a maximum of 20 times per minute.
/// Requires a signed JSON Web Token (JWT) created by an EBS. 
/// For signing requirements, see <see href="https://dev.twitch.tv/docs/extensions/building/#signing-the-jwt">Signing the JWT</see>. 
/// The signed JWT must include the role, user_id, and exp fields (see <see href="https://dev.twitch.tv/docs/extensions/reference/#jwt-schema">JWT Schema</see>). 
/// Set the role field to external and the user_id field to the user id of the user that owns the extension.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="jwt">
/// A signed JWT created by an Extension Backend Service.
/// For signing requirements, see <see href="https://dev.twitch.tv/docs/extensions/building/#signing-the-jwt">Signing the JWT</see>.
/// The signed JWT must include the role, user_id, and exp fields (see <see href="https://dev.twitch.tv/docs/extensions/reference/#jwt-schema">JWT Schema</see>). The role field must be set to external.
/// </param>
/// <param name="data">
/// Data used to set the configuration.
/// </param>
public class SetExtensionConfigurationSegmentRequest(string clientId, string jwt, SetExtensionConfigurationSegmentRequestData data)
    : HelixApiRequest<SetExtensionConfigurationSegmentResponse, SetExtensionConfigurationSegmentRequestData>(
        "/extensions/configurations",
        clientId,
        jwt,
        data
        )
{
    public override HttpMethod Method => HttpMethod.Put;
}

/// <summary>
/// Contains data used to set an extension configuration segment.
/// </summary>
public record SetExtensionConfigurationSegmentRequestData
{
    /// <summary>
    /// The id of the extension to update.
    /// </summary>
    public required string ExtensionId { get; set; }
    /// <summary>
    /// The configuration segment to update.
    /// </summary>
    [JsonConverter(typeof(SnakeCaseLowerJsonStringEnumConverter<ExtensionConfigurationSegmentType>))]
    public required ExtensionConfigurationSegmentType Segment { get; set; }
    /// <summary>
    /// The user id of the broadcaster that installed the extension.
    /// Include this property only if the <see cref="Segment"/> is set to <see cref="ExtensionConfigurationSegmentType.Developer"/> or <see cref="ExtensionConfigurationSegmentType.Broadcaster"/>.
    /// </summary>
    public string? BroadcasterId { get; set; }
    /// <summary>
    /// The contents of the segment.
    /// This may be in plain-text or string-encoded JSON.
    /// </summary>
    public string? Content { get; set; }
    /// <summary>
    /// The version number that identifies this definition of the segment’s data. 
    /// If not specified, the latest definition is updated.
    /// </summary>
    public string? Version { get; set; }
}
