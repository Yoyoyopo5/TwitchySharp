using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.EventSub.Enums.Events.Channel.ChannelPoints;

/// <summary>
/// Contains static definitions for possible message fragment types in Channel Points reward redemption messages.
/// </summary>
/// <param name="Value">The string value of the message fragment type.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<ChannelPointsRewardRedemptionChatMessageV2FragmentType, string>))]
public record ChannelPointsRewardRedemptionChatMessageV2FragmentType(string Value) : ValueBackedEnum<string>(Value)
{
    public static ChannelPointsRewardRedemptionChatMessageV2FragmentType Text { get; } = new("text");
    public static ChannelPointsRewardRedemptionChatMessageV2FragmentType Emote { get; } = new("emote");
}