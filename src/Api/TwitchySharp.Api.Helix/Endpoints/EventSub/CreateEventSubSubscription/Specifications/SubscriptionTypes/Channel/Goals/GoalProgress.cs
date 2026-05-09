using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// Get notified when progress (either positive or negative) is made towards a broadcaster's goal.
/// Note that it is possible to receive this event before receiving a <see cref="GoalBegin"/> event.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadGoals"/>.
/// </remarks>
/// <param name="BroadcasterUserId">
/// The user id of the broadcaster to get notified about.
/// This user must have created a user access token for this application that includes <see cref="Scope.ChannelReadGoals"/>.
/// </param>
public sealed record GoalProgress(UserId BroadcasterUserId)
    : IUserAuthorizedSubscriptionTypeSpecification
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.GoalProgress;
    public static ConditionKey AuthorizingUserConditionKey { get; } = new("broadcaster_user_id");
    public IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet.Create(Scope.ChannelReadGoals);
    public UserId AuthorizingUser => BroadcasterUserId;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new("broadcaster_user_id"), BroadcasterUserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
