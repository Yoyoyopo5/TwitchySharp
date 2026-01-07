namespace TwitchySharp.EventSub.Models.Events.Channel.Subscription;

/// <summary>
/// Contains information about a specific emote used in a resubscription message.
/// </summary>
public record ResubscriptionMessageEmote
{
    /// <summary>
    /// The id of the emote.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The character index at which the emote starts in the message.
    /// </summary>
    public required int Begin { get; init; }
    /// <summary>
    /// The character index at which the emote ends in the message.
    /// </summary>
    public required int End { get; init; }
}
