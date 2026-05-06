using System.Collections.Generic;
using System.Collections.Immutable;
using TwitchySharp.Shared.EventSub;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// Get notified when a broadcaster ends a goal.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadGoals"/>.
/// </remarks>
/// <param name="BroadcasterUserId">
/// The user id of the broadcaster to get notified about.
/// This user must have created a user access token for this application that includes <see cref="Scope.ChannelReadGoals"/>.
/// </param>
public sealed record GoalEnd(UserId BroadcasterUserId)
    : IUserAuthorizedSubscriptionType
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.GoalEnd;
    public ConditionKey AuthorizingUserConditionKey => new ConditionKey("broadcaster_user_id");
    public IReadOnlySet<Scope> ValidScopes => ImmutableHashSet.Create(Scope.ChannelReadGoals);

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
