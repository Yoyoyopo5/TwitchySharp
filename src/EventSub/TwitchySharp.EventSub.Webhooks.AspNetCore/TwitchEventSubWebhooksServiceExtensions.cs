using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using TwitchySharp.EventSub.Webhooks.Functional;
using TwitchySharp.EventSub.Webhooks.Serialization;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore;

public static class TwitchEventSubWebhooksServiceExtensions
{
    /// <summary>
    /// Add and configure the Twitch EventSub webhook message processing pipeline.
    /// </summary>
    /// <param name="services">The service collection to add the service to.</param>
    /// <param name="configurePipeline">Configure the processing pipeline with middleware.</param>
    /// <param name="createPipeline">Create the processing pipeline (e.g. via <see cref="WebhookRequestDeserializer"/>)</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddTwitchEventSubWebhooks(
        this IServiceCollection services,
        Func<IServiceProvider, Func<ProcessWebhookRequest, ProcessWebhookRequest>> configurePipeline,
        Func<IServiceProvider, ProcessWebhookRequest>? createPipeline = null
        )
    {
        services.TryAddScoped(sp => AspNetWebhookRequestHandler.Create(
            EventSubWebhookHeaderReader.Read,
            configurePipeline(sp)(createPipeline?.Invoke(sp) ?? WebhookRequestDeserializer.Create()),
            sp.GetService<ILoggerFactory>()
            ));
        return services;
    }
}
