using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models.Helix.Chat.Enums;
using TwitchySharp.Api.Models.Helix.Chat.Responses;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Chat.Requests;
/// <summary>
/// Sends an announcement to the broadcaster’s chat room.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageAnnouncements"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#send-chat-announcement">Send Chat Announcement</see> for more information.
/// </remarks>
public record SendChatAnnouncementRequest
    : TwitchHelixRequest<SendChatAnnouncementResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorManageAnnouncements"/.></param>
    /// <param name="broadcasterId">The user id of the broadcaster whose chat room you want to send the announcement to.</param>
    /// <param name="moderatorId">The user id of the broadcaster or a moderator of the broadcaster's channel. This must be the same user who created the <paramref name="accessToken"/>.</param>
    /// <param name="announcement">The announcement to send.</param>
    public SendChatAnnouncementRequest(
        string clientId,
        string accessToken,
        string broadcasterId,
        string moderatorId,
        SendChatAnnouncementRequestData announcement
        )
        : base(
            "/chat/announcements",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
                .Add("moderator_id", moderatorId)
            )
    {
        Method = HttpMethod.Post;
        ContentObject = announcement;
    }
}

/// <summary>
/// Contains data used to create a chat announcement.
/// </summary>
public record SendChatAnnouncementRequestData
{
    /// <summary>
    /// The announcement to make in the broadcaster’s chat room. 
    /// Announcements are limited to a maximum of 500 characters; announcements longer than 500 characters are truncated.
    /// </summary>
    public required string Message { get; init; }
    /// <summary>
    /// The color used to highlight the announcement.
    /// </summary>
    public required ChatAnnouncementColor Color { get; init; }
}