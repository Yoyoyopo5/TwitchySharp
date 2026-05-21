using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains static definitions for possible Bits power up types.
/// </summary>
[Wrapper<string>]
public readonly partial record struct BitsPowerUpType(string Value)
{
    public static BitsPowerUpType MessageEffect { get; } = new("message_effect");
    public static BitsPowerUpType Celebration { get; } = new("celebration");
    public static BitsPowerUpType GigantifyAnEmote { get; } = new("gigantify_an_emote");
}
