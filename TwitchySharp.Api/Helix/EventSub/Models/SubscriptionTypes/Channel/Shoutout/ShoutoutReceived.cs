using System.Collections.Generic;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.EventSub.Constants;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// Sends a notification when the specified broadcaster receives a Shoutout.
/// <b>Note: </b> Sent only if Twitch posts the Shoutout to the broadcaster’s activity feed.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadShoutouts"/> or <see cref="Scope.ModeratorManageShoutouts"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) that you want to receive notifications about when they receive a Shoutout.</param>
/// <param name="ModeratorUserId">
/// The user id of the broadcaster or one of the broadcaster’s moderators.
/// This user must have created a user access token for this application that includes <see cref="Scope.ModeratorReadShoutouts"/> or <see cref="Scope.ModeratorManageShoutouts"/>.
/// </param>
public sealed record ShoutoutReceived(UserId BroadcasterUserId, UserId ModeratorUserId)
    : IEventSubSubscriptionType
{
    public EventSubSubscriptionTypeName Name { get; } = new(EventSubSubscriptionTypeNames.SHOUTOUT_RECEIVED);
    public EventSubSubscriptionTypeVersion Version { get; } = new(EventSubSubscriptionTypeVersions.V1);

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId)
            .Set("moderator_user_id", ModeratorUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
