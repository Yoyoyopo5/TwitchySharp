using System.Collections.Generic;
using TwitchySharp.Shared.EventSub.Constants;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// A viewer has redeemed a custom channel points reward on the specified channel.
/// </summary>
/// <param name="BroadcasterUserId">The broadcaster user ID for the channel you want to receive channel points custom reward redemption add notifications for.</param>
/// <param name="RewardId">Optional. Specify a reward id to only receive notifications for a specific reward.</param>
public sealed record ChannelPointsCustomRewardRedemptionAdd(UserId BroadcasterUserId, string? RewardId = null)
    : IEventSubSubscriptionType
{
    public EventSubSubscriptionTypeName Name => new(EventSubSubscriptionTypeNames.CHANNEL_POINTS_AUTOMATIC_REWARD_REDEMPTION_ADD);
    public EventSubSubscriptionTypeVersion Version => new(EventSubSubscriptionTypeVersions.V1);

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId)
            .Set("reward_id", RewardId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
