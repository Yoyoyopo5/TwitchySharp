using System.Collections.Generic;
using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// Sends a notification when the specified broadcaster sends a Shoutout.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadShoutouts"/> or <see cref="Scope.ModeratorManageShoutouts"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) that you want to receive notifications about when they send a Shoutout.</param>
/// <param name="ModeratorUserId">
/// The user id of the broadcaster or one of the broadcaster's moderators.
/// This user must have created a user access token for this application that includes <see cref="Scope.ModeratorReadShoutouts"/> or <see cref="Scope.ModeratorManageShoutouts"/>.
/// </param>
public sealed record ShoutoutCreate(UserId BroadcasterUserId, UserId ModeratorUserId)
    : IUserAuthorizedSubscriptionType
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ShoutoutCreate;
    public ConditionKey AuthorizingUserConditionKey => new ConditionKey("moderator_user_id");
    public IReadOnlySet<Scope> ValidScopes => ImmutableHashSet.Create(Scope.ModeratorReadShoutouts, Scope.ModeratorManageShoutouts);

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId)
            .Set(new ConditionKey("moderator_user_id"), ModeratorUserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
