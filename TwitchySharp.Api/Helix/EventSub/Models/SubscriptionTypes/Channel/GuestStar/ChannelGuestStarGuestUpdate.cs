using System.Collections.Generic;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;
using TwitchySharp.Shared.EventSub;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// A guest or a slot is updated in an active Guest Star session.
/// </summary>
/// <remarks>
/// Requires a user access token that includes (one of) <see cref="Scope.ChannelReadGuestStar"/>, <see cref="Scope.ChannelManageGuestStar"/>, <see cref="Scope.ModeratorReadGuestStar"/>, or <see cref="Scope.ModeratorManageGuestStar"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) hosting the Guest Star Session.</param>
/// <param name="ModeratorUserId">The user id of the broadcaster or a moderator of the specified broadcaster.</param>
public sealed record ChannelGuestStarGuestUpdate(UserId BroadcasterUserId, UserId ModeratorUserId)
    : IUserAuthorizedSubscriptionType
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelGuestStarGuestUpdate;
    public ConditionKey AuthorizingUserConditionKey => new ConditionKey("moderator_user_id");
    public IEnumerable<Scope> ValidScopes => [ Scope.ChannelReadGuestStar, Scope.ChannelManageGuestStar, Scope.ModeratorReadGuestStar, Scope.ModeratorManageGuestStar ];

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId)
            .Set(new ConditionKey("moderator_user_id"), ModeratorUserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
