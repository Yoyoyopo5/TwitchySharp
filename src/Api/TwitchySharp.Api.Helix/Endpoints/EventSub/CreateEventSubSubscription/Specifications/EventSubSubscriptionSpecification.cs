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

public static class EventSubSubscriptionSpecificationExtensions
{
    /// <summary>
    /// Get the correct <see cref="TwitchIdentity"/> to use when creating the <paramref name="specification"/>.
    /// </summary>
    /// <param name="specification">The <see cref="EventSubSubscriptionSpecification"/> to get the <see cref="TwitchIdentity"/> for.</param>
    /// <returns>A <see cref="TwitchIdentity"/> that can be used with a <see cref="CreateEventSubSubscriptionRequest"/>.</returns>
    public static TwitchIdentity GetRequestIdentity(this EventSubSubscriptionSpecification specification)
        => GetRequestIdentity(specification.Type, specification.Transport.Method);

    /// <summary>
    /// Get the correct <see cref="TwitchIdentity"/> to use when creating the <paramref name="typeSpecification"/>.
    /// </summary>
    /// <param name="typeSpecification">The <see cref="EventSubSubscriptionTypeSpecification"/> to get the <see cref="TwitchIdentity"/> for.</param>
    /// <param name="transportMethod">The <see cref="EventSubTransportMethod"/> that would be used when creating the subscription.</param>
    /// <returns>A <see cref="TwitchIdentity"/> that can be used with a <see cref="CreateEventSubSubscriptionRequest"/>.</returns>
    public static TwitchIdentity GetRequestIdentity(
        this EventSubSubscriptionTypeSpecification typeSpecification,
        EventSubTransportMethod transportMethod
        )
        => transportMethod switch
        {
            _ when transportMethod == EventSubTransportMethod.Websocket
                => typeSpecification.Identity, // Pass through user identity
            _ => typeSpecification.Identity switch
            {
                TwitchIdentity.User userIdentity => new TwitchIdentity.Client(userIdentity.ClientId),
                TwitchIdentity.Extension extensionIdentity => new TwitchIdentity.Client(extensionIdentity.ExtensionId),
                _ => typeSpecification.Identity // Client
            }
        };

    /// <summary>
    /// Get a <see cref="TwitchRequestAuthorizationContext"/> to use when creating the <paramref name="specification"/>.
    /// </summary>
    /// <param name="specification">The <see cref="EventSubSubscriptionSpecification"/> to get the <see cref="TwitchRequestAuthorizationContext"/> for.</param>
    /// <returns>A <see cref="TwitchRequestAuthorizationContext"/> that can be used with a <see cref="CreateEventSubSubscriptionRequest"/>.</returns>
    public static TwitchRequestAuthorizationContext GetRequestAuthorizationContext(this EventSubSubscriptionSpecification specification)
        => new()
        {
            Identity = specification.GetRequestIdentity(),
            ValidScopes = specification.Type.ValidScopes
        };
}
