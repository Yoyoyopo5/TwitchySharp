using TwitchySharp.Helpers;

namespace TwitchySharp.EventSub.Enums.Events.Channel.Predictions;

/// <summary>
/// Contains static definitions for possible channel prediction outcome colors.
/// </summary>
/// <param name="Value">The string value of the color.</param>
public record ChannelPredictionColor(string Value) : ValueBackedEnum<string>(Value)
{
    public static ChannelPredictionColor Pink { get; } = new("pink");
    public static ChannelPredictionColor Blue { get; } = new("blue");
}
