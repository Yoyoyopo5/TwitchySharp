using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers.JsonConverters.Enums;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Sends an announcement to the broadcaster’s chat room.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#send-chat-announcement">send chat announcement</see> for more information.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageAnnouncements"/>.
/// </summary>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorManageAnnouncements"/.></param>
/// <param name="broadcasterId">The user id of the broadcaster whose chat room you want to send the announcement to.</param>
/// <param name="moderatorId">The user id of the broadcaster or a moderator of the broadcaster's channel. This must be the same user who created the <paramref name="accessToken"/>.</param>
/// <param name="announcement">The announcement to send.</param>
public class SendChatAnnouncementRequest(string clientId, string accessToken, string broadcasterId, string moderatorId, SendChatAnnouncementRequestData announcement)
    : HelixApiRequest<SendChatAnnouncementResponse, SendChatAnnouncementRequestData>(
        "/chat/announcements" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("moderator_id", moderatorId),
        clientId,
        accessToken,
        announcement
        );

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
    [JsonConverter(typeof(SnakeCaseLowerJsonStringEnumConverter<ChatAnnouncementColor>))] // For lower-case serialization
    public required ChatAnnouncementColor Color { get; init; }
}

/// <summary>
/// Available announcement colors.
/// See <see cref="SendChatAnnouncementRequest"/>.
/// </summary>
public enum ChatAnnouncementColor
{
    /// <summary>
    /// Uses channel's accent color.
    /// </summary>
    Primary,
    Blue,
    Green,
    Orange,
    Purple
}
