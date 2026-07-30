using TwitchySharp.EventSub.Webhooks.Serialization;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Webhooks.Functional;

/// <summary>
/// A function processing EventSub webhook requests.
/// </summary>
/// <remarks>
/// The <see cref="WebhookRequestDeserializer"/> has a static <c>Create</c> method that can begin a processing pipeline.
/// </remarks>
/// <param name="request">The webhook request to process.</param>
/// <param name="ct">Cancellation token.</param>
/// <returns>A <see cref="ValueTask"/> containing a <see cref="Validation"/> containing the request.</returns>
public delegate ValueTask<Validation<WebhookRequestContent>> ProcessWebhookRequest(EventSubWebhookRequest request, CancellationToken ct);

public static partial class ProcessWebhookRequestExtensions
{
    public static ProcessWebhookRequest With(this ProcessWebhookRequest process, Func<ProcessWebhookRequest, ProcessWebhookRequest> with)
        => with(process);
}
