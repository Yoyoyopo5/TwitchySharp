using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// A suspicious user has been updated.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadSuspiciousUsers"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) you want to get suspicious user update notifications for.</param>
/// <param name="ModeratorUserId">
/// The user id of the broadcaster or a moderator in the broadcaster's channel.
/// This user must have created a user access token that includes <see cref="Scope.ModeratorReadSuspiciousUsers"/> for this application.
/// </param>
public sealed record ChannelSuspiciousUserUpdate(UserId BroadcasterUserId, UserId ModeratorUserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<ChannelSuspiciousUserUpdate>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelSuspiciousUserUpdate;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.ChannelSuspiciousUserUpdate;
    public static ConditionKey AuthorizingUserConditionKey { get; } = new ConditionKey("moderator_user_id");
    public override IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.ModeratorReadSuspiciousUsers);
    public override TwitchIdentity Identity { get; } = new TwitchIdentity.User(ModeratorUserId);

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new ConditionKey("moderator_user_id"), ModeratorUserId)
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);
    public static Validation<ChannelSuspiciousUserUpdate> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("moderator_user_id"), out UserId ModeratorUserId, value => new(value))
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .Map(_ => new ChannelSuspiciousUserUpdate(ModeratorUserId, BroadcasterUserId));
}
