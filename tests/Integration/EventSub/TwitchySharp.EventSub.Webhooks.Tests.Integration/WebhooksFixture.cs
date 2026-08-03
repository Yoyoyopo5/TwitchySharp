using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Webhooks.AspNetCore;
using TwitchySharp.EventSub.Webhooks.Functional;
using TwitchySharp.EventSub.Webhooks.Crypto;
using TwitchySharp.EventSub.Webhooks.Idempotency;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Webhooks.Tests.Integration;

public class WebhooksFixture : IAsyncLifetime
{
    public const string WEBHOOKS_SECRET = "super_secure_secret";
    public const string WEBHOOKS_PATH = "/test-webhooks";

    private readonly HashSet<WebhookMessageId> IdempotencyCache = [];
    public WebhookSecret WebhooksSecret { get; init; } = new(WEBHOOKS_SECRET);
    public IWebHost Host { get; } // TODO: Convert to WebApplication
    public TestHandler Handler { get; } = new();

    public WebhooksFixture()
    {
        Host = ConfigureWebHost(new WebHostBuilder()).Build();
    }

    public ValueTask<IResult> SimulateRequest(HttpContext context, CancellationToken ct)
    {
        using IServiceScope scope = Host.Services.CreateScope();
        return scope.ServiceProvider.GetRequiredService<HandleAspNetWebhookRequest>()(context, ct);
    }

    private IWebHostBuilder ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseTestServer();
        builder.ConfigureServices((ctx, s) =>
        {
            s.AddRouting();
            s.AddTwitchEventSubWebhooks(sp => pipeline => 
                pipeline
                    .WithHashValidation(WebhookHashVerifier.Create((subscription, ct) => ValueTask.FromResult<WebhookSecret?>(WebhooksSecret)))
                    .WithIdempotentRequests((messageId, ct) =>
                    {
                        if (IdempotencyCache.Contains(messageId))
                            return ValueTask.FromResult(true);
                        IdempotencyCache.Add(messageId);
                        return ValueTask.FromResult(false);
                    })
                    .MapCallbackVerification((subscription, challenge, ct) => Handler.OnCallbackVerification(subscription, challenge, ct))
                    .MapSubscriptionRevoked((subscription, ct) => Handler.OnSubscriptionRevoked(subscription, ct))
                    .MapError((error, ct) => Handler.OnError(error, ct))
                    .MapNotification<IEventSubNotification>((notification, ct) => Handler.OnNotified(notification, ct))
            );
        });
        builder.Configure(app =>
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapTwitchWebhooks(WEBHOOKS_PATH);
            });
        });

        return builder;
    }

    public async ValueTask InitializeAsync()
        => await Host.StartAsync(TestContext.Current.CancellationToken);

    public async ValueTask DisposeAsync()
    {
        await Host.StopAsync(TestContext.Current.CancellationToken);
        Host.Dispose();
    }
}

public static class WebhooksSecretExtensions
{
    public static byte[] ToBytes(this WebhookSecret secret)
        => Encoding.UTF8.GetBytes(secret.Value);
}

public class TestHandler
{
    public EventSubSubscription? LastCallback { get; set; }
    public string? LastCallbackChallenge { get; set; }
    public EventSubSubscription? LastRevoked { get; set; }
    public IEventSubNotification? LastNotification { get; set; }
    public Error? LastError { get; set; }

    public ValueTask OnNotified(IEventSubNotification notification, CancellationToken ct = default)
    {
        LastNotification = notification;
        return ValueTask.CompletedTask;
    }

    public ValueTask OnSubscriptionRevoked(EventSubSubscription revokedSubscription, CancellationToken ct = default)
    {
        LastRevoked = revokedSubscription;
        return ValueTask.CompletedTask;
    }

    public ValueTask OnCallbackVerification(EventSubSubscription newSubscription, string challenge, CancellationToken ct = default)
    {
        LastCallback = newSubscription;
        LastCallbackChallenge = challenge;
        return ValueTask.CompletedTask;
    }
    public ValueTask OnError(Error error, CancellationToken ct = default)
    {
        LastError = error;
        return ValueTask.CompletedTask;
    }
}
