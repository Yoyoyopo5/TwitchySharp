namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific mention in a chat message.
/// </summary>
public record ChannelChatMessageMention
{
    /// <summary>
    /// The id of the user that was mentioned.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The display name of the user that was mentioned.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The login (username) of the user that was mentioned.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
}
