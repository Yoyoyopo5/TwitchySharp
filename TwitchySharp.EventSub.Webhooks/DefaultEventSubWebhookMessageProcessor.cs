using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TwitchySharp.EventSub.NotificationConverters;
using TwitchySharp.EventSub.Notifications;
using TwitchySharp.Shared;

namespace TwitchySharp.EventSub.Webhooks;
public class DefaultEventSubWebhookMessageProcessor(
    IWebhookEventSubHandler handler,
    Func<string, string> secretResolver,
    INotificationConverter? converter = null,
    JsonSerializerOptions? serializerOptions = null
    )
    : IEventSubWebhookMessageProcessor
{
    private readonly IWebhookEventSubHandler _handler = handler;
    private readonly INotificationConverter _converter = converter ?? new NotificationConverter();
    private readonly Func<string, string> _secretResolver = secretResolver;
    private readonly JsonSerializerOptions _serializerOptions = serializerOptions ?? JsonConfig.ApiOptions;

    public ValueTask HandleRequest(EventSubWebhookRequestHeader requestHeader, Stream bodyStream)
    {
        throw new NotImplementedException();
    }

    public ValueTask HandleRequest(KeyValuePair<string, string> requestHeaders, string body)
    {
        throw new NotImplementedException();
    }

    private bool IsRequestFromTwitch(KeyValuePair<string, string> headers, string body)
    {
        JsonSerializer.Deserialize<EventSubNotification<JsonElement, JsonElement>>(body, _serializerOptions);
    }

    public ValueTask CallbackVerification(string challenge, EventSubSubscription subscription)
    {
        throw new NotImplementedException();
    }

    public ValueTask Notification(IEventSubNotification notification)
    {
        throw new NotImplementedException();
    }

    public ValueTask Revocation(EventSubSubscription subscription)
    {
        throw new NotImplementedException();
    }
}
