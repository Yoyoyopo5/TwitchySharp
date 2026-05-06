namespace TwitchySharp.EventSub.Models.Events.Channel.Chat;

/// <summary>
/// Contains information about a specific message thread.
/// </summary>
public record ChannelChatMessageReply
{
    /// <summary>
    /// The id of the parent message of the thread.
    /// </summary>
    public required string ParentMessageId { get; init; }
    /// <summary>
    /// The text of the parent message of the thread.
    /// </summary>
    public required string ParentMessageBody { get; init; }
    /// <summary>
    /// The id of the user that sent the parent message of the thread.
    /// </summary>
    public required string ParentUserId { get; init; }
    /// <summary>
    /// The display name of the user that sent the parent message of the thread.
    /// </summary>
    public required string ParentUserName { get; init; }
    /// <summary>
    /// The login (username) of the user that sent the parent message of the thread.
    /// </summary>
    public required string ParentUserLogin { get; init; }
    /// <summary>
    /// The id of the last message of the thread.
    /// </summary>
    public required string ThreadMessageId { get; init; }
    /// <summary>
    /// The id of the user that sent the last message of the thread.
    /// </summary>
    public required string ThreadUserId { get; init; }
    /// <summary>
    /// The display name of the user that sent the last message of the thread.
    /// </summary>
    public required string ThreadUserName { get; init; }
    /// <summary>
    /// The login (username) of the user that sent the last message of the thread.
    /// </summary>
    public required string ThreadUserLogin { get; init; }
}
