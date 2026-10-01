using System.Text.Json;
using Microsoft.IO;
using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Serialization;
using TwitchySharp.Infrastructure.Functional;
using TwitchySharp.Serialization;

namespace TwitchySharp.EventSub.Websocket.Functional;

/// <summary>
/// Process a single EventSub Websocket message.
/// </summary>
/// <param name="message">The incoming message stream.</param>
/// <param name="ct">Cancellation token.</param>
/// <returns>A <see cref="ValueTask"/> containing a <see cref="Result"/> of the deserialized <see cref="EventSubWebsocketMessage"/>.</returns>
public delegate ValueTask<Result<EventSubWebsocketMessage>> ProcessWebsocketMessage(WebsocketMessageStream message, CancellationToken ct);

/// <summary>
/// Creation helpers for <see cref="ProcessWebsocketMessage"/>.
/// </summary>
public static class ProcessWebsocketMessageSerializationExtensions
{
    /// <summary>
    /// An exception occurred during message deserialization.
    /// </summary>
    /// <param name="JsonSerializerException">The exception.</param>
    public record DeserializationExceptionError(Exception JsonSerializerException)
        : Error(JsonSerializerException.Message);

    /// <summary>
    /// A required JSON element has <see cref="JsonValueKind.Null"/>.
    /// </summary>
    /// <param name="RequiredElement">The name of the element that was required.</param>
    public record NullLiteralError(string? RequiredElement)
        : Error($"Required element {RequiredElement} was a null literal.");

    /// <summary>
    /// The message has a <see cref="JsonValueKind"/> other than <see cref="JsonValueKind.Object"/>.
    /// </summary>
    /// <param name="ActualKind">The message's <see cref="JsonValueKind"/>.</param>
    public record InvalidMessageKindError(JsonValueKind ActualKind)
        : Error($"Message was JSON value kind {ActualKind} (expected Object).");

    /// <summary>
    /// A required property of the message was not present or was not <see cref="JsonValueKind.Object"/>.
    /// </summary>
    /// <param name="RequiredProperty">The name of the invalid property.</param>
    public record MissingRequiredObjectPropertyError(string RequiredProperty)
        : Error($"Message was missing required object property \"{RequiredProperty}\".");

    /// <summary>
    /// The <see cref="WebsocketMessageType"/> of the message was not a supported value.
    /// </summary>
    /// <param name="MessageType">The message type value that was received.</param>
    public record UnsupportedMessageTypeError(string MessageType)
        : Error($"Unsupported websocket message type.");

    /// <summary>
    /// Contains optional configuration for <see cref="ByJsonDeserialization"/>.
    /// </summary>
    public record MessageDeserializationOptions
    {
        /// <summary>
        /// The function to use when deserializing notification message types.
        /// </summary>
        /// <remarks>
        /// By default, uses <see cref="DeserializeNotificationExtensions.ByPolymorphicJsonDeserialization"/>
        /// </remarks>
        public DeserializeNotification DeserializeNotification { get; init; }
            = DeserializeNotification.ByPolymorphicJsonDeserialization();
    }

    extension(ProcessWebsocketMessage p)
    {
        /// <summary>
        /// Create a <see cref="ProcessWebsocketMessage"/> function that deserializes incoming websocket message streams as JSON.
        /// </summary>
        /// <param name="configure">A function takes the default set of options and returns configured options.</param>
        /// <returns>A <see cref="ProcessWebsocketMessage"/> function that uses JSON deserialization.</returns>
        public static ProcessWebsocketMessage ByJsonDeserialization(
            Func<MessageDeserializationOptions, MessageDeserializationOptions>? configure = null
            )
        {
            MessageDeserializationOptions opts = configure is null ? new() : configure(new());

            return async (message, ct) =>
            {
                try
                {
                    using JsonDocument messageDocument = await JsonDocument.ParseAsync(message, cancellationToken: ct);

                    return await messageDocument.RootElement
                        .ToMessageElement()
                        .Match(
                            e => ValueTask.FromResult<Result<EventSubWebsocketMessage>>(e),
                            messageElement => messageElement.ToWebsocketMessage(opts.DeserializeNotification, ct)
                            );
                }
                catch (Exception ex)
                {
                    return new DeserializationExceptionError(ex);
                }
            };
        }
    }

