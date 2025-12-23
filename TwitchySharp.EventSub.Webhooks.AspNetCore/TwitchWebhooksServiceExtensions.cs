using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.NotificationConverters;
using TwitchySharp.EventSub.Webhooks.CallbackVerifiers;
using TwitchySharp.EventSub.Webhooks.MessageVerifiers;
using TwitchySharp.EventSub.Webhooks.SecretResolvers;
using TwitchySharp.EventSub.Webhooks.SignatureComputers;
using TwitchySharp.EventSub.Webhooks.WebhookMessageProcessors;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore;

public static class TwitchEventSubWebhooksServiceExtensions
{
    public static IServiceCollection AddTwitchEventSubWebhooksVerification(
        this IServiceCollection services, 
        Action<TwitchEventSubWebhooksVerificationOptions> configureOptions
        )
    {
        services.Configure(configureOptions);

        services.TryAddScoped<ITwitchWebhooksHeaderConverter, DefaultTwitchWebhooksHeaderConverter>();
        services.TryAddScoped<ITwitchEventSubWebhookSecretsResolver>(sp =>
            new FixedSecretTwitchWebhookSecretsResolver(sp.GetRequiredService<IOptions<TwitchEventSubWebhooksVerificationOptions>>().Value.Secret ?? throw new NotSupportedException($"The {nameof(TwitchEventSubWebhooksVerificationOptions.Secret)} must be configured in the {nameof(TwitchEventSubWebhooksVerificationOptions)}."))
            );
        services.TryAddScoped<IComputeTwitchWebhookSignature, DefaultTwitchWebhookCrypto>();
        services.TryAddScoped<ITwitchWebhookMessageVerifier>(sp => 
            new DefaultTwitchWebhookMessageVerifier(sp.GetRequiredService<ITwitchEventSubWebhookSecretsResolver>())
            );
        return services;
    }

    public static IServiceCollection AddTwitchEventSubWebhooks(
        this IServiceCollection services, 
        Action<TwitchEventSubWebhooksOptions>? configureOptions = null
        )
    {
        if (configureOptions is not null)
            services.Configure(configureOptions);

        services.TryAddScoped<ITwitchWebhooksHeaderConverter, DefaultTwitchWebhooksHeaderConverter>();
        services.TryAddScoped<IWebhookCallbackVerifier, DefaultWebhookCallbackVerifier>();
        services.TryAddScoped<INotificationConverter>(sp => 
            new NotificationConverter(sp.GetService<IOptions<TwitchEventSubWebhooksOptions>>()?.Value.NotificationTypes)
            );
        services.TryAddScoped<IEventSubWebhookMessageProcessor>(sp => 
            new DefaultEventSubWebhookMessageProcessor(
                sp.GetRequiredService<IWebhookEventSubHandler>(), // You must register an IWebhookEventSubHandler implementation separately
                sp.GetService<INotificationConverter>(),
                sp.GetService<IWebhookCallbackVerifier>(),
                sp.GetService<IOptions<TwitchEventSubWebhooksOptions>>()?.Value.JsonSerializerOptions
                )
            );

        services.AddHostedService<TwitchWebhooksConfigurationValidator>();

        return services;
    }
}
