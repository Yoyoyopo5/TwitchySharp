namespace TwitchySharp.Api.Helix.EventSub;

/// <summary>
/// Used when creating a new EventSub subscription.
/// See built-in derived types <see cref="WebhookSubscriptionTransport"/>, <see cref="WebsocketSubscriptionTransport"/>, and <see cref="ConduitSubscriptionTransport"/>.
/// </summary>
public abstract record NewEventSubSubscriptionTransport
{
    public abstract string Method { get; }
    public virtual string? Callback { get; }
    public virtual string? Secret { get; }
    public virtual string? SessionId { get; }
    public virtual string? ConduitId { get; }
}
