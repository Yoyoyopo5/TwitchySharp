using System.Collections.Generic;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;
using TwitchySharp.Shared.EventSub;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// A chat message has been sent by a suspicious user.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadSuspiciousUsers"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) you want to get suspicious user message notifications for.</param>
/// <param name="ModeratorUserId">
/// The user id of the broadcaster or a moderator in the broadcaster's channel.
/// This user must have created a user access token that includes <see cref="Scope.ModeratorReadSuspiciousUsers"/> for this application.
/// </param>
public sealed record ChannelSuspiciousUserMessage(UserId BroadcasterUserId, UserId ModeratorUserId)
    : IUserAuthorizedSubscriptionType
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelSuspiciousUserMessage;
    public ConditionKey AuthorizingUserConditionKey => new ConditionKey("moderator_user_id");
    public IEnumerable<Scope> ValidScopes => [ Scope.ModeratorReadSuspiciousUsers ];

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new ConditionKey("moderator_user_id"), ModeratorUserId)
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
