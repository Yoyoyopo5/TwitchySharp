using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;

/// <summary>
/// A user is notified if a message is caught by automod for review.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageAutomod"/>.
/// The user who created the access token must be the same user as the <paramref name="ModeratorUserId"/>.
/// </remarks>
/// <param name="BroadcasterUserId">User id of the broadcaster (channel).</param>
/// <param name="ModeratorUserId">User id of a moderator in the broadcaster's chat. This can also be the broadcaster.</param>
public sealed record AutomodMessageHold(UserId BroadcasterUserId, UserId ModeratorUserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<AutomodMessageHold>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.AutomodMessageHold;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.AutomodMessageHold;
    public override IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.ModeratorManageAutomod);
    public override TwitchIdentity Identity { get; } = new TwitchIdentity.User(ModeratorUserId);
    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new("broadcaster_user_id"), BroadcasterUserId)
            .Set(new("moderator_user_id"), ModeratorUserId);
    public static Validation<AutomodMessageHold> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId broadcasterUserId, value => new(value))
            .GetRequiredValue(new("moderator_user_id"), out UserId moderatorUserId, value => new(value))
            .Map(_ => new AutomodMessageHold(broadcasterUserId, moderatorUserId));
}
