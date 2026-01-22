using System.Collections.Generic;
using TwitchySharp.Shared.EventSub.Constants;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// A broadcaster updates their channel properties e.g., category, title, content classification labels, broadcast, or language.
/// </summary>
/// <remarks>
/// No authorization required.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) you want to get updates for.</param>
public sealed record ChannelUpdate(UserId BroadcasterUserId)
    : IEventSubSubscriptionType
{
    public EventSubSubscriptionTypeName Name { get; } = new(EventSubSubscriptionTypeNames.CHANNEL_UPDATE);
    public EventSubSubscriptionTypeVersion Version { get; } = new(EventSubSubscriptionTypeVersions.V2);

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
