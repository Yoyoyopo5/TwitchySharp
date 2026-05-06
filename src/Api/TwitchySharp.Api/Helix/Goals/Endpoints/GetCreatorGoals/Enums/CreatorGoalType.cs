using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api.Helix.Goals;

/// <summary>
/// Possible goal types.
/// </summary>
[Wrapper<string>]
public readonly partial record struct CreatorGoalType(string Value)
{
    /// <summary>
    /// The goal is to increase followers.
    /// </summary>
    public static CreatorGoalType Follower { get; } = new("follower");
    /// <summary>
    /// The goal is to increase subscription points.
    /// Higher tier subscriptions contribute more to this type of goal.
    /// </summary>
    public static CreatorGoalType Subscription { get; } = new("subscription");
    /// <summary>
    /// The goal is to increase subscriptions.
    /// This type shows the net increase or decrease in the number of subscriptions.
    /// </summary>
    public static CreatorGoalType SubscriptionCount { get; } = new("subscription_count");
    /// <summary>
    /// The goal is to increase subscriptions.
    /// This type shows only the net increase in tier points associated with new subscriptions (from users that have not subscribed before).
    /// Higher tier subscriptions contribute more to this type of goal.
    /// </summary>
    public static CreatorGoalType NewSubscription { get; } = new("new_subscription");
    /// <summary>
    /// The goal is to increase subscriptions.
    /// This type shows only the net increase in subscription count associated with new subscriptions (from users that have not subscribed before).
    /// </summary>
    public static CreatorGoalType NewSubscriptionCount { get; } = new("new_subscription_count");
}
