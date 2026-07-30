using System.Text.Json;
using System.Text.Json.Serialization;
using TwitchySharp.EventSub.Notifications;

namespace TwitchySharp.EventSub.Serialization;

internal class NotificationConverter(Func<EventSubSubscriptionType, Func<JsonSerializerOptions, JsonDocument, IEventSubNotification?>?> map) : JsonConverter<IEventSubNotification>
{
    private const string SUBSCRIPTION_PROPERTY = "subscription";
    private const string SUBSCRIPTION_TYPE_PROPERTY = "type";
    private const string SUBSCRIPTION_VERSION_PROPERTY = "version";

    private readonly Func<EventSubSubscriptionType, Func<JsonSerializerOptions, JsonDocument, IEventSubNotification?>?> _map = map;

    public override IEventSubNotification? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new NotSupportedException("Notification must be an object.");

        using JsonDocument document = JsonDocument.ParseValue(ref reader);
        if (!document.RootElement.TryGetProperty(SUBSCRIPTION_PROPERTY, out JsonElement subscriptionElement))
            throw new NotSupportedException("Notification does not have a subscription property.");

        if (!subscriptionElement.TryGetProperty(SUBSCRIPTION_TYPE_PROPERTY, out JsonElement subscriptionTypeElement))
            throw new NotSupportedException("Notification does not have a type property.");

        if (!subscriptionElement.TryGetProperty(SUBSCRIPTION_VERSION_PROPERTY, out JsonElement subscriptionVersionElement))
            throw new NotSupportedException("Notification does not have a version property.");

        if (subscriptionTypeElement.ValueKind != JsonValueKind.String)
            throw new NotSupportedException($"Notification type property is {subscriptionTypeElement.ValueKind} (Expected {nameof(JsonValueKind.String)}).");

        if (subscriptionVersionElement.ValueKind != JsonValueKind.String)
            throw new NotSupportedException($"Notification version property is {subscriptionTypeElement.ValueKind} (Expected {nameof(JsonValueKind.String)}).");

        EventSubSubscriptionType type = new(subscriptionTypeElement.GetString()!, subscriptionVersionElement.GetString()!);
        return _map(type) is not { } deserializer
            ? throw new NotSupportedException("Unknown subscription type.")
            : deserializer(options, document);
    }
    public override void Write(Utf8JsonWriter writer, IEventSubNotification value, JsonSerializerOptions options)
        => JsonSerializer.Serialize<object>(writer, value, options);
}
