using System.Collections.Generic;
using System.Collections.Immutable;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;
using TwitchySharp.Shared.EventSub;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// A user is sent a warning. Broadcasters and moderators can see the warning's details.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadWarnings"/> or <see cref="Scope.ModeratorManageWarnings"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) to receive warning sent notifications for.</param>
/// <param name="ModeratorUserId">
/// The user id of the broadcaster or a moderator in a broadcaster's chat.
/// This user must have created a user access token that includes <see cref="Scope.ModeratorReadWarnings"/> or <see cref="Scope.ModeratorManageWarnings"/> for this application.
/// </param>
public sealed record ChannelWarningSend(UserId BroadcasterUserId, UserId ModeratorUserId)
    : IUserAuthorizedSubscriptionType
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelWarningSend;
    public ConditionKey AuthorizingUserConditionKey => new ConditionKey("moderator_user_id");
    public IReadOnlySet<Scope> ValidScopes => ImmutableHashSet.Create(Scope.ModeratorReadWarnings, Scope.ModeratorManageWarnings);

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new ConditionKey("broadcaster_user_id"), BroadcasterUserId)
            .Set(new ConditionKey("moderator_user_id"), ModeratorUserId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
