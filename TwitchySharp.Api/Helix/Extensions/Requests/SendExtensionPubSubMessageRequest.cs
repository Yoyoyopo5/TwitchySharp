using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
/// See <see href="https://dev.twitch.tv/docs/api/reference/#send-extension-pubsub-message">Send Extension PubSub Message</see> for more information.
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
/// <param name="messageData">
/// Data used to create and send the message.
/// Use derived classes <see cref="BroadcastPubSubMessageData"/> and <see cref="GlobalPubSubMessageData"/>.
/// </param>
public class SendExtensionPubSubMessageRequest(string clientId, string jwt, SendExtensionPubSubMessageRequestData messageData)
    : HelixApiRequest<SendExtensionPubSubMessageResponse, SendExtensionPubSubMessageRequestData>(
        "/extensions/pubsub",
        clientId,
        jwt,
        messageData
        );

/// <summary>
/// Used to send a PubSub message globally to all instances of an extension.
/// </summary>
public record GlobalPubSubMessageData
    : SendExtensionPubSubMessageRequestData
{
    /// <summary>
    /// <inheritdoc cref="GlobalPubSubMessageData"/>
    /// </summary>
    public GlobalPubSubMessageData()
        => (_target, IsGlobalBroadcast) = ([ ExtensionPubSubMessageTarget.Global ], true);
}

/// <summary>
/// Used to send a PubSub message to a specific broadcaster or specific user through Whispers.
/// Be sure to set the <see cref="ExtensionJwtPayload.ChannelId"/> to the user id of the channel you want to broadcast to.
/// </summary>
public record BroadcastPubSubMessageData
    : SendExtensionPubSubMessageRequestData
{
    /// <summary>
    /// <inheritdoc cref="BroadcastPubSubMessageData"/>
    /// </summary>
    public BroadcastPubSubMessageData()
        => IsGlobalBroadcast = false;

    /// <summary>
    /// Set the broadcaster the message should be sent to.
    /// </summary>
    /// <param name="broadcasterId">The user id of the broadcaster to send the PubSub message to.</param>
    public BroadcastPubSubMessageData To(string broadcasterId)
        => this with { _target = _target.Add(ExtensionPubSubMessageTarget.Broadcast), BroadcasterId = broadcasterId };

    /// <summary>
    /// Set the user the message should be sent to through Whispers.
    /// </summary>
    /// <param name="userId">The id of the user to send the PubSub message to.</param>
    public BroadcastPubSubMessageData WhisperTo(string userId)
        => this with { _target = _target.Add(ExtensionPubSubMessageTarget.Whisper(userId)) };
}

/// <summary>
/// Contains data used to send an extension PubSub message.
/// Use derived classes <see cref="BroadcastPubSubMessageData"/> and <see cref="GlobalPubSubMessageData"/>.
/// </summary>
public record SendExtensionPubSubMessageRequestData
{
    protected ImmutableHashSet<ExtensionPubSubMessageTarget> _target { get; set; } = [];
    /// <summary>
    /// The target of the message. 
    /// The <see cref="ExtensionPubSubMessageTarget.Broadcast"/> and <see cref="ExtensionPubSubMessageTarget.Global"/> values are mutually exclusive.
    /// </summary>
    public IEnumerable<ExtensionPubSubMessageTarget> Target => _target;
    /// <summary>
    /// The user id of the broadcaster to send the message to. 
    /// Don’t include this field if <see cref="IsGlobalBroadcast"/> is set to <see langword="true"/>.
    /// </summary>
    public string? BroadcasterId { get; protected set; }
    /// <summary>
    /// Determines whether the message should be sent to all channels where your extension is active. 
    /// Set to <see langword="true"/> if the message should be sent to all channels. The default is <see langword="false"/>.
    /// </summary>
    public bool? IsGlobalBroadcast { get; protected set; }
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
