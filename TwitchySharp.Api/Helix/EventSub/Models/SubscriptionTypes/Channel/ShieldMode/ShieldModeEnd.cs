using System.Collections.Generic;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;
using TwitchySharp.Shared.EventSub;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// Sends a notification when the broadcaster deactivates Shield Mode.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadShieldMode"/> or <see cref="Scope.ModeratorManageShieldMode"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) that you want to receive notifications about when they deactivate Shield Mode.</param>
/// <param name="ModeratorUserId">
/// The user id of the broadcaster or one of the broadcaster's moderators.
/// This user must have created a user access token for this application that includes <see cref="Scope.ModeratorReadShieldMode"/> or <see cref="Scope.ModeratorManageShieldMode"/>.
/// </param>
public sealed record ShieldModeEnd(UserId BroadcasterUserId, UserId ModeratorUserId)
    : IUserAuthorizedSubscriptionType
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ShieldModeEnd;
    public ConditionKey AuthorizingUserConditionKey => new ConditionKey("moderator_user_id");
    public IEnumerable<Scope> ValidScopes => [ Scope.ModeratorReadShieldMode, Scope.ModeratorManageShieldMode ];

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId)
            .Set(new ConditionKey("moderator_user_id"), ModeratorUserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
