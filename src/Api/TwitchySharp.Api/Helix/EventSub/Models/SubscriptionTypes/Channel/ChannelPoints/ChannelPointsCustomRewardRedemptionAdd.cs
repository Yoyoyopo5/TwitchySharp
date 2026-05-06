using System.Collections.Generic;
using System.Collections.Immutable;
using TwitchySharp.Shared.EventSub;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// A viewer has redeemed a custom channel points reward on the specified channel.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadRedemptions"/> or <see cref="Scope.ChannelManageRedemptions"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The broadcaster user ID for the channel you want to receive channel points custom reward redemption add notifications for.</param>
/// <param name="RewardId">Optional. Specify a reward id to only receive notifications for a specific reward.</param>
public sealed record ChannelPointsCustomRewardRedemptionAdd(UserId BroadcasterUserId, string? RewardId = null)
    : IUserAuthorizedSubscriptionType
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelPointsCustomRewardRedemptionAdd;
    public ConditionKey AuthorizingUserConditionKey => new ConditionKey("broadcaster_user_id");
    public IReadOnlySet<Scope> ValidScopes => ImmutableHashSet.Create(Scope.ChannelReadRedemptions, Scope.ChannelManageRedemptions);

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId)
            .Set(new ConditionKey("reward_id"), RewardId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
