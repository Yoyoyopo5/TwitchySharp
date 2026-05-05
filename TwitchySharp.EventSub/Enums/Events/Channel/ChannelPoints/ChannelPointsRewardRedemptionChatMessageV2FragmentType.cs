using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Enums.Events.Channel.ChannelPoints;

/// <summary>
/// Contains static definitions for possible message fragment types in Channel Points reward redemption messages.
/// </summary>
/// <param name="Value">The string value of the message fragment type.</param>
[Wrapper<string>]
public readonly partial record struct ChannelPointsRewardRedemptionChatMessageV2FragmentType(string Value)
{
    public static ChannelPointsRewardRedemptionChatMessageV2FragmentType Text { get; } = new("text");
    public static ChannelPointsRewardRedemptionChatMessageV2FragmentType Emote { get; } = new("emote");
}