namespace TwitchySharp.EventSub.Webhooks.AspNetCore;

/// <summary>
/// Configuration options for Twitch EventSub Webhooks.
/// </summary>
public class TwitchEventSubWebhooksOptions
{
    /// <summary>
    /// The webhooks message handler to use.
    /// </summary>
    /// <remarks>
    /// The <see cref="IWebhookEventSubHandler"/> returned from this function will be notified of incoming
    /// EventSub webhook messages received at <see cref="TwitchWebhooksRouteExtensions.MapTwitchWebhooks(Microsoft.AspNetCore.Routing.IEndpointRouteBuilder, string)"/>.
    /// </remarks>
    public Func<IServiceProvider, IWebhookEventSubHandler>? MessageHandler { get; set; }
    /// <summary>
    /// The webhook secret resolver to use.
    /// </summary>
    /// <remarks>
    /// The function returned from this function will be used to verify the hash of incoming EventSub webhook messages to ensure they originate from Twitch.
    /// Twitch signs messages using the webhook secret used when creating the EventSub subscription.
    /// Ignore this option at your own risk: If left <see langword="null"/>, all incoming messages will be considered valid.
    /// </remarks>
    public Func<IServiceProvider, ResolveWebhookSecret>? SecretResolver { get; set; }
}
