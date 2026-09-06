using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// A viewer has redeemed a custom channel points reward on the specified channel.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadRedemptions"/> or <see cref="Scope.ChannelManageRedemptions"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The broadcaster user ID for the channel you want to receive channel points custom reward redemption add notifications for.</param>
/// <param name="RewardId">Optional. Specify a reward id to only receive notifications for a specific reward.</param>
public sealed record ChannelPointsCustomRewardRedemptionAdd(UserId BroadcasterUserId, RewardId? RewardId = null)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<ChannelPointsCustomRewardRedemptionAdd>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelPointsCustomRewardRedemptionAdd;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.ChannelPointsCustomRewardRedemptionAdd;
    public override EventSubSubscriptionAuthenticationContext.UserAuthorized AuthenticationContext
        => new()
        {
            Identity = new TwitchIdentity.User(BroadcasterUserId),
            ValidScopes = ImmutableHashSet.Create(Scope.ChannelReadRedemptions, Scope.ChannelManageRedemptions)
        };

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId)
            .Set(new ConditionKey("reward_id"), RewardId);
    public static Validation<ChannelPointsCustomRewardRedemptionAdd> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .GetValue(new("reward_id"), out RewardId? RewardId, value => new(value))
            .Map(_ => new ChannelPointsCustomRewardRedemptionAdd(BroadcasterUserId, RewardId));
}
