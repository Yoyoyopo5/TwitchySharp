using TwitchySharp.EventSub.Interfaces;

namespace TwitchySharp.EventSub.Models.Events.Channel.ChannelPoints;

/// <summary>
/// Contains information about the message submitted with a specific reward redemption.
/// </summary>
public record ChannelPointsRewardRedemptionMessageV2 : IChatMessage
{
    public required string Text { get; init; }
    /// <summary>
    /// The message fragments.
    /// </summary>
    public required ChannelPointsRewardRedemptionMessageV2Fragment[] Fragments { get; init; }
    IEnumerable<IChatMessageFragment> IChatMessage.Fragments => Fragments;
}
