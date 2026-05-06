using TwitchySharp.EventSub.Webhooks.Responses;

namespace TwitchySharp.EventSub.Webhooks.WebhookMessageProcessors;
/// <summary>
/// Processes Twitch EventSub webhook requests.
/// </summary>
public interface IEventSubWebhookMessageProcessor
{
    /// <summary>
    /// Process a single Twitch EventSub webhook request, returning a response.
    /// </summary>
    /// <param name="requestHeader">The request header.</param>
    /// <param name="bodyStream">The request body.</param>
    /// <returns>A response object that can be mapped into an HTTP response.</returns>
    ValueTask<WebhookResponseData> HandleRequest(EventSubWebhookRequestHeader requestHeader, Stream bodyStream, CancellationToken ct = default);
}
