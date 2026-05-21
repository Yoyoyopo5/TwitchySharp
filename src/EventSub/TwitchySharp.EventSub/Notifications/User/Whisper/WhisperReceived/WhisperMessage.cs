namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific message sent using Twitch Whispers.
/// </summary>
public record WhisperMessage
{
    /// <summary>
    /// The text of the message.
    /// </summary>
    public required string Text { get; init; }
}
