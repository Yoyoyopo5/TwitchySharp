using System.Collections.Generic;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// A notification is sent when a specified channel receives a subscriber. This does not include resubscribes.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadSubscriptions"/>.
/// </remarks>
/// <param name="BroadcasterUserId">
/// The user id of the broadcaster (channel) you want to get subscribe notifications for.
/// This user must have created a user access token that includes <see cref="Scope.ChannelReadSubscriptions"/> for this application.
/// </param>
public sealed record ChannelSubscribe(UserId BroadcasterUserId)
    : IEventSubSubscriptionType
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelSubscribe;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
