using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// A custom channel points reward has been removed from the specified channel.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadRedemptions"/> or <see cref="Scope.ChannelManageRedemptions"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The broadcaster user ID for the channel you want to receive channel points custom reward remove notifications for.</param>
/// <param name="RewardId">Optional. Specify a reward id to only receive notifications for a specific reward.</param>
public sealed record ChannelPointsCustomRewardRemove(UserId BroadcasterUserId, RewardId? RewardId = null)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<ChannelPointsCustomRewardRemove>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelPointsCustomRewardRemove;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.ChannelPointsCustomRewardRemove;
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
    public static Validation<ChannelPointsCustomRewardRemove> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .GetRequiredValue(new("reward_id"), out RewardId RewardId, value => new(value))
            .Map(_ => new ChannelPointsCustomRewardRemove(BroadcasterUserId, RewardId));
}
