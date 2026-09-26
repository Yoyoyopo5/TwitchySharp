using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// A user is notified if a message is caught by automod for review.
/// Only public blocked terms trigger notifications, not private ones.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageAutomod"/> for <paramref name="ModeratorUserId"/>.
/// </remarks>
/// <param name="BroadcasterUserId">User id of the broadcaster (channel).</param>
/// <param name="ModeratorUserId">User id of a moderator in the broadcaster's chat. This can also be the broadcaster.</param>
public sealed record AutomodMessageHoldV2(UserId BroadcasterUserId, UserId ModeratorUserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<AutomodMessageHoldV2>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.AutomodMessageHoldV2;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.AutomodMessageHoldV2;
    public override EventSubSubscriptionAuthenticationContext.UserAuthorized AuthenticationContext
        => new()
        {
            Identity = new TwitchIdentity.User(ModeratorUserId),
            ValidScopes = ImmutableHashSet.Create(Scope.ModeratorManageAutomod)
        };

    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId)
            .Set(new ConditionKey("moderator_user_id"), ModeratorUserId);
    public static Validation<AutomodMessageHoldV2> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("broadcaster_user_id"), out UserId BroadcasterUserId, value => new(value))
            .GetRequiredValue(new("moderator_user_id"), out UserId ModeratorUserId, value => new(value))
            .Map(_ => new AutomodMessageHoldV2(BroadcasterUserId, ModeratorUserId));
}
