namespace TwitchySharp.EventSub.Models.Events.Automod.Message;

/// <summary>
/// Contains information about the message that was caught by Automod.
/// </summary>
public record AutomodCaughtChatMessage
{
    /// <summary>
    /// The full text of the message.
    /// </summary>
    public required string Text { get; init; }
    /// <summary>
    /// The segments of the message that triggered the Automod.
    /// </summary>
    public required AutomodCaughtMessageFragment[] Fragments { get; init; }
}
