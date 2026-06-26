using System.Text.Json;
using Microsoft.IO;
using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Serialization;
using TwitchySharp.EventSub.Websocket.Functional;
using TwitchySharp.Infrastructure.Functional;
using TwitchySharp.Serialization;

namespace TwitchySharp.EventSub.Websocket.Serialization;

/// <summary>
/// Contains a static method for creating a default <see cref="DeserializeWebsocketMessage"/> function.
/// </summary>
public static class WebsocketMessageDeserializer
{
    /// <summary>
    /// An error that occurred during Websocket message deserialization.
    /// </summary>
    /// <param name="Message">The error message.</param>
    public record DeserializationError(string Message) : Error(Message);

    /// <summary>
    /// Create a default <see cref="ProcessWebsocketMessage"/> function with the configured parameters.
    /// </summary>
    /// <param name="deserializeNotification">The notification payload deserialize function to use.</param>
    /// <param name="serializerOptions">The serializer options to use.</param>
    /// <returns>A configured function that deserializes incoming Twitch EventSub Websocket messages.</returns>
    public static ProcessWebsocketMessage Create(
        DeserializeNotification? deserializeNotification = null,
        JsonSerializerOptions? serializerOptions = null
        )
    {
        deserializeNotification ??= NotificationDeserializer.CreateDeserializer();
        serializerOptions ??= JsonConfig.ApiOptions;

        return (message, ct) => DeserializeMessage(deserializeNotification, serializerOptions, message, ct);
    }

    private static async ValueTask<Validation<EventSubWebsocketMessage>> DeserializeMessage(DeserializeNotification deserializeNotification, JsonSerializerOptions options, WebsocketMessageStream message, CancellationToken ct = default)
    {
        using JsonDocument document = await JsonDocument.ParseAsync(message, cancellationToken: ct);
        MessageElement messageElement = new(document.RootElement);

        return await messageElement.Deserialize(options)
            .Match<ValueTask<Validation<EventSubWebsocketMessage>>>(
            onError: e => ValueTask.FromResult<Validation<EventSubWebsocketMessage>>(e),
            onValid: async message => message.Metadata.MessageType.Value switch
            {
                WebsocketMessageTypes.WELCOME => message.DeserializePayload<WelcomeMessagePayload>(options),
                WebsocketMessageTypes.KEEPALIVE => message.DeserializePayload<KeepaliveMessagePayload>(options),
                WebsocketMessageTypes.NOTIFICATION => (await message.Payload.ToNotification(deserializeNotification, ct))
                    .Bind<EventSubWebsocketMessage>(n => message.WithPayload(_ => new NotificationMessagePayload() { Notification = n })),
                WebsocketMessageTypes.REVOCATION => message.DeserializePayload<RevocationMessagePayload>(options),
                WebsocketMessageTypes.RECONNECT => message.DeserializePayload<ReconnectMessagePayload>(options),
                _ => new DeserializationError("The \"message_type\" metadata property was an unsupported value.")
            });
    }

    private readonly record struct MessageElement(JsonElement Value);
    private readonly record struct MetadataElement(JsonElement Value);
    private static Validation<MetadataElement> GetMetadata(this MessageElement messageElement)
        => messageElement.Value.TryGetProperty("metadata", out JsonElement metadataElement) switch
        {
            true when metadataElement.ValueKind == JsonValueKind.Object => new MetadataElement(metadataElement),
            _ => new DeserializationError("Message was missing required property \"metadata\".")
        };
    private readonly record struct PayloadElement(JsonElement Value);
    private static Validation<PayloadElement> GetPayload(this MessageElement messageElement)
        => messageElement.Value.TryGetProperty("payload", out JsonElement payloadElement) switch
        {
            true when payloadElement.ValueKind == JsonValueKind.Object => new PayloadElement(payloadElement),
            _ => new DeserializationError("Notification message was missing required property \"payload\".")
        };

    private static Validation<EventSubWebsocketMessage<PayloadElement>> Deserialize(this MessageElement message, JsonSerializerOptions options)
        => message.GetMetadata()
            .Bind<EventSubMessageMetadata>(m => JsonSerializer.Deserialize<EventSubMessageMetadata>(m.Value, options)!)
            .Bind(metadata => message.GetPayload().Map(payload => new EventSubWebsocketMessage<PayloadElement>()
            {
                Metadata = metadata,
                Payload = payload
            }));

    private static EventSubWebsocketMessage<TPayload> WithPayload<TPayload>(this EventSubWebsocketMessage<PayloadElement> message, Func<PayloadElement, TPayload> deserialize)
    => new()
    {
        Metadata = message.Metadata,
        Payload = deserialize(message.Payload)
    };
    private static EventSubWebsocketMessage<TPayload> DeserializePayload<TPayload>(this EventSubWebsocketMessage<PayloadElement> message, JsonSerializerOptions options)
        => message.WithPayload(payload => JsonSerializer.Deserialize<TPayload>(payload.Value, options)!);

    private static readonly RecyclableMemoryStreamManager _memoryManager = new();
    private static ValueTask<Validation<IEventSubNotification>> ToNotification(this PayloadElement payloadElement, DeserializeNotification deserialize, CancellationToken ct)
    {
        using Stream stream = _memoryManager.GetStream();
        return deserialize(payloadElement.WriteTo(stream), ct);
    }

    private static NotificationPayloadStream WriteTo(this PayloadElement payloadElement, Stream stream)
    {
        using (Utf8JsonWriter writer = new(stream))
        {
            payloadElement.Value.WriteTo(writer);
        }
        stream.Position = 0;
        return new(stream);
    }
}
