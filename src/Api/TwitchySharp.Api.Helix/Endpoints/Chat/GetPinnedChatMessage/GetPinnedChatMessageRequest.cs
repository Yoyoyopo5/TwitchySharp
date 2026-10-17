using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Chat;

/// <summary>
/// Gets the currently pinned message for the specified broadcaster’s chat room.
/// </summary>
/// <remarks>
/// <para>
/// Only one mod-pinned message can be active per channel at a time.
/// </para>
/// <para>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadChatMessages"/> or <see cref="Scope.ModeratorManageChatMessages"/>, or
/// an app access token where the application, through a prior authorization, has <see cref="Scope.ModeratorReadChatMessages"/> or <see cref="Scope.ModeratorManageChatMessages"/> and <see cref="Scope.UserBot"/> for the <see cref="ModeratorId"/> and <see cref="Scope.ChannelBot"/> for the <see cref="BroadcasterId"/>.
/// </para>
/// <para>
/// See <see href="https://dev.twitch.tv/docs/api/reference#get-pinned-chat-message">Get Pinned Chat Message</see> for more information.
/// </para>
/// </remarks>
public record GetPinnedChatMessageRequest : TwitchHelixRequest<GetPinnedChatMessageResponseContent>,
    IAuthenticatedTwitchRequest<UserSupportingPriorAuthorizationAuthenticationContext>
{
    public override HttpMethod Method { get; } = HttpMethod.Get;
    protected override string Path { get; } = "/chat/pins";
    public UserSupportingPriorAuthorizationAuthenticationContext AuthenticationContext
    {
        get => field ?? new()
        {
            Identity = new TwitchIdentity.User(ModeratorId),
            ValidScopes = ImmutableHashSet.Create(Scope.ModeratorReadChatMessages, Scope.ModeratorManageChatMessages)
        };
        init;
    }

    protected override HttpQueryParameters? QueryParameters =>
        new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("moderator_id", ModeratorId);

    /// <summary>
    /// The user id of the broadcaster (channel) from whose chat to get the pinned message from.
    /// </summary>
    public required UserId BroadcasterId { get; init; }
    /// <summary>
    /// The user id of the moderator to read chat as.
    /// </summary>
    /// <remarks>
    /// This must be the broadcaster or a moderator of the broadcaster's chat.
    /// </remarks>
    public required UserId ModeratorId { get; init; }
}
