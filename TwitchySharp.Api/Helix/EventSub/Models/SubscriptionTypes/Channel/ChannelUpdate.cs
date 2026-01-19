using System.Collections.Generic;
using TwitchySharp.Shared.EventSub.Constants;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// A broadcaster updates their channel properties e.g., category, title, content classification labels, broadcast, or language.
/// </summary>
/// <remarks>
/// No authorization required.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) you want to get updates for.</param>
public sealed record ChannelUpdate(string BroadcasterUserId)
    : IEventSubSubscriptionType
{
    public string Type => EventSubSubscriptionTypeNames.CHANNEL_UPDATE;
    public string Version => EventSubSubscriptionTypeVersions.V2;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
