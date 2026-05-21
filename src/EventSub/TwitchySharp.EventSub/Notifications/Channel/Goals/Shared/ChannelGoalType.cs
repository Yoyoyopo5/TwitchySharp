using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains static definitions for channel goal types.
/// </summary>
/// <param name="Value">The string value of the goal type.</param>
[Wrapper<string>]
public readonly partial record struct ChannelGoalType(string Value) // Not sure if we should merge this with the one from Api lib, there seems to be subtle differences in the docs spec.
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
