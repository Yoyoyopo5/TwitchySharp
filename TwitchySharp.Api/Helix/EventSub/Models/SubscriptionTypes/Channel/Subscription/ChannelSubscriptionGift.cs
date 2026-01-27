using System.Collections.Generic;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// A notification when a viewer gives a gift subscription to one or more users in the specified channel.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadSubscriptions"/>.
/// </remarks>
/// <param name="BroadcasterUserId">
/// The user id of the broadcaster (channel) you want to get subscription gift notifications for.
/// This user must have created a user access token that includes <see cref="Scope.ChannelReadSubscriptions"/> for this application.
/// </param>
public sealed record ChannelSubscriptionGift(UserId BroadcasterUserId)
    : IEventSubSubscriptionType
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelSubscriptionGift;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
