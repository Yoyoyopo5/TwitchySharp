using System.Collections.Generic;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.EventSub.Constants;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// A user is sent a warning. Broadcasters and moderators can see the warning’s details.
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
    : IEventSubSubscriptionType
{
    public EventSubSubscriptionTypeName Name { get; } = new(EventSubSubscriptionTypeNames.CHANNEL_WARNING_SEND);
    public EventSubSubscriptionTypeVersion Version { get; } = new(EventSubSubscriptionTypeVersions.V1);

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId)
            .Set("moderator_user_id", ModeratorUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
