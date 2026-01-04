using TwitchySharp.EventSub.Enums.Events.Channel.ChannelPoints;
using TwitchySharp.EventSub.Interfaces;
using TwitchySharp.Helpers;

namespace TwitchySharp.EventSub.Models.Events.Channel.ChannelPoints;

/// <summary>
/// Contains information about a specific fragment of a message that was submitted with a channel points reward redemption.
/// </summary>
public record ChannelPointsRewardRedemptionMessageV2Fragment : IChatMessageFragment
{
    public required string Text { get; init; }
    /// <summary>
    /// The fragment type.
    /// </summary>
    public required ChannelPointsRewardRedemptionChatMessageV2FragmentType Type { get; init; }
    ValueBackedEnum<string> IChatMessageFragment.Type => Type;
    /// <summary>
    /// The emote associated with the fragment.
    /// This is <see langword="null"/> unless <see cref="Type"/> is <see cref="ChannelPointsRewardRedemptionChatMessageV2FragmentType.Emote"/>
    /// </summary>
    public ChannelPointsRewardRedemptionMessageV2Emote? Emote { get; init; }
    IChatMessageEmote? IChatMessageFragment.Emote => Emote;
    /// <summary>
    /// Not supported for this event type.
    /// Set to <see langword="null"/>.
    /// </summary>
    IChatMessageCheermote? IChatMessageFragment.Cheermote => null;
}
