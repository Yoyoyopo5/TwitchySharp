using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Removes a single chat message or all chat messages from the broadcaster’s chat room.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageChatMessages"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#delete-chat-messages">Delete Chat Messages</see> for more information.
/// </remarks>
public record DeleteChatMessagesRequest
    : TwitchHelixRequest<DeleteChatMessagesResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorManageChatMessages"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    public DeleteChatMessagesRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        DeleteChatMessagesRequestParameters parameters
        ) : base(
            "/moderation/chat",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("moderator_id", parameters.ModeratorId)
                .Add("message_id", parameters.MessageId)
            )
    {
        Method = HttpMethod.Delete;
    }
}

/// <summary>
/// Request parameters for a <see cref="DeleteChatMessagesRequest"/>.
/// </summary>
public record DeleteChatMessagesRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster (channel) whose chat to delete a chat message from.
    /// </summary>
    public required UserId BroadcasterId { get; set; }

    /// <summary>
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId ModeratorId { get; set; }

    /// <summary>
    /// The id of the message to remove.
    /// </summary>
    /// <remarks>
    /// The message must:
    /// <list type="bullet">
    /// <item>have been created within the last 6 hours.</item>
    /// <item>not belong to the broadcaster.</item>
    /// <item>not belong to a different moderator than specified in the moderatorId.</item>
    /// </list>
    /// If this parameter is <see langword="null"/>, the request removes all messages in the chatroom.
    /// </remarks>
    public MessageId? MessageId { get; set; }
}
