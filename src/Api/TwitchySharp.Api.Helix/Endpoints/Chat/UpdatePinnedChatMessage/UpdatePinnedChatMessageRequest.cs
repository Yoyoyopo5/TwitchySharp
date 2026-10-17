using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Chat;

/// <summary>
/// Updates the duration of an existing pinned chat message.
/// </summary>
/// <remarks>
/// <para>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageChatMessages"/>, or
/// an app access token where the application, through a prior authorization, has <see cref="Scope.ModeratorManageChatMessages"/> and <see cref="Scope.UserBot"/> for the <see cref="ModeratorId"/> and <see cref="Scope.ChannelBot"/> for the <see cref="BroadcasterId"/>.
/// </para>
/// <para>
/// See <see href="https://dev.twitch.tv/docs/api/reference#update-pinned-chat-message">Update Pinned Chat Message</see> for more information.
/// </para>
/// </remarks>
public record UpdatePinnedChatMessageRequest : TwitchHelixRequest<UpdatePinnedChatMessageResponseContent>,
    IAuthenticatedTwitchRequest<UserSupportingPriorAuthorizationAuthenticationContext>
{
    public override HttpMethod Method { get; } = HttpMethod.Patch;
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
            .Add("message_id", MessageId)
            .Add("duration_seconds", Duration is null
                ? null
                : ((int)double.Floor(Duration.Value.TotalSeconds)).ToString());

    public override ValueTask<TwitchResponse<UpdatePinnedChatMessageResponseContent>> CreateResponse(HttpResponseMessage response, CancellationToken ct)
        => EmptyResponse.Create<UpdatePinnedChatMessageResponseContent>(response, ct);

    /// <summary>
    /// The user id of the broadcaster (channel) in whose chat to update the pinned message in.
    /// </summary>
    public required UserId BroadcasterId { get; init; }
    /// <summary>
    /// The user id of the moderator to update the pinned message as.
    /// </summary>
    public required UserId ModeratorId { get; init; }
    /// <summary>
    /// The id of the pinned message to update.
    /// </summary>
    public required MessageId MessageId { get; init; }
    /// <summary>
    /// The new duration to pin the message for.
    /// </summary>
    /// <remarks>
    /// This can be between 30 seconds to 30 minutes.
    /// If <see langword="null"/>, the message will be pinned until the stream ends.
    /// </remarks>
    public TimeSpan? Duration { get; init; }
}
