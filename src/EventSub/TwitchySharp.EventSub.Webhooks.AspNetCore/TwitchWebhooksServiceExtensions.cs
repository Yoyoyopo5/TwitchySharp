using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using TwitchySharp.EventSub.Webhooks.Crypto;
using TwitchySharp.EventSub.Webhooks.Functional;
using TwitchySharp.EventSub.Webhooks.Serialization;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore;

internal delegate ValueTask<IResult> HandleAspNetWebhookRequest(HttpContext context, CancellationToken ct);

public static class TwitchEventSubWebhooksServiceExtensions
{
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
        Action<TwitchEventSubWebhooksOptions> configure
        )
    {
        TwitchEventSubWebhooksOptions options = new();
        configure(options);

        services.AddSingleton<ReadWebhookHeader>(_ => EventSubWebhookHeaderReader.Read);

        // TODO:
        // This needs to be more fluently configurable.
        // We should return something from this function that allows consumers to compose a pipeline,
        // similar to delegating handlers with .AddHttpClient.
        services.TryAddScoped(sp =>
        {
            ProcessWebhookRequest pipeline = WebhookRequestDeserializer.Create(options.NotificationDeserializer?.Invoke(sp), options.MessageDeserializerOptions);
            VerifyWebhookHash? verify = (options.SecretResolver is not null ? WebhookHashVerifier.Create(options.SecretResolver(sp)) : null) ?? sp.GetService<VerifyWebhookHash>();
            IWebhookEventSubHandler? handler = options.MessageHandler?.Invoke(sp) ?? sp.GetService<IWebhookEventSubHandler>();

            // Pipeline builds from inner to outer.
            pipeline = verify is not null ? pipeline.WithHashValidation(verify) : pipeline;
            pipeline = handler is not null ? pipeline.WithHandler(handler) : pipeline;

            return pipeline;
        });

        services.AddConfigValidation(options);

        return services;
    }

    private static IServiceCollection AddConfigValidation(this IServiceCollection services, TwitchEventSubWebhooksOptions options)
    {
        WebhooksConfigValidationContext validationContext = new()
        {
            HasHandler = services.Any(d => d.ServiceType == typeof(IWebhookEventSubHandler)) || options.MessageHandler is not null,
            HasVerifier = services.Any(d => d.ServiceType == typeof(VerifyWebhookHash)) || options.SecretResolver is not null
        };

        services.AddHostedService(sp => new TwitchWebhooksConfigurationValidator(
            context: validationContext,
            loggerFactory: sp.GetService<ILoggerFactory>()
            ));

        return services;
    }
}
