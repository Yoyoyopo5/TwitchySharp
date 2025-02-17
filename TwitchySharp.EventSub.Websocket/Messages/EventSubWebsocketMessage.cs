namespace TwitchySharp.EventSub.Websocket.Messages;

internal record EventSubWebsocketMessage<TPayload> : IEventSubWebsocketMessage
{
    public required EventSubMessageMetadata Metadata { get; init; }
    public required TPayload Payload { get; init; }
}

internal interface IEventSubWebsocketMessage
{
    EventSubMessageMetadata Metadata { get; }
}
