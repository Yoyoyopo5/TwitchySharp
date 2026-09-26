using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Removes a single chat message or all chat messages from the broadcaster's chat room.
/// </summary>
/// <remarks>
/// <para>
/// Requires a user access token with <see cref="Scope.ModeratorManageChatMessages"/>, or
/// an app access token where the application, through a prior authorization, has <see cref="Scope.ModeratorManageChatMessages"/> for the <see cref="ModeratorId"/>.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#delete-chat-messages">Delete Chat Messages</see> for more information.
/// </remarks>
public record DeleteChatMessagesRequest
    : TwitchHelixRequest<DeleteChatMessagesResponseContent>,
    IAuthenticatedTwitchRequest<UserSupportingPriorAuthorizationAuthenticationContext>
{
    protected override string Path => "/moderation/chat";
    public override HttpMethod Method => HttpMethod.Delete;
    private UserSupportingPriorAuthorizationAuthenticationContext DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.User(ModeratorId),
        ValidScopes = ImmutableHashSet.Create(Scope.ModeratorManageChatMessages)
    };
    public UserSupportingPriorAuthorizationAuthenticationContext AuthenticationContext
    {
        get => field ?? DefaultAuthenticationContext;
        init;
    }
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("moderator_id", ModeratorId)
            .Add("message_id", MessageId);

    /// <summary>
    /// The user id of the broadcaster (channel) whose chat to delete a chat message from.
    /// </summary>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId ModeratorId { get; init; }

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
    public MessageId? MessageId { get; init; }

    public override Func<Stream, CancellationToken, ValueTask<DeleteChatMessagesResponseContent>>? ConvertResponseContent { get; init; }
        = (_, _) => ValueTask.FromResult(new DeleteChatMessagesResponseContent());
}