    private readonly record struct MessageElement(JsonElement Value);
    private static Result<MessageElement> ToMessageElement(this JsonElement element)
        => element.ValueKind == JsonValueKind.Object
            ? new MessageElement(element)
            : new InvalidMessageKindError(element.ValueKind);
    private static ValueTask<Result<EventSubWebsocketMessage>> ToWebsocketMessage(
        this MessageElement messageElement,
        DeserializeNotification deserializeNotification,
        CancellationToken ct)
        => messageElement.GetMetadata()
            .Bind(metadata => metadata.Value.DeserializeValidation<EventSubMessageMetadata>(METADATA_PROPERTY_NAME))
            .Match<ValueTask<Result<EventSubWebsocketMessage>>>(
                e => ValueTask.FromResult<Result<EventSubWebsocketMessage>>(e),
                async metadata => metadata.MessageType.Value switch
                {
                    WebsocketMessageTypes.WELCOME => EventSubWebsocketMessage.Create<WelcomeMessagePayload>(metadata, messageElement.GetPayload()),
                    WebsocketMessageTypes.KEEPALIVE => EventSubWebsocketMessage.Create<KeepaliveMessagePayload>(metadata, messageElement.GetPayload()),
                    WebsocketMessageTypes.NOTIFICATION => await messageElement.GetPayload().ToNotification(deserializeNotification, ct)
                        .MapAsync<IEventSubNotification, EventSubWebsocketMessage>(n => new EventSubWebsocketMessage<NotificationMessagePayload>() { Metadata = metadata, Payload = new(n) }),
                    WebsocketMessageTypes.REVOCATION => EventSubWebsocketMessage.Create<RevocationMessagePayload>(metadata, messageElement.GetPayload()),
                    WebsocketMessageTypes.RECONNECT => EventSubWebsocketMessage.Create<ReconnectMessagePayload>(metadata, messageElement.GetPayload()),
                    _ => new UnsupportedMessageTypeError(metadata.MessageType.Value)
                });

    private readonly record struct MetadataElement(JsonElement Value);
    private const string METADATA_PROPERTY_NAME = "metadata";
    private static Result<MetadataElement> GetMetadata(this MessageElement messageElement)
        => messageElement.Value.TryGetProperty(METADATA_PROPERTY_NAME, out JsonElement metadataElement) switch
        {
            true when metadataElement.ValueKind == JsonValueKind.Object => new MetadataElement(metadataElement),
            _ => new MissingRequiredObjectPropertyError(METADATA_PROPERTY_NAME)
        };

    private static Result<T> DeserializeValidation<T>(
        this JsonElement element,
        string elementName
        )
    {
        try
        {
            return JsonSerializer.Deserialize<T>(element, JsonConfig.ApiOptions) is T metadata
                ? metadata
                : new NullLiteralError(elementName);
        }
        catch (Exception ex)
        {
            return new DeserializationExceptionError(ex);
        }
    }

    private const string PAYLOAD_PROPERTY_NAME = "payload";
    private static Result<JsonElement> GetPayload(this MessageElement messageElement)
        => messageElement.Value.TryGetProperty(PAYLOAD_PROPERTY_NAME, out JsonElement payloadElement) switch
        {
            true when payloadElement.ValueKind == JsonValueKind.Object => payloadElement,
            _ => new MissingRequiredObjectPropertyError(PAYLOAD_PROPERTY_NAME)
        };

    extension(EventSubWebsocketMessage _)
    {
        private static Result<EventSubWebsocketMessage> Create<TPayload>(
            EventSubMessageMetadata metadata,
            Result<JsonElement> payloadElement
            )
            => payloadElement.Bind(p => p.DeserializeValidation<TPayload>(PAYLOAD_PROPERTY_NAME))
                .Map<EventSubWebsocketMessage>(payload => new EventSubWebsocketMessage<TPayload>()
                {
                    Metadata = metadata,
                    Payload = payload
                });
    }

    private static readonly RecyclableMemoryStreamManager _memoryManager = new();
    private static ValueTask<Result<IEventSubNotification>> ToNotification(
        this Result<JsonElement> payloadElement,
        DeserializeNotification deserialize,
        CancellationToken ct)
        => payloadElement.Match<ValueTask<Result<IEventSubNotification>>>(
                e => ValueTask.FromResult<Result<IEventSubNotification>>(e),
                async p =>
                {
                    using Stream stream = _memoryManager.GetStream();
                    return await deserialize(p.WriteTo(stream), ct);
                });

    private static NotificationPayloadStream WriteTo(this JsonElement payloadElement, Stream stream)
    {
        using (Utf8JsonWriter writer = new(stream))
        {
            payloadElement.WriteTo(writer);
        }
        stream.Position = 0;
        return new(stream);
    }
}
