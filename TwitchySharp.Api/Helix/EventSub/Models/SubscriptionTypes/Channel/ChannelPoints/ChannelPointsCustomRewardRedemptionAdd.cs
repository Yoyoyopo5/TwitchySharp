using System.Collections.Generic;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;
using TwitchySharp.Shared.EventSub;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// A viewer has redeemed a custom channel points reward on the specified channel.
/// </summary>
/// <param name="BroadcasterUserId">The broadcaster user ID for the channel you want to receive channel points custom reward redemption add notifications for.</param>
/// <param name="RewardId">Optional. Specify a reward id to only receive notifications for a specific reward.</param>
public sealed record ChannelPointsCustomRewardRedemptionAdd(UserId BroadcasterUserId, string? RewardId = null)
    : IEventSubSubscriptionType
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelPointsCustomRewardRedemptionAdd;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId)
            .Set(new ConditionKey("reward_id"), RewardId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
