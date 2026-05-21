using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains static definitions for possible ended channel prediction statuses.
/// </summary>
/// <param name="Value">The string value of the prediction status.</param>
[Wrapper<string>]
public readonly partial record struct ChannelPredictionStatus(string Value)
{
    public static ChannelPredictionStatus Resolved { get; } = new("resolved");
    public static ChannelPredictionStatus Canceled { get; } = new("canceled");
}
