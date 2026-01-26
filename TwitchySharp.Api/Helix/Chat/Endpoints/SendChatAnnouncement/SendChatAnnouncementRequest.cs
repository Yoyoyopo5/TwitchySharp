using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Sends an announcement to the broadcaster's chat room.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageAnnouncements"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#send-chat-announcement">Send Chat Announcement</see> for more information.
/// </remarks>
public record SendChatAnnouncementRequest
    : TwitchHelixRequest<SendChatAnnouncementResponse>
{
    protected override string Path => "/chat/announcements";
    public override HttpMethod Method => HttpMethod.Post;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(ModeratorId);
    public override IEnumerable<Scope> ValidScopes => [ Scope.ModeratorManageAnnouncements ];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("moderator_id", ModeratorId);
    public override object? ContentObject => Announcement;

    /// <summary>
    /// The user id of the broadcaster (channel) whose chat room you want to send the announcement to.
    /// </summary>
    public required UserId BroadcasterId { get; set; }

    /// <summary>
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// </summary>
    /// <remarks>
    /// This must be the same user who created the access token used in the request.
    /// Requires <see cref="Scope.ModeratorManageAnnouncements"/>.
    /// </remarks>
    public required UserId ModeratorId { get; set; }

    /// <summary>
    /// The announcement to send.
    /// </summary>
    public required SendChatAnnouncementRequestData Announcement { get; set; }
}

/// <summary>
/// Contains data used to create a chat announcement.
/// </summary>
public record SendChatAnnouncementRequestData
{
    /// <summary>
    /// The announcement to make in the broadcaster's chat room.
    /// Announcements are limited to a maximum of 500 characters; announcements longer than 500 characters are truncated.
    /// </summary>
    public required string Message { get; init; }

    /// <summary>
    /// The color used to highlight the announcement.
    /// </summary>
    public required ChatAnnouncementColor Color { get; init; }
}
