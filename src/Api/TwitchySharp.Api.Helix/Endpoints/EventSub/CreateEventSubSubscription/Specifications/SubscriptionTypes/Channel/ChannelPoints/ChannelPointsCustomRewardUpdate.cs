using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.EventSub.Channel;
/// <summary>
/// A custom channel points reward has been updated for the specified channel.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadRedemptions"/> or <see cref="Scope.ChannelManageRedemptions"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The broadcaster user ID for the channel you want to receive channel points custom reward update notifications for.</param>
/// <param name="RewardId">Optional. Specify a reward id to only receive notifications for a specific reward.</param>
public sealed record ChannelPointsCustomRewardUpdate(UserId BroadcasterUserId, string? RewardId = null)
    : IUserAuthorizedSubscriptionTypeSpecification
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelPointsCustomRewardUpdate;
    public static ConditionKey AuthorizingUserConditionKey { get; } = new("broadcaster_user_id");
    public IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.ChannelReadRedemptions, Scope.ChannelManageRedemptions);
    public UserId AuthorizingUser => BroadcasterUserId;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId)
            .Set(new ConditionKey("reward_id"), RewardId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
