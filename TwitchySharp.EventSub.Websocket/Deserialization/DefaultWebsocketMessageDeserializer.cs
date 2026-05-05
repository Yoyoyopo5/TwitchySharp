using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Interfaces;
using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Notifications;
using TwitchySharp.EventSub.NotificationConverters;
using TwitchySharp.EventSub.Websocket.Messages;
using TwitchySharp.EventSub.Websocket.Messages.Enums;
using TwitchySharp.EventSub.Websocket.Messages.Payloads;
using TwitchySharp.Shared;

namespace TwitchySharp.EventSub.Websocket.Deserialization;

/// <summary>
/// The default implementation for <see cref="IEventSubWebsocketMessageDeserializer"/>.
/// Converts raw messages from a Twitch EventSub Websocket server into C# objects.
/// </summary>
/// <param name="notificationConverter">
/// The notification converter to use when deserializing notifications.
/// Defaults to <see cref="NotificationConverter"/> if left <see langword="null"/>.
/// </param>
/// <param name="jsonSerializerOptions">
/// The JSON serializer options to use when deserializing messages.
/// Defaults to <see cref="JsonConfig.ApiOptions"/> if left <see langword="null"/>.
/// </param>
public class DefaultWebsocketMessageDeserializer(
    INotificationConverter? notificationConverter = null,
    JsonSerializerOptions? jsonSerializerOptions = null
    )
    : IEventSubWebsocketMessageDeserializer
{
    private readonly INotificationConverter _converter = notificationConverter ?? new NotificationConverter();
    private readonly JsonSerializerOptions _serializerOptions = jsonSerializerOptions ?? JsonConfig.ApiOptions;

    public async ValueTask<IEventSubWebsocketMessage> DeserializeMessage(Stream message, CancellationToken ct = default)
    {
        EventSubWebsocketMessage<JsonElement> websocketMessage = await JsonSerializer.DeserializeAsync<EventSubWebsocketMessage<JsonElement>>(message, _serializerOptions, ct) ?? throw new NotSupportedException("Null literal message values are not supported.");
        object payload = DeserializeMessagePayload(websocketMessage.Metadata.MessageType, websocketMessage.Payload, _serializerOptions);
        return new EventSubWebsocketMessage<object> { Metadata = websocketMessage.Metadata, Payload = payload };
    }

    private object DeserializeMessagePayload(EventSubMessageType messageType, JsonElement payload, JsonSerializerOptions options)
        => messageType.Value switch
        {
            EventSubMessageTypes.WELCOME => JsonSerializer.Deserialize<WelcomeMessagePayload>(payload, options),
            EventSubMessageTypes.KEEPALIVE => JsonSerializer.Deserialize<KeepaliveMessagePayload>(payload, options),
            EventSubMessageTypes.NOTIFICATION => _converter.Deserialize(payload) as object,
            EventSubMessageTypes.REVOCATION => JsonSerializer.Deserialize<RevocationMessagePayload>(payload, options),
            EventSubMessageTypes.RECONNECT => JsonSerializer.Deserialize<ReconnectMessagePayload>(payload, options),
            _ => throw new NotSupportedException($"Tried to deserialize unsupported message type {messageType}.")
        } ?? throw new NotSupportedException("Null literal message payload values are not supported.");
}
