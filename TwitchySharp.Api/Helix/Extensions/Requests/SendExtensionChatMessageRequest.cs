using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Sends a message to the specified broadcaster’s chat room. 
/// The extension’s name is used as the username for the message in the chat room. 
/// To send a chat message, your extension must enable Chat Capabilities (under your extension’s Capabilities tab).
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#send-extension-chat-message">send extension chat message</see> for more information.
/// </summary>
/// <remarks>
/// <b>Rate Limits:</b> You may send a maximum of 12 messages per minute per channel.
/// <br/>
/// Requires a signed JSON Web Token (JWT) created by an EBS. 
/// For signing requirements, see <see href="https://dev.twitch.tv/docs/extensions/building/#signing-the-jwt">Signing the JWT</see>. 
/// The signed JWT must include the role and user_id fields (see <see href="https://dev.twitch.tv/docs/extensions/reference/#jwt-schema">JWT Schema</see>). 
/// The role field must be set to external.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="jwt">A signed JWT created by an EBS.</param>
/// <param name="broadcasterId">The user id of the broadcaster that has activated the extension.</param>
/// <param name="messageData">The data to send.</param>
public class SendExtensionChatMessageRequest(string clientId, string jwt, string broadcasterId, SendExtensionChatMessageRequestData messageData)
    : HelixApiRequest<SendExtensionChatMessageResponse, SendExtensionChatMessageRequestData>(
        "/extensions/chat" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId),
        clientId,
        jwt,
        messageData
        );

/// <summary>
/// Contains data used to send an extension chat message.
/// </summary>
public record SendExtensionChatMessageRequestData
{
    /// <summary>
    /// The message to send in chat.
    /// The message may contain a maximum of 280 characters.
    /// </summary>
    public required string Text { get; set; }
    /// <summary>
    /// The id of the extension that's sending the chat message.
    /// </summary>
    public required string ExtensionId { get; set; }
    /// <summary>
    /// The extension's version number.
    /// </summary>
    public required string ExtensionVersion { get; set; }
}
