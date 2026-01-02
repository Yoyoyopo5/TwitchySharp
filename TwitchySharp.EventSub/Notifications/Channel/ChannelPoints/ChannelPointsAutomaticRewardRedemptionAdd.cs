using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Channel.ChannelPoints;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelPointsAutomaticRewardRedemptionAdd"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchannel_points_automatic_reward_redemptionadd">Channel Points Automatic Reward Redemption Add</see> for more information.
/// </remarks>
public record ChannelPointsAutomaticRewardRedemptionAddNotification : EventSubNotification<ChannelPointsAutomaticRewardRedemptionAddEvent, ChannelPointsAutomaticRewardRedemptionAddCondition>
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelPointsAutomaticRewardRedemptionAdd"/>.
/// </summary>
public record ChannelPointsAutomaticRewardRedemptionAddCondition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelPointsAutomaticRewardRedemptionAdd"/> event.
/// </summary>
public record ChannelPointsAutomaticRewardRedemptionAddEvent
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
    /// The message that was sent with the redemption.
    /// This is <see langword="null"/> if the reward does not require user input.
    /// </summary>
    public ChannelPointsRewardRedemptionMessage? Message { get; init; } // Almost certain this can be null
    /// <summary>
    /// The message that was sent with the redemption, in string format.
    /// This is <see langword="null"/> if the reward does not require user input.
    /// </summary>
    public string? UserInput { get; init; }
    /// <summary>
    /// The date and time when the reward was redeemed.
    /// </summary>
    public required DateTimeOffset RedeemedAt { get; init; }
}

/// <summary>
/// Contains information about a specific automatic (built-in) channel points reward that was redeemed.
/// </summary>
public record ChannelPointsAutomaticRewardRedemptionReward
{
    /// <summary>
    /// The type of reward that was redeemed.
    /// </summary>
    public required ChannelPointsAutomaticRewardType Type { get; init; }
    /// <summary>
    /// The cost of the reward, in channel points.
    /// </summary>
    public required int Cost { get; init; }
    /// <summary>
    /// The emote associated with the reward redemption, if any.
    /// </summary>
    public ChannelPointsAutomaticRewardUnlockedEmote? UnlockedEmote { get; init; } // Need to see if this is populated on gigantify.
}

/// <summary>
/// Contains static definitions for possible automatic (built-in) channel points reward types.
/// </summary>
/// <param name="Value">The string value of the reward type.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<ChannelPointsAutomaticRewardType, string>))]
public record ChannelPointsAutomaticRewardType(string Value) : ValueBackedEnum<string>(Value)
{
    public static ChannelPointsAutomaticRewardType SingleMessageBypassSubMode { get; } = new("single_message_bypass_sub_mode");
    public static ChannelPointsAutomaticRewardType SendHighlightedMessage { get; } = new("send_highlighted_message");
    public static ChannelPointsAutomaticRewardType RandomSubEmoteUnlock { get; } = new("random_sub_emote_unlock");
    public static ChannelPointsAutomaticRewardType ChosenSubEmoteUnlock { get; } = new("chosen_sub_emote_unlock");
    public static ChannelPointsAutomaticRewardType ChosenModifiedSubEmoteUnlock { get; } = new("chosen_modified_sub_emote_unlock");
    public static ChannelPointsAutomaticRewardType MessageEffect { get; } = new("message_effect");
    public static ChannelPointsAutomaticRewardType GigantifyAnEmote { get; } = new("gigantify_an_emote");
    public static ChannelPointsAutomaticRewardType Celebration { get; } = new("celebration");
}

/// <summary>
/// Contains information about a specific emote unlocked from an automatic (built-in) channel points reward.
/// </summary>
public record ChannelPointsAutomaticRewardUnlockedEmote
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
/// Contains information about a message submitted with a channel points reward redemption.
/// </summary>
public record ChannelPointsRewardRedemptionMessage
{
    /// <summary>
    /// The text of the chat message.
    /// </summary>
    public required string Text { get; init; }
    /// <summary>
    /// The emotes included in the chat message.
    /// </summary>
    public required ChannelPointsRewardRedemptionMessageEmote[] Emotes { get; init; }
}

/// <summary>
/// Contains information about a specific emote in a channel points reward redemption chat message.
/// </summary>
public record ChannelPointsRewardRedemptionMessageEmote // Really need to merge some of these classes with others, but the spec is such a mess I'm kind of afraid to. Interfaces may grant some degree of interop without harming flexibility too much in case spec changes.
{
    /// <summary>
    /// The id of the emote.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The character index of the chat message where the emote begins.
    /// </summary>
    public required int Begin { get; init; }
    /// <summary>
    /// The character index of the chat message where the emote ends.
    /// </summary>
    public required int End { get; init; }
}
