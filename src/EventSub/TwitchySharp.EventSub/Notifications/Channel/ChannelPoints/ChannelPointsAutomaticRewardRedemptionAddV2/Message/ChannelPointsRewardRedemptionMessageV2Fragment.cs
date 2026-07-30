namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific fragment of a message that was submitted with a channel points reward redemption.
/// </summary>
public record ChannelPointsRewardRedemptionMessageV2Fragment
{
    public required string Text { get; init; }
    /// <summary>
    /// The fragment type.
    /// </summary>
    public required ChannelPointsRewardRedemptionChatMessageV2FragmentType Type { get; init; }
    /// <summary>
    /// The emote associated with the fragment.
    /// This is <see langword="null"/> unless <see cref="Type"/> is <see cref="ChannelPointsRewardRedemptionChatMessageV2FragmentType.Emote"/>
    /// </summary>
    public ChannelPointsRewardRedemptionMessageV2Emote? Emote { get; init; }
}
