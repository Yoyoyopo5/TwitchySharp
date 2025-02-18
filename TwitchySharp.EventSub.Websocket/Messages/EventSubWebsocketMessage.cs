using System.Reactive;
using System.Text.Json;
using TwitchySharp.EventSub.Websocket.Messages.Payloads;

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
