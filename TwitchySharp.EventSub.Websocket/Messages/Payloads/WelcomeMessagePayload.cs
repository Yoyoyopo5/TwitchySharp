namespace TwitchySharp.EventSub.Websocket.Messages.Payloads;

public record WelcomeMessagePayload
{
    public required EventSubWebsocketSession Session { get; init; }
}
