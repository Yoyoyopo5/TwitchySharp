using System.Collections.Generic;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;
using TwitchySharp.Shared.EventSub;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// A message in the automod queue had its status changed. Only public blocked terms trigger notifications, not private ones.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageAutomod"/>.
/// The user who created the access token must be the same user as the <paramref name="ModeratorUserId"/>.
/// </remarks>
/// <param name="BroadcasterUserId">User id of the broadcaster (channel).</param>
/// <param name="ModeratorUserId">User id of a moderator in the broadcaster's chat. This can also be the broadcaster.</param>
public sealed record AutomodMessageUpdateV2(UserId BroadcasterUserId, UserId ModeratorUserId)
    : IUserAuthorizedSubscriptionType
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.AutomodMessageUpdateV2;
    public ConditionKey AuthorizingUserConditionKey => new ConditionKey("moderator_user_id");
    public IEnumerable<Scope> ValidScopes => [ Scope.ModeratorManageAutomod ];

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId)
            .Set(new ConditionKey("moderator_user_id"), ModeratorUserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
