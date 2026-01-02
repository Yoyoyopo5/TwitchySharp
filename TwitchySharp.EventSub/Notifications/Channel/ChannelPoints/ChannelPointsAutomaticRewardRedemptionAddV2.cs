using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Channel;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelPointsAutomaticRewardRedemptionAddV2"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchannel_points_automatic_reward_redemptionadd-v2">Channel Points Automatic Reward Redemption Add V2</see> for more information.
/// </remarks>
public record ChannelPointsAutomaticRewardRedemptionAddV2Notification : EventSubNotification<ChannelPointsAutomaticRewardRedemptionAddV2Event, ChannelPointsAutomaticRewardRedemptionAddV2Condition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelPointsAutomaticRewardRedemptionAddV2"/>.
/// </summary>
public record ChannelPointsAutomaticRewardRedemptionAddV2Condition
{
    /// <summary>
    /// The user id of the broadcaster (channel) to get Channel Points Automatic Reward Redemption Add V2 notifications for.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
}
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelPointsAutomaticRewardRedemptionAddV2"/> event.
/// </summary>
public record ChannelPointsAutomaticRewardRedemptionAddV2Event
{
    /// <summary>
    /// The user id of the broadcaster (channel) whose chat the reward was redeemed in.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) whose chat the reward was redeemed in.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) whose chat the reward was redeemed in.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The id of the user that redeemed the reward.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that redeemed the reward.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that redeemed the reward.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The id of the redemption.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The reward that was redeemed.
    /// </summary>
    public required ChannelPointsAutomaticRewardRedemptionV2Reward Reward { get; init; }
    /// <summary>
    /// The chat message that was submitted with the redemption.
    /// </summary>
    public ChannelPointsRewardRedemptionMessageV2? Message { get; init; }
    /// <summary>
    /// The date and time when the reward was redeemed.
    /// </summary>
    public required DateTimeOffset RedeemedAt { get; init; }
}

/// <summary>
/// Contains information about a specific automatic (built-in) channel points reward that was redeemed.
/// </summary>
public record ChannelPointsAutomaticRewardRedemptionV2Reward
{
    /// <summary>
    /// The type of automatic (built-in) reward that was redeemed.
    /// </summary>
    public required ChannelPointsAutomaticRewardV2Type Type { get; init; }
    /// <summary>
    /// The number of channel points used to redeem the reward.
    /// </summary>
    public required int ChannelPoints { get; init; }
    /// <summary>
    /// The emote associated with the reward redemption, if any.
    /// </summary>
    public ChannelPointsAutomaticRewardV2UnlockedEmote? Emote { get; init; }
}

/// <summary>
/// Contains information about a specific emote unlocked from an automatic (built-in) channel points reward.
/// </summary>
public record ChannelPointsAutomaticRewardV2UnlockedEmote
{
    /// <summary>
    /// The id of the emote.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The name of the emote.
    /// </summary>
    public required string Name { get; init; }
}

/// <summary>
/// Contains static definitions for possible automatic (built-in) channel points reward types.
/// </summary>
/// <param name="Value">The string value of the reward type.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<ChannelPointsAutomaticRewardV2Type, string>))]
public record ChannelPointsAutomaticRewardV2Type(string Value) : ValueBackedEnum<string>(Value)
{
    public static ChannelPointsAutomaticRewardV2Type SingleMessageBypassSubMode { get; } = new("single_message_bypass_sub_mode");
    public static ChannelPointsAutomaticRewardV2Type SendHighlightedMessage { get; } = new("send_highlighted_message");
    public static ChannelPointsAutomaticRewardV2Type RandomSubEmoteUnlock { get; } = new("random_sub_emote_unlock");
    public static ChannelPointsAutomaticRewardV2Type ChosenSubEmoteUnlock { get; } = new("chosen_sub_emote_unlock");
    public static ChannelPointsAutomaticRewardV2Type ChosenModifiedSubEmoteUnlock { get; } = new("chosen_modified_sub_emote_unlock");
}

/// <summary>
/// Contains information about the message submitted with a specific reward redemption.
/// </summary>
public record ChannelPointsRewardRedemptionMessageV2
{
    /// <summary>
    /// The message text.
    /// </summary>
    public required string Text { get; init; }
    /// <summary>
    /// The message fragments.
    /// </summary>
    public required ChannelPointsRewardRedemptionMessageV2Fragment[] Fragments { get; init; }
}

/// <summary>
/// Contains information about a specific fragment of a message that was submitted with a channel points reward redemption.
/// </summary>
public record ChannelPointsRewardRedemptionMessageV2Fragment
{
    /// <summary>
    /// The text of the fragment.
    /// </summary>
    public required string Text { get; init; }
    /// <summary>
    /// The fragment type.
    /// </summary>
    public required ChannelChatMessageFragmentType Type { get; init; }
    /// <summary>
    /// The emote associated with the fragment.
    /// This is <see langword="null"/> unless <see cref="Type"/> is <see cref="ChannelChatMessageFragmentType.Emote"/>
    /// </summary>
    public ChannelPointsRewardRedemptionMessageV2Emote? Emote { get; init; }
}

/// <summary>
/// Contains information about a specific emote used in a reward redemption message.
/// </summary>
public record ChannelPointsRewardRedemptionMessageV2Emote
{
    /// <summary>
    /// The id of the emote.
    /// </summary>
    public required string Id { get; init; }
}
