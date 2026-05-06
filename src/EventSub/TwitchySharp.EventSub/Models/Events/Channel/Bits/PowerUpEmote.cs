namespace TwitchySharp.EventSub.Models.Events.Channel.Bits;

/// <summary>
/// An emote referenced in a Bits power-up.
/// </summary>
public record PowerUpEmote
{
    /// <summary>
    /// The id of the emote.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The name of the emote.
    /// </summary>
    public required string Name { get; init; }
}
