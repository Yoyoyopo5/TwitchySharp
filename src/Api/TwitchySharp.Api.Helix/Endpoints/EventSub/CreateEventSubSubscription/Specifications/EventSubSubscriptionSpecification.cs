namespace TwitchySharp.Api.Helix.EventSub;

/// <summary>
/// Represents the specification for creating a new Twitch EventSub subscription,
/// including the subscription type and transport details.
/// </summary>
public record EventSubSubscriptionSpecification
{
    /// <summary>
    /// The type of subscription to create.
    /// </summary>
    public required EventSubSubscriptionTypeSpecification Type { get; init; }
    /// <summary>
    /// The transport type that you want Twitch to use when sending you notifications.
    /// Possible transport types are <see cref="WebhookSubscriptionTransport"/>, <see cref="WebsocketSubscriptionTransport"/>, and <see cref="ConduitSubscriptionTransport"/>.
    /// </summary>
    public required EventSubSubscriptionTransportSpecification Transport { get; init; }
}

internal static class EventSubSubscriptionSpecificationExtensions
{
    extension(EventSubSubscriptionSpecification specification)
    {
        public ITwitchRequestAuthenticationContext<TwitchIdentity> AuthenticationContext
            => specification.Type.ToRequestAuthenticationContext(specification.Transport.Method);
    }
}
