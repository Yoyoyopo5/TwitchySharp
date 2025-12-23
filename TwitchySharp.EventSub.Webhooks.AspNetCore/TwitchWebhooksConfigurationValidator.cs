using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Webhooks.MessageVerifiers;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore;

internal class TwitchWebhooksConfigurationValidator(IServiceProvider serviceProvider, ILogger<TwitchWebhooksConfigurationValidator>? logger = null) : IHostedService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public Task StartAsync(CancellationToken ct)
    {
        if (_serviceProvider.GetService<ITwitchWebhookMessageVerifier>() is null)
            logger?.LogWarning("Security Warning: Twitch webhook requests will not be validated with secrets. Please use the AddTwitchEventSubWebhooksVerification IServiceCollection extension method, register an {VerifierType} in the service provider for improved security, or disable this warning by registering a stub verifier.", nameof(ITwitchWebhookMessageVerifier));
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken ct) => Task.CompletedTask;
}
