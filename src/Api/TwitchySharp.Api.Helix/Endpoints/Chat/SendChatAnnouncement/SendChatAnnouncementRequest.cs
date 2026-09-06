using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Sends an announcement to the broadcaster's chat room.
/// </summary>
/// <remarks>
/// <para>
/// Requires a user access token with <see cref="Scope.ModeratorManageAnnouncements"/>, or
/// an app access token where the application, through a prior authorization, has <see cref="Scope.ModeratorManageAnnouncements"/> and <see cref="Scope.UserBot"/> for the <see cref="ModeratorId"/>,
/// and <see cref="Scope.ChannelBot"/> for the <see cref="BroadcasterId"/>.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#send-chat-announcement">Send Chat Announcement</see> for more information.
/// </remarks>
public record SendChatAnnouncementRequest
    : TwitchHelixRequest<SendChatAnnouncementResponseContent>,
    IAuthenticatedTwitchRequest<UserSupportingPriorAuthorizationAuthenticationContext>
{
    protected override string Path => "/chat/announcements";
    public override HttpMethod Method => HttpMethod.Post;
    private UserSupportingPriorAuthorizationAuthenticationContext DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.User(ModeratorId),
        ValidScopes = ImmutableHashSet.Create(Scope.ModeratorManageAnnouncements)
    };
    public UserSupportingPriorAuthorizationAuthenticationContext AuthenticationContext
    {
        get => field ?? DefaultAuthenticationContext;
        init;
    }
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("moderator_id", ModeratorId);
    public override object? ContentObject => Announcement;

    /// <summary>
    /// The user id of the broadcaster (channel) whose chat room you want to send the announcement to.
    /// </summary>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// </summary>
    /// <remarks>
    /// This must be the same user who created the access token used in the request.
    /// Requires <see cref="Scope.ModeratorManageAnnouncements"/>.
    /// </remarks>
    public required UserId ModeratorId { get; init; }

    /// <summary>
    /// The announcement to send.
    /// </summary>
    public required SendChatAnnouncementRequestData Announcement { get; init; }

    public override Func<Stream, CancellationToken, ValueTask<SendChatAnnouncementResponseContent>>? ConvertResponseContent { get; init; }
        = (_, _) => ValueTask.FromResult(new SendChatAnnouncementResponseContent());
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
