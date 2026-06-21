using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using TwitchySharp.EventSub.Webhooks.Crypto;
using TwitchySharp.EventSub.Webhooks.Functional;
using TwitchySharp.EventSub.Webhooks.Idempotency;
using TwitchySharp.EventSub.Webhooks.Serialization;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore;

public static class TwitchEventSubWebhooksServiceExtensions
{
    /// <summary>
    /// Add and configure the Twitch EventSub webhook message processing pipeline.
    /// </summary>
    /// <remarks>
    /// This is the overload for defining a custom processing pipeline.
    /// For easier first-time setup, consider using <see cref="AddTwitchEventSubWebhooks(IServiceCollection, Action{TwitchEventSubWebhooksOptions})"/>.
    /// </remarks>
    /// <param name="services">The service collection to add the service to.</param>
    /// <param name="configurePipeline">Configure the processing pipeline with middleware.</param>
    /// <param name="createPipeline">Create the processing pipeline (e.g. via <see cref="WebhookRequestDeserializer"/>)</param>
    /// <returns></returns>
    public static IServiceCollection AddTwitchEventSubWebhooks(
        this IServiceCollection services,
        Func<IServiceProvider, Func<ProcessWebhookRequest, ProcessWebhookRequest>> configurePipeline,
        Func<IServiceProvider, ProcessWebhookRequest>? createPipeline
        )
    {
        services.TryAddScoped(sp => AspNetWebhookRequestHandler.Create(
            EventSubWebhookHeaderReader.Read,
            configurePipeline(sp)(createPipeline?.Invoke(sp) ?? WebhookRequestDeserializer.Create()),
            sp.GetService<ILoggerFactory>()
            ));
        return services;
    }

    /// <summary>
    /// Add and configure the Twitch EventSub webhook message processing pipeline.
    /// </summary>
    /// <param name="services">The service collection to add the service to.</param>
    /// <param name="configure">Configure the service options.</param>
    /// <returns>The service collection.</returns>
    // Note that this isn't actually tied to AspNetCore but rather Microsoft.Extensions.Hosting
    // We don't want to add that dependency to the Webhooks lib, nor do we want to create another lib (right now), so it goes here.
    public static IServiceCollection AddTwitchEventSubWebhooks(
        this IServiceCollection services,
        Action<TwitchEventSubWebhooksOptions>? configure = null
        )
    {
        TwitchEventSubWebhooksOptions options = new();
        configure?.Invoke(options);

        services.AddTwitchEventSubWebhooks(
            configurePipeline: sp =>
            {
                VerifyWebhookHash? verify = (options.SecretResolver is not null ? WebhookHashVerifier.Create(options.SecretResolver(sp)) : null) ?? sp.GetService<VerifyWebhookHash>();
                IWebhookEventSubHandler? handler = options.MessageHandler?.Invoke(sp) ?? sp.GetService<IWebhookEventSubHandler>();
                Func<WebhookMessageId, CancellationToken, ValueTask<bool>>? idempotency = options.IdempotencyCache?.Invoke(sp) ?? sp.GetService<Func<WebhookMessageId, CancellationToken, ValueTask<bool>>>();

                return pipeline =>
                {
                    pipeline = verify is not null ? pipeline.WithHashValidation(verify) : pipeline;
                    pipeline = idempotency is not null ? pipeline.WithIdempotentRequests(idempotency) : pipeline;
                    pipeline = handler is not null ? pipeline.WithHandler(handler) : pipeline;

                    return pipeline;
                };
            },
            createPipeline: sp => WebhookRequestDeserializer.Create(options.NotificationDeserializer?.Invoke(sp), options.MessageDeserializerOptions)
            );

        services.AddConfigValidation(options);

        return services;
    }

    private static IServiceCollection AddConfigValidation(this IServiceCollection services, TwitchEventSubWebhooksOptions options)
    {
        WebhooksConfigValidationContext validationContext = new()
        {
            HasHandler = services.Any(d => d.ServiceType == typeof(IWebhookEventSubHandler)) || options.MessageHandler is not null,
            HasVerifier = services.Any(d => d.ServiceType == typeof(VerifyWebhookHash)) || options.SecretResolver is not null,
            HasIdempotency = services.Any(d => d.ServiceType == typeof(Func<WebhookMessageId, CancellationToken, ValueTask<bool>>)) || options.IdempotencyCache is not null
        };

        services.AddHostedService(sp => new TwitchWebhooksConfigurationValidator(
            context: validationContext,
            loggerFactory: sp.GetService<ILoggerFactory>()
            ));

        return services;
    }
}
