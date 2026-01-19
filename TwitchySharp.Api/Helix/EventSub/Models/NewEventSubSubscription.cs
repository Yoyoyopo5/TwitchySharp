namespace TwitchySharp.Api.Helix.EventSub;

public record NewEventSubSubscription
{
    /// <summary>
    /// The type of subscription to create. 
    /// See the <see cref="EventSub.Types"/> namespace for built-in subscription types.
    /// </summary>
    public required IEventSubSubscriptionType Type { get; set; }
    /// <summary>
    /// The transport type that you want Twitch to use when sending you notifications.
    /// Possible transport types are <see cref="WebhookSubscriptionTransport"/>, <see cref="WebsocketSubscriptionTransport"/>, and <see cref="ConduitSubscriptionTransport"/>.
    /// </summary>
    public required NewEventSubSubscriptionTransport Transport { get; set; }
}
