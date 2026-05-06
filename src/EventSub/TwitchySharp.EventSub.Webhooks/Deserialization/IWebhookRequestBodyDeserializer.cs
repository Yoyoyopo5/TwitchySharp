using TwitchySharp.EventSub.Models.Notifications;
using TwitchySharp.EventSub.Webhooks.Enums;
using TwitchySharp.EventSub.Webhooks.Requests;

namespace TwitchySharp.EventSub.Webhooks.Deserialization;

/// <summary>
/// Deserializes Twitch EventSub webhook request body Streams into C# types.
/// </summary>
/// <remarks>
/// See <see cref="DefaultWebhookRequestDeserializer"/>.
/// </remarks>
public interface IWebhookRequestBodyDeserializer
{
    /// <summary>
    /// Deserialize a Twitch EventSub webhook request body.
    /// </summary>
    /// <param name="messageType">The message type. This is available from the request header.</param>
    /// <param name="body">The body of the webhook request as a <see cref="Stream"/>.</param>
    /// <returns>
    /// The deserialized body. 
    /// Use pattern matching to determine the underlying type.
    /// This is a <see cref="IEventSubNotification"/> because the notification request body is itself a
    /// </returns>
    ValueTask<IWebhookRequestData> DeserializeRequestBody(EventSubWebhookMessageType messageType, Stream body, CancellationToken ct = default);
}
