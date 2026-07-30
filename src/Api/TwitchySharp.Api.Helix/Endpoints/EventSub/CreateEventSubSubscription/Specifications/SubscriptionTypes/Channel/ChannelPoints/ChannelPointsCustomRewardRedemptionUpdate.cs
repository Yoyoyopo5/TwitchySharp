using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// A redemption of a channel points custom reward has been updated for the specified channel.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadRedemptions"/> or <see cref="Scope.ChannelManageRedemptions"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The broadcaster user ID for the channel you want to receive channel points custom reward redemption update notifications for.</param>
/// <param name="RewardId">Optional. Specify a reward id to only receive notifications for a specific reward.</param>
public sealed record ChannelPointsCustomRewardRedemptionUpdate(UserId BroadcasterUserId, RewardId? RewardId = null)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<ChannelPointsCustomRewardRedemptionUpdate>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelPointsCustomRewardRedemptionUpdate;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.ChannelPointsCustomRewardRedemptionUpdate;
    public override IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.ChannelReadRedemptions, Scope.ChannelManageRedemptions);
    public override TwitchIdentity Identity { get; } = new TwitchIdentity.User(BroadcasterUserId);

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId)
            .Set(new ConditionKey("reward_id"), RewardId);
    public static Validation<ChannelPointsCustomRewardRedemptionUpdate> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .GetValue(new("reward_id"), out RewardId RewardId, value => new(value))
            .Map(_ => new ChannelPointsCustomRewardRedemptionUpdate(BroadcasterUserId, RewardId));
}
