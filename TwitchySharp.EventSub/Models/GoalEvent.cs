using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models;

/// <summary>
/// The base class for channel goal events.
/// </summary>
/// <remarks>
/// <see cref="EventSubSubscriptionType.GoalBegin"/>,
/// <see cref="EventSubSubscriptionType.GoalProgress"/>,
/// <see cref="EventSubSubscriptionType.GoalEnd"/>.
/// </remarks>
public record GoalEvent
{
    /// <summary>
    /// The id of the event.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) hosting the goal.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) hosting the goal.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) hosting the goal.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The type of goal.
    /// </summary>
    public required ChannelGoalType Type { get; init; }
    /// <summary>
    /// The description of the goal, if specified by the broadcaster.
    /// This can be a maximum of 40 characters.
    /// </summary>
    public required string Description { get; init; } // May be nullable.
    /// <summary>
    /// The current value of the goal.
    /// The exact meaning of this integer is determined by <see cref="Type"/>.
    /// </summary>
    public required int CurrentAmount { get; init; }
    /// <summary>
    /// The target value of the goal.
    /// </summary>
    public required int TargetAmount { get; init; }
    /// <summary>
    /// The date and time when the goal was created.
    /// </summary>
    public required DateTimeOffset StartedAt { get; init; }
}

/// <summary>
/// Contains static definitions for channel goal types.
/// </summary>
/// <param name="Value">The string value of the goal type.</param>
public record ChannelGoalType(string Value) : ValueBackedEnum<string>(Value) // Not sure if we should merge this with the one from Api lib, there seems to be subtle differences in the docs spec.
{
    /// <summary>
    /// The goal is to increase followers.
    /// </summary>
    public static ChannelGoalType Followers { get; } = new("follow"); // Api endpoint spec lists as "follower", need to investigate with live API.
    /// <summary>
    /// The goal is to increase subscriptions. 
    /// This type shows the net increase or decrease in tier points associated with the subscriptions.
    /// </summary>
    public static ChannelGoalType Subscriptions { get; } = new("subscription");
    /// <summary>
    /// The goal is to increase subscriptions. 
    /// This type shows the net increase or decrease in the number of subscriptions.
    /// </summary>
    public static ChannelGoalType SubscriptionCount { get; } = new("subscription_count");
    /// <summary>
    /// The goal is to increase subscriptions. 
    /// This type shows only the net increase in tier points associated with the subscriptions (it does not account for users that unsubscribed since the goal started).
    /// </summary>
    public static ChannelGoalType NewSubscription { get; } = new("new_subscription");
    /// <summary>
    /// The goal is to increase subscriptions. 
    /// This type shows only the net increase in the number of subscriptions (it does not account for users that unsubscribed since the goal started).
    /// </summary>
    public static ChannelGoalType NewSubscriptionCount { get; } = new("new_subscription_count");
    /// <summary>
    /// The goal is to increase the amount of Bits used on the channel.
    /// </summary>
    public static ChannelGoalType NewBits { get; } = new("new_bit");
    /// <summary>
    /// The goal is to increase the number of unique Cheerers to Cheer on the channel.
    /// </summary>
    public static ChannelGoalType NewCheerers { get; } = new("new_cheerer");
}
