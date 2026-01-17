using System.Net.Http;
using TwitchySharp.Api.Models.Helix.Extensions.Responses;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Extensions.Requests;
/// <summary>
/// Sends a message to the specified broadcaster’s chat room.
/// </summary>
/// <remarks>
/// The extension’s name is used as the username for the message in the chat room. 
/// To send a chat message, your extension must enable Chat Capabilities (under your extension’s Capabilities tab).
/// <b>Rate Limits:</b> You may send a maximum of 12 messages per minute per channel.
/// <br/>
/// Requires a signed JSON Web Token (JWT) created by an EBS. 
/// For signing requirements, see <see href="https://dev.twitch.tv/docs/extensions/building/#signing-the-jwt">Signing the JWT</see>. 
/// The signed JWT must include the role and user_id fields (see <see href="https://dev.twitch.tv/docs/extensions/reference/#jwt-schema">JWT Schema</see>). 
/// The role field must be set to external.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#send-extension-chat-message">Send Extension Chat Message</see> for more information.
/// </remarks>
public record SendExtensionChatMessageRequest : TwitchHelixRequest<SendExtensionChatMessageResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="jwt">A signed JWT created by an EBS.</param>
    /// <param name="broadcasterId">The user id of the broadcaster that has activated the extension.</param>
    /// <param name="messageData">The data to send.</param>
    public SendExtensionChatMessageRequest(
        string clientId,
        string jwt,
        string broadcasterId,
        SendExtensionChatMessageRequestData messageData
        ) : base(
            "/extensions/chat",
            clientId,
            jwt,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
        )
    {
        Method = HttpMethod.Post;
        ContentObject = messageData;
    }
}

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
