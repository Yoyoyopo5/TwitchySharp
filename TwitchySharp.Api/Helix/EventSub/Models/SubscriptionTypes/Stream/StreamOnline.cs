using System.Collections.Generic;
using TwitchySharp.Shared.EventSub.Constants;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// The specified broadcaster starts a stream.
/// </summary>
/// <remarks>
/// No authorization required.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) you want to get stream online notifications for.</param>
public sealed record StreamOnline(string BroadcasterUserId)
    : IEventSubSubscriptionType
{
    public string Type => EventSubSubscriptionTypeNames.STREAM_ONLINE;
    public string Version => EventSubSubscriptionTypeVersions.V1;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
