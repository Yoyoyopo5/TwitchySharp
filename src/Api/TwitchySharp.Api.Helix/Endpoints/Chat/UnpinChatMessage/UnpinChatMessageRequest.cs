using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Chat;

/// <summary>
/// Unpins a pinned chat message from the specified broadcaster’s chat room.
/// </summary>
/// <remarks>
/// <para>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageChatMessages"/>, or
/// an app access token where the application, through a prior authorization, has <see cref="Scope.ModeratorManageChatMessages"/> and <see cref="Scope.UserBot"/> for the <see cref="ModeratorId"/> and <see cref="Scope.ChannelBot"/> for the <see cref="BroadcasterId"/>.
/// </para>
/// <para>
/// See <see href="https://dev.twitch.tv/docs/api/reference#unpin-chat-message">Unpin Chat Message</see> for more information.
/// </para>
/// </remarks>
public record UnpinChatMessageRequest : TwitchHelixRequest<UnpinChatMessageResponseContent>,
    IAuthenticatedTwitchRequest<UserSupportingPriorAuthorizationAuthenticationContext>
{
    public override HttpMethod Method { get; } = HttpMethod.Delete;
    protected override string Path { get; } = "/chat/pins";
    public UserSupportingPriorAuthorizationAuthenticationContext AuthenticationContext
    {
        get => field ?? new()
        {
            Identity = new TwitchIdentity.User(ModeratorId),
            ValidScopes = ImmutableHashSet.Create(Scope.ModeratorManageChatMessages)
        };
        init;
    }

    protected override HttpQueryParameters? QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("moderator_id", ModeratorId)
            .Add("message_id", MessageId);

    public override ValueTask<TwitchResponse<UnpinChatMessageResponseContent>> CreateResponse(HttpResponseMessage response, CancellationToken ct)
        => EmptyResponse.Create<UnpinChatMessageResponseContent>(response, ct);

    /// <summary>
    /// The user id of the broadcaster (channel) in whose chat to unpin the pinned message in.
    /// </summary>
    public required UserId BroadcasterId { get; init; }
    /// <summary>
    /// The user id of the moderator to unpin the pinned message as.
    /// </summary>
    public required UserId ModeratorId { get; init; }
    /// <summary>
    /// The id of the pinned message to unpin.
    /// </summary>
    public required MessageId MessageId { get; init; }
}
