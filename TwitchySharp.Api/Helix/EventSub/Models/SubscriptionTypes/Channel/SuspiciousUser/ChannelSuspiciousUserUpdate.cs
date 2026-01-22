using System.Collections.Generic;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.EventSub.Constants;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// A suspicious user has been updated.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadSuspiciousUsers"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) you want to get suspicious user update notifications for.</param>
/// <param name="ModeratorUserId">
/// The user id of the broadcaster or a moderator in the broadcaster's channel.
/// This user must have created a user access token that includes <see cref="Scope.ModeratorReadSuspiciousUsers"/> for this application.
/// </param>
public sealed record ChannelSuspiciousUserUpdate(UserId BroadcasterUserId, UserId ModeratorUserId)
    : IEventSubSubscriptionType
{
    public EventSubSubscriptionTypeName Name { get; } = new(EventSubSubscriptionTypeNames.CHANNEL_SUSPICIOUS_USER_UPDATE);
    public EventSubSubscriptionTypeVersion Version { get; } = new(EventSubSubscriptionTypeVersions.V1);

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("moderator_user_id", ModeratorUserId)
            .Set("broadcaster_user_id", BroadcasterUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
