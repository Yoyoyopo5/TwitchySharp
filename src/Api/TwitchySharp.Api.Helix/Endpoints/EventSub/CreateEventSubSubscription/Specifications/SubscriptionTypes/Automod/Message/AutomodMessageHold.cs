using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;

/// <summary>
/// A user is notified if a message is caught by automod for review.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageAutomod"/> for <paramref name="ModeratorUserId"/>.
/// </remarks>
/// <param name="BroadcasterUserId">User id of the broadcaster (channel).</param>
/// <param name="ModeratorUserId">User id of a moderator in the broadcaster's chat. This can also be the broadcaster.</param>
public sealed record AutomodMessageHold(UserId BroadcasterUserId, UserId ModeratorUserId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<AutomodMessageHold>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.AutomodMessageHold;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.AutomodMessageHold;
    public override EventSubSubscriptionAuthenticationContext.UserAuthorized AuthenticationContext
        => new()
        {
            Identity = new TwitchIdentity.User(ModeratorUserId),
            ValidScopes = ImmutableHashSet.Create(Scope.ModeratorManageAutomod)
        };
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
