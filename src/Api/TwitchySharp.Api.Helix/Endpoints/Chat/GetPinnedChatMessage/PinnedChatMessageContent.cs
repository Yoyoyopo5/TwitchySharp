namespace TwitchySharp.Api.Helix.Chat;

/// <summary>
/// A pinned chat messsage.
/// </summary>
public record PinnedChatMessageContent
{
    /// <summary>
    /// The plain text of the message.
    /// </summary>
    public required string Text { get; init; }
    /// <summary>
    /// An ordered list of message fragments.
    /// </summary>
    public PinnedChatMessageFragment[] Fragments { get; init; } = [];
}
