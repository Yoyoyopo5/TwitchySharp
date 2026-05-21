namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// An emote referenced in a Bits power-up.
/// </summary>
public record PowerUpEmote
{
    /// <summary>
    /// The id of the emote.
    /// </summary>
    public required EmoteId Id { get; init; }
    /// <summary>
    /// The name of the emote.
    /// </summary>
    public required EmoteName Name { get; init; }
}
