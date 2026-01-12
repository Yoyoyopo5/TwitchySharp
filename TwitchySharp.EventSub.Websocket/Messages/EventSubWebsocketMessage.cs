using System.Reactive;
using System.Text.Json;
using TwitchySharp.EventSub.Websocket.Messages.Payloads;

namespace TwitchySharp.EventSub.Websocket.Messages;

public record EventSubWebsocketMessage<TPayload> : IEventSubWebsocketMessage
{
    public required EventSubMessageMetadata Metadata { get; init; }
    public required TPayload Payload { get; init; }
}

internal record EventSubWebsocketMessage : IEventSubWebsocketMessage
{
    public required EventSubMessageMetadata Metadata { get; init; }
}

public interface IEventSubWebsocketMessage
{
    EventSubMessageMetadata Metadata { get; }
}
