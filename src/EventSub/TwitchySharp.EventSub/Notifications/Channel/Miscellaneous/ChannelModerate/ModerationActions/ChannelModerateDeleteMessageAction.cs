namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.DeleteMessage"/> or <see cref="ChannelModerateActionType.SharedChatDeleteMessage"/> action.
/// </summary>
public record ChannelModerateDeleteMessageAction
{
    /// <summary>
    /// The id of user whose message is being deleted.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of user whose message is being deleted.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of user whose message is being deleted.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The id of the message that was deleted.
    /// </summary>
    public required MessageId MessageId { get; init; }
    /// <summary>
    /// The message that was deleted, in <see langword="string"/> format.
    /// </summary>
    public required string MessageBody { get; init; }
}
