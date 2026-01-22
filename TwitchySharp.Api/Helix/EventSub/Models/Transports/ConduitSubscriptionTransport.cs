using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.EventSub;

/// <summary>
/// An EventSub transport that uses <see href="https://dev.twitch.tv/docs/eventsub/handling-conduit-events/">conduits</see>.
/// </summary>
public sealed record ConduitSubscriptionTransport
    : NewEventSubSubscriptionTransport
{
    /// <param name="conduitId">The id of the conduit to use for the subscription notifications.</param>
    public ConduitSubscriptionTransport(ConduitId conduitId)
        => (Method, ConduitId) = (EventSubTransportMethod.Conduit, conduitId);
}
