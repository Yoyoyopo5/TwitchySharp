using TwitchySharp.Helpers;

namespace TwitchySharp.EventSub.Enums.Events.Channel.Predictions;

/// <summary>
/// Contains static definitions for possible ended channel prediction statuses.
/// </summary>
/// <param name="Value">The string value of the prediction status.</param>
public record ChannelPredictionStatus(string Value) : ValueBackedEnum<string>(Value)
{
    public static ChannelPredictionStatus Resolved { get; } = new("resolved");
    public static ChannelPredictionStatus Canceled { get; } = new("canceled");
}
