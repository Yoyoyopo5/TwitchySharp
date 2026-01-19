namespace TwitchySharp.Api.Helix.EventSub;

/// <summary>
/// An EventSub transport that uses <see href="https://dev.twitch.tv/docs/eventsub/handling-conduit-events/">conduits</see>.
/// </summary>
/// <param name="ConduitId">The id of the conduit to use for the subscription notifications.</param>
public sealed record ConduitSubscriptionTransport(string ConduitId)
    : NewEventSubSubscriptionTransport
{
    /// <summary>
    /// The transport method identifier.
    /// </summary>
    public override string Method => "conduit";
    /// <summary>
    /// The id of the conduit that notifications are sent to.
    /// </summary>
    public override string ConduitId { get; } = ConduitId;
}
