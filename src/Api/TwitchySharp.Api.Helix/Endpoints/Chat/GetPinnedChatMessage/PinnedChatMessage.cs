namespace TwitchySharp.Api.Helix.Chat;

/// <summary>
/// A specific pinned chat message.
/// </summary>
public record PinnedChatMessage
{
    /// <summary>
    /// The id of the pinned message.
    /// </summary>
    public required MessageId MessageId { get; init; }
    /// <summary>
    /// The broadcaster's user identifier.
    /// </summary>
    /// <remarks>Required; assigned during object initialization.</remarks>
    public required UserId BroadcasterId { get; init; }
    /// <summary>
    /// The id of the user who sent the pinned message.
    /// </summary>
    public required UserId SenderUserId { get; init; }
    /// <summary>
    /// The login of the user who sent the pinned message.
    /// </summary>
    public required UserLogin SenderUserLogin { get; init; }
    /// <summary>
    /// The display name of the user who sent the pinned message.
    /// </summary>
    public required UserName SenderUserName { get; init; }
    /// <summary>
    /// The id of the user who pinned the message.
    /// </summary>
    public required UserId PinnedByUserId { get; init; }
    /// <summary>
    /// The login of the user who pinned the message.
    /// </summary>
    public required UserLogin PinnedByUserLogin { get; init; }
    /// <summary>
    /// The display name of the user who pinned the message.
    /// </summary>
    public required UserName PinnedByUserName { get; init; }
    /// <summary>
    /// The pinned message.
    /// </summary>
    public required PinnedChatMessageContent Message { get; init; }
    /// <summary>
    /// The time when the message was pinned.
    /// </summary>
    public required DateTimeOffset StartsAt { get; init; }
    /// <summary>
    /// The time when the pin will expire.
    /// </summary>
    /// <remarks>
    /// This is <see langword="null"/> if the pin expires at the end of stream.
    /// </remarks>
    public DateTimeOffset? EndsAt { get; init; }
    /// <summary>
    /// The time of the last update.
    /// </summary>
    public required DateTimeOffset UpdatedAt { get; init; }
}
