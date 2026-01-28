using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.Api.Helix.EventSub;

public record EventSubSubscriptionSpecification
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
    public required EventSubSubscriptionTransportSpecification Transport { get; set; }
}

internal static class EventSubSubscriptionSpecificationExtensions
{
    /// <summary>
    /// Determines whether the subscription requires a user access token.
    /// </summary>
    /// <param name="subscription">The subscription to check.</param>
    /// <returns><see langword="true"/> if the subscription uses WebSocket transport; otherwise, <see langword="false"/>.</returns>
    internal static bool RequiresUserAccessToken(this EventSubSubscriptionSpecification subscription)
        => subscription.Transport.Method == EventSubTransportMethod.Websocket;
}
