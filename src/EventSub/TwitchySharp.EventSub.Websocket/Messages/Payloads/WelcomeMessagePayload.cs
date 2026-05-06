namespace TwitchySharp.EventSub.Websocket.Messages.Payloads;

/// <summary>
/// A welcome message payload.
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/handling-websocket-events#welcome-message">Welcome Message</see> for more information.
/// </remarks>
public record WelcomeMessagePayload
{
    /// <summary>
    /// The EventSub session you are connected to.
    /// This contains the id you will need to use when subscribing to events.
    /// </summary>
    public required EventSubWebsocketSession Session { get; init; }
}
