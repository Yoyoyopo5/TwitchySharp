using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore;

internal readonly record struct WebhooksConfigValidationContext
{
    public required bool HasHandler { get; init; }
    public required bool HasVerifier { get; init; }
    public required bool HasIdempotency { get; init; }
}

internal class TwitchWebhooksConfigurationValidator(WebhooksConfigValidationContext context, ILoggerFactory? loggerFactory = null) : IHostedService
{
    private readonly WebhooksConfigValidationContext _context = context;

    public Task StartAsync(CancellationToken ct)
    {
        ILogger? logger = loggerFactory?.CreateLogger("TwitchySharp.EventSub.Webhooks.AspNetCore");

        if (!_context.HasHandler)
            logger?.LogWarning("Twitch EventSub Webhooks does not have a message handler and will default to an empty handler (i.e. no side effects will be run on events received). Use the `configure` parameter of `AddTwitchEventSubWebhooks` to configure a message handler.");

        if (!_context.HasVerifier)
            logger?.LogWarning("Security Warning: Twitch EventSub Webhooks does not have a hash verifier and will default to not verifying the hashes of incoming webhook requests (i.e. verifying that the requests actually came from Twitch). Use the `configure` parameter of `AddTwitchEventSubWebhooks` to configure a webhook secret resolver.");

        if (!_context.HasIdempotency)
            logger?.LogInformation("Twitch EventSub Webhooks does not have message id idempotency configured. If Twitch sends a duplicate notification, it will not be ignored. Use the `configure` parameter of `AddTwitchEventSubWebhooks` to configure an idempotency cache.");

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken ct) => Task.CompletedTask;
}
