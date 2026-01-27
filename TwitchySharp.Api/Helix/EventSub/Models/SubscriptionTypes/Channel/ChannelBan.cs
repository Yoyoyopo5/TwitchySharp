using System.Collections.Generic;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// A viewer is banned from the specified channel.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelModerate"/>
/// </remarks>
/// <param name="BroadcasterUserId">
/// The user id of the broadcaster (channel) you want to get ban notifications for.
/// This must have created a user access token including <see cref="Scope.ChannelModerate"/> for this application.
/// </param>
public sealed record ChannelBan(UserId BroadcasterUserId)
    : IEventSubSubscriptionType
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelBan;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
