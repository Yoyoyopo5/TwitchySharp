using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;

/// <summary>
/// A viewer has redeemed an automatic channel points reward on the specified channel.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadRedemptions"/> or <see cref="Scope.ChannelManageRedemptions"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The broadcaster user ID for the channel you want to receive Channel Points Reward Add V2 notifications for.</param>
public sealed record ChannelPointsAutomaticRewardRedemptionAddV2(UserId BroadcasterUserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<ChannelPointsAutomaticRewardRedemptionAddV2>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelPointsAutomaticRewardRedemptionAddV2;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.ChannelPointsAutomaticRewardRedemptionAddV2;
    public override IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.ChannelReadRedemptions, Scope.ChannelManageRedemptions);
    public override TwitchIdentity Identity { get; } = new TwitchIdentity.User(BroadcasterUserId);

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);
    public static Validation<ChannelPointsAutomaticRewardRedemptionAddV2> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .Map(_ => new ChannelPointsAutomaticRewardRedemptionAddV2(BroadcasterUserId));
}
