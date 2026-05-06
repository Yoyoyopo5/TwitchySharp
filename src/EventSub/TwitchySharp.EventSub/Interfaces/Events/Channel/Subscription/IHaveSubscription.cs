using TwitchySharp.Shared.Enums;

namespace TwitchySharp.EventSub.Interfaces.Events.Channel.Subscription;

/// <summary>
/// A channel subscription.
/// </summary>
public interface IHaveSubscription
{
    /// <summary>
    /// The tier of the subscription.
    /// </summary>
    SubscriptionTier Tier { get; }
}
