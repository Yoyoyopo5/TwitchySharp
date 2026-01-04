using TwitchySharp.Helpers;

namespace TwitchySharp.EventSub.Enums.Events.Channel.Bits;

/// <summary>
/// Contains static definitions for possible Bits power up types.
/// </summary>
public record BitsPowerUpType(string Value) : ValueBackedEnum<string>(Value)
{
    public static BitsPowerUpType MessageEffect { get; } = new("message_effect");
    public static BitsPowerUpType Celebration { get; } = new("celebration");
    public static BitsPowerUpType GigantifyAnEmote { get; } = new("gigantify_an_emote");
}
