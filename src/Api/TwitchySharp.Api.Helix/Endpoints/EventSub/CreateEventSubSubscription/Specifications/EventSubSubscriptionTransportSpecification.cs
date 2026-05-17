namespace TwitchySharp.Api.Helix.EventSub;

/// <summary>
/// The transport that should be used when creating a new EventSub subscription.
/// </summary>
/// <remarks>
/// See built-in derived types <see cref="WebhookSubscriptionTransport"/>, <see cref="WebsocketSubscriptionTransport"/>, and <see cref="ConduitSubscriptionTransport"/>.
/// </remarks>
public abstract record EventSubSubscriptionTransportSpecification
{
    /// <summary>
    /// The transport method identifier.
    /// </summary>
    public EventSubTransportMethod Method { get; protected init; } = new(string.Empty);
    /// <summary>
    /// The url that webhook subscription notifications will be sent to.
    /// </summary>
    public Uri? Callback { get; protected init; }
    /// <summary>
    /// The secret used to verify the signature of the webhook notification.
    /// </summary>
    /// <remarks>
    /// For information about how the secret is used, see <see href="https://dev.twitch.tv/docs/eventsub/handling-webhook-events#verifying-the-event-message">Verifying the event message</see>.
    /// </remarks>
    public WebhookSecret? Secret { get; protected init; }
    /// <summary>
    /// The id of the EventSub WebSocket session that notifications will be sent to.
    /// </summary>
    public EventSubWebsocketSessionId? SessionId { get; protected init; }
    /// <summary>
    /// The id of the conduit that notifications are sent to.
    /// </summary>
    public ConduitId? ConduitId { get; protected init; }
}
