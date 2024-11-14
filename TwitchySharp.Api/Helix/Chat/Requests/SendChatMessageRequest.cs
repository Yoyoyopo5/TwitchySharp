using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Sends a message to the broadcaster’s chat room.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#send-chat-message">send chat message</see> for more information.
/// <br/>
/// Requires a user access token with <see cref="Scope.UserWriteChat"/> or an app access token where the sending user has <see cref="Scope.UserBot"/> and <see cref="Scope.ChannelBot"/> on another user access token granted to this <paramref name="clientId"/>.
/// </summary>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">
/// A user access token that includes <see cref="Scope.UserWriteChat"/>, or an app access token.
/// <b>NOTE:</b> If an app access token is used, the <see cref="SendChatMessageRequestData.SenderId">sender</see> must have a user access token with <see cref="Scope.UserBot"/> and <see cref="Scope.ChannelBot"/> that was issued to the same <paramref name="clientId"/>
/// <b>AND</b> the sender must be the <see cref="SendChatMessageRequestData.BroadcasterId">broadcaster</see> or a moderator of the broadcaster's channel.
/// </param>
/// <param name="messageData">The message to send.</param>
public class SendChatMessageRequest(string clientId, string accessToken, SendChatMessageRequestData messageData)
    : HelixApiRequest<SendChatMessageResponse, SendChatMessageRequestData>(
        "/chat/messages",
        clientId,
        accessToken,
        messageData
        );

/// <summary>
/// Contains information used to send a chat message to a specific chat room.
/// </summary>
public record SendChatMessageRequestData
{
    /// <summary>
    /// The user id of the broadcaster whose chat room the message will be sent to.
    /// </summary>
    public required string BroadcasterId { get; init; }
    /// <summary>
    /// The user id of the user sending the message.
    /// If a user access token was used in the <see cref="SendChatMessageRequest"/>, this must be the same user who created the token.
    /// If an app access token was used, this user must have created a user access token with <see cref="Scope.UserBot"/> and <see cref="Scope.ChannelBot"/>, and it must be the broadcaster or a moderator in the broadcaster's chat room.
    /// </summary>
    public required string SenderId { get; init; }
    /// <summary>
    /// The message to send. 
    /// The message is limited to a maximum of 500 characters. 
    /// Chat messages can also include emoticons. To include emoticons, use the name of the emote. 
    /// The names are case sensitive. Don’t include colons around the name (e.g., :bleedPurple:). 
    /// If Twitch recognizes the name, Twitch converts the name to the emote before writing the chat message to the chat room.
    /// </summary>
    public required string Message { get; init; }
    /// <summary>
    /// The message id of the chat message being replied to.
    /// </summary>
    public string? ReplyParentMessageId { get; init; }
}
