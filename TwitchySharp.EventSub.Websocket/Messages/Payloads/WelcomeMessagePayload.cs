namespace TwitchySharp.EventSub.Websocket.Messages.Payloads;

internal record WelcomeMessagePayload
{
    public required EventSubWebsocketSession Session { get; init; }
}
