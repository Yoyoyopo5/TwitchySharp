using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Sends a message to one or more viewers. 
/// You can send messages to a specific channel or to all channels where your extension is active. 
/// This endpoint uses the same mechanism as the <see href="https://dev.twitch.tv/docs/extensions/reference#send">send</see> JavaScript helper function used to send messages.
/// </summary>
/// <remarks>
/// Rate Limits: You may send a maximum of 100 messages per minute per combination of extension client ID and broadcaster ID.
/// <br/>
/// Requires a signed JSON Web Token (JWT) created by an EBS. 
/// For signing requirements, see <see href="https://dev.twitch.tv/docs/extensions/building/#signing-the-jwt">Signing the JWT</see>. 
/// The signed JWT must include the role, user_id, and exp fields (see <see href="https://dev.twitch.tv/docs/extensions/reference/#jwt-schema">JWT Schema</see>) along with the channel_id and pubsub_perms fields. 
/// The role field must be set to external.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="jwt">A signed JWT created by an EBS.</param>
/// <param name="messageData">Data used to create and send the message.</param>
public class SendExtensionPubSubMessageRequest(string clientId, string jwt, SendExtensionPubSubMessageRequestData messageData)
    : HelixApiRequest<SendExtensionPubSubMessageResponse, SendExtensionPubSubMessageRequestData>(
        "/extensions/pubsub",
        clientId,
        jwt,
        messageData
        );

/// <summary>
/// Contains data used to send an extension PubSub message.
/// </summary>
public record SendExtensionPubSubMessageRequestData
{
    /// <summary>
    /// The target of the message. 
    /// The <see cref="ExtensionPubSubMessageTarget.Broadcast"/> and <see cref="ExtensionPubSubMessageTarget.Global"/> values are mutually exclusive.
    /// </summary>
    public required ExtensionPubSubMessageTarget[] Target { get; set; }
    /// <summary>
    /// The user id of the broadcaster to send the message to. 
    /// Don’t include this field if <see cref="IsGlobalBroadcast"/> is set to <see langword="true"/>.
    /// </summary>
    public required string BroadcasterId { get; set; }
    /// <summary>
    /// Determines whether the message should be sent to all channels where your extension is active. 
    /// Set to <see langword="true"/> if the message should be sent to all channels. The default is <see langword="false"/>.
    /// </summary>
    public bool? IsGlobalBroadcast { get; set; }
    /// <summary>
    /// The message to send. The message can be a plain-text string or a string-encoded JSON object. 
    /// The message is limited to a maximum of 5 KB.
    /// </summary>
    public required string Message { get; set; }
}

/// <summary>
/// Contains static values for possible Extension PubSub message targets.
/// </summary>
/// <param name="target"></param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<ExtensionPubSubMessageTarget, string>))]
public record ExtensionPubSubMessageTarget(string target)
    : ValueBackedEnum<string>(target)
{
    /// <summary>
    /// Sends a message to a specific channel's chatroom.
    /// </summary>
    public static ExtensionPubSubMessageTarget Broadcast { get; } = new("broadcast");
    /// <summary>
    /// Sends a message to all channels on which the extension is active.
    /// </summary>
    public static ExtensionPubSubMessageTarget Global { get; } = new("global");
    /// <summary>
    /// Sends a message to a specific user as a whisper.
    /// </summary>
    /// <param name="userId">The user id of the user to send the message to.</param>
    /// <returns></returns>
    public static ExtensionPubSubMessageTarget Whisper(string userId) => new($"whisper-{userId}");
}
