namespace TwitchySharp.EventSub.Models.Events.Automod.Message;

/// <summary>
/// Contains information about a specific Automod hold, including the Automod settings that triggered the hold.
/// </summary>
public record AutomodHold
{
    /// <summary>
    /// The Automod category that triggered the hold.
    /// </summary>
    public required string Category { get; init; }
    /// <summary>
    /// The level of severity of the held message.
    /// </summary>
    public required int Level { get; init; }
    /// <summary>
    /// The bounds of the text that caused the message to be caught.
    /// </summary>
    public required AutomodHoldBoundary[] Boundaries { get; init; }
}
