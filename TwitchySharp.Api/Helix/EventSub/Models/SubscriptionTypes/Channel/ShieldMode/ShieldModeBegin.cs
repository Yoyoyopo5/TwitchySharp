using System.Collections.Generic;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// Sends a notification when the broadcaster activates Shield Mode.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadShieldMode"/> or <see cref="Scope.ModeratorManageShieldMode"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) that you want to receive notifications about when they activate Shield Mode.</param>
/// <param name="ModeratorUserId">
/// The user id of the broadcaster or one of the broadcaster's moderators.
/// This user must have created a user access token for this application that includes <see cref="Scope.ModeratorReadShieldMode"/> or <see cref="Scope.ModeratorManageShieldMode"/>.
/// </param>
public sealed record ShieldModeBegin(UserId BroadcasterUserId, UserId ModeratorUserId)
    : IEventSubSubscriptionType
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ShieldModeBegin;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId)
            .Set("moderator_user_id", ModeratorUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
