using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Removes a single chat message or all chat messages from the broadcaster’s chat room.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#delete-chat-messages">delete chat messages</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageChatMessages"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorManageChatMessages"/>.</param>
/// <param name="broadcasterId">The user id of the broadcaster (channel) whose chat to delete a chat message from.</param>
/// <param name="moderatorId">
/// The user id of the broadcaster or a moderator of the broadcaster's channel.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="messageId">
/// The id of the message to remove.
/// The message must:
/// <list type="bullet">
/// <item>have been created within the last 6 hours.</item>
/// <item>not belong to the broadcaster.</item>
/// <item>not belong to a different moderator than specified in the <paramref name="moderatorId"/>.</item>
/// </list>
/// If this parameter is <see langword="null"/>, the request removes all messages in the chatroom.
/// </param>
public class DeleteChatMessagesRequest(
    string clientId,
    string accessToken,
    string broadcasterId,
    string moderatorId,
    string? messageId = null
    )
    : HelixApiRequest<DeleteChatMessagesResponse>(
        "/moderation/chat" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("moderator_id", moderatorId)
            .Add("message_id", messageId),
        clientId,
        accessToken
        )
{
    public override HttpMethod Method => HttpMethod.Delete;
}
