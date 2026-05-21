namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific message thread.
/// </summary>
public record ChannelChatMessageReply
{
    /// <summary>
    /// The id of the parent message of the thread.
    /// </summary>
    public required MessageId ParentMessageId { get; init; }
    /// <summary>
    /// The text of the parent message of the thread.
    /// </summary>
    public required string ParentMessageBody { get; init; }
    /// <summary>
    /// The id of the user that sent the parent message of the thread.
    /// </summary>
    public required UserId ParentUserId { get; init; }
    /// <summary>
    /// The display name of the user that sent the parent message of the thread.
    /// </summary>
    public required UserName ParentUserName { get; init; }
    /// <summary>
    /// The login (username) of the user that sent the parent message of the thread.
    /// </summary>
    public required UserLogin ParentUserLogin { get; init; }
    /// <summary>
    /// The id of the last message of the thread.
    /// </summary>
    public required MessageId ThreadMessageId { get; init; }
    /// <summary>
    /// The id of the user that sent the last message of the thread.
    /// </summary>
    public required UserId ThreadUserId { get; init; }
    /// <summary>
    /// The display name of the user that sent the last message of the thread.
    /// </summary>
    public required UserName ThreadUserName { get; init; }
    /// <summary>
    /// The login (username) of the user that sent the last message of the thread.
    /// </summary>
    public required UserLogin ThreadUserLogin { get; init; }
}
