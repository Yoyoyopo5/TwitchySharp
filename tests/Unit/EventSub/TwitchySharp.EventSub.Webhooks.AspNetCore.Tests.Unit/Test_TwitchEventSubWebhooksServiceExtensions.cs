using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Webhooks.Crypto;
using TwitchySharp.EventSub.Webhooks.Functional;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore.Tests.Unit;

public class Test_TwitchEventSubWebhooksServiceExtensions
{
    // We may want to move some of this into integration testing
    // since the AddTwitchEventSubWebhooks call is doing significant
    // orchestration between different units.

    private static ServiceProvider BuildMockServiceProvider(Action<TwitchEventSubWebhooksOptions>? configure = null)
        => new ServiceCollection().AddSingleton<ILoggerFactory, StubLoggerFactory>().AddTwitchEventSubWebhooks(configure).BuildServiceProvider();

    private const string CORRECT_SECRET = "super_secure_secret";

    private const string CALLBACK_VERIFICATION_JSON = """
        {
            "challenge": "fake-challenge",
            "subscription": {
                "id": "12345",
                "status": "webhook_callback_verification_pending",
                "type": "channel.chat.clear",
                "version": "1",
                "cost": 1,
                "condition": {
                    "broadcaster_user_id": "1",
                    "user_id": "1"
                },
                "transport": {
                    "method": "webhook",
                    "callback": "https://fake-callback.com"
                },
                "created_at": "2026-06-21T10:46:12.26371Z"
            }
        }
        """;

    private static DefaultHttpContext CreateStubContext()
    {
        DefaultHttpContext context = new();

        context.Request.Headers.Add(new("Twitch-Eventsub-Message-Id", new("12345")));
        context.Request.Headers.Add(new("Twitch-Eventsub-Message-Type", new(EventSubWebhookMessageType.WebhookCallbackVerification)));
        context.Request.Headers.Add(new("Twitch-Eventsub-Message-Signature", new("93c64c8f37e5d13cd95963d28b9c92d7d0c0283d343443d01e351b39d8295968")));
        context.Request.Headers.Add(new("Twitch-Eventsub-Message-Timestamp", new("1248174")));
        context.Request.Headers.Add(new("Twitch-Eventsub-Subscription-Type", new(EventSubSubscriptionType.ChannelChatClear.Type)));
        context.Request.Headers.Add(new("Twitch-Eventsub-Subscription-Version", new(EventSubSubscriptionType.ChannelChatClear.Version)));

        context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(CALLBACK_VERIFICATION_JSON));

        return context;
    }

    private record TrackingHandler(Action OnInvoked) : IWebhookEventSubHandler
    {
        private ValueTask Notify()
        {
            OnInvoked();
            return ValueTask.CompletedTask;
        }
        public ValueTask OnCallbackVerification(EventSubSubscription newSubscription, string challenge, CancellationToken ct = default)
            => Notify();
        public ValueTask OnError(Error error, CancellationToken ct = default)
            => Notify();
        public ValueTask OnNotified(IEventSubNotification notification, CancellationToken ct = default)
            => Notify();
        public ValueTask OnSubscriptionRevoked(EventSubSubscription revokedSubscription, CancellationToken ct = default)
            => Notify();
    }

    [Fact]
    public void AddTwitchEventSubWebhooks_GetService_ReturnsHandleAspNetWebhookRequest()
    {
        ServiceProvider sp = BuildMockServiceProvider();

        HandleAspNetWebhookRequest? pipeline = sp.GetService<HandleAspNetWebhookRequest>();

        Assert.NotNull(pipeline);
    }

    private class StubLoggerFactory : ILoggerFactory
    {
        public StubLogger Logger { get; } = new();

        public void AddProvider(ILoggerProvider provider) { }
        public ILogger CreateLogger(string categoryName) => Logger;
        public void Dispose() { }
    }

    private class StubLogger : ILogger
    {
        private class StubDisposable : IDisposable
        {
            public void Dispose() { }
        }

        private readonly List<string> _logs = [];
        public IReadOnlyList<string> Logs => _logs;

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => new StubDisposable();
        public bool IsEnabled(LogLevel logLevel) => true;
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter) => _logs.Add(formatter(state, exception));
    }

    [Fact]
    public async Task AddTwitchEventSubWebhooks_NoConfiguration_ConfigurationValidatorLogsWarnings()
    {
        ServiceProvider sp = BuildMockServiceProvider();

        await sp.GetServices<IHostedService>().OfType<TwitchWebhooksConfigurationValidator>().Single().StartAsync(TestContext.Current.CancellationToken);

        IReadOnlyList<string> logs = (sp.GetRequiredService<ILoggerFactory>() as StubLoggerFactory)!.Logger.Logs;

        Assert.Equal(3, logs.Count);
    }

    [Fact]
    public async Task HandleWebhookRequest_WithHandlerConfigured_InvokesHandler()
    {
        bool handlerInvoked = false;

        TrackingHandler handler = new(() => handlerInvoked = true);

        ServiceProvider sp = BuildMockServiceProvider(options =>
        {
            options.MessageHandler = (_) => handler;
        });

        HandleAspNetWebhookRequest pipeline = sp.GetRequiredService<HandleAspNetWebhookRequest>();

        await pipeline(CreateStubContext(), TestContext.Current.CancellationToken);

        Assert.True(handlerInvoked);
    }

    [Fact]
    public async Task HandleWebhookRequest_WithSecretResolverConfigured_InvokesSecretResolver()
    {
        bool secretResolverCalled = false;

        ServiceProvider sp = BuildMockServiceProvider(options =>
        {
            options.SecretResolver = _ => (_, _) =>
            {
                secretResolverCalled = true;
                return ValueTask.FromResult<WebhookSecret?>(new WebhookSecret(CORRECT_SECRET));
            };
        });

        HandleAspNetWebhookRequest pipeline = sp.GetRequiredService<HandleAspNetWebhookRequest>();

        await pipeline(CreateStubContext(), TestContext.Current.CancellationToken);

        Assert.True(secretResolverCalled);
    }

    [Fact]
    public async Task HandleWebhookRequest_WithIdempotencyConfigured_InvokesIdempotency()
    {
        bool idempotencyCalled = false;

        ServiceProvider sp = BuildMockServiceProvider(options =>
        {
            options.IdempotencyCache = _ => (_, _) =>
            {
                idempotencyCalled = true;
                return ValueTask.FromResult(false);
            };
        });

        HandleAspNetWebhookRequest pipeline = sp.GetRequiredService<HandleAspNetWebhookRequest>();

        await pipeline(CreateStubContext(), TestContext.Current.CancellationToken);

        Assert.True(idempotencyCalled);
    }

    [Fact]
    public async Task HandleWebhookRequest_WithAllConfigured_InvokesInCorrectOrder()
    {
        int order = 1;

        int secretResolverOrder = 0;
        int idempotencyCacheOrder = 0;
        int handlerOrder = 0;

        TrackingHandler handler = new(() => handlerOrder = order++);

        ServiceProvider sp = BuildMockServiceProvider(options =>
        {
            options.SecretResolver = _ => (_, _) =>
            {
                secretResolverOrder = order++;
                return ValueTask.FromResult<WebhookSecret?>(new WebhookSecret(CORRECT_SECRET));
            };
            options.IdempotencyCache = _ => (_, _) =>
            {
                idempotencyCacheOrder = order++;
                return ValueTask.FromResult(false);
            };
            options.MessageHandler = _ => handler;
        });

        HandleAspNetWebhookRequest pipeline = sp.GetRequiredService<HandleAspNetWebhookRequest>();

        await pipeline(CreateStubContext(), TestContext.Current.CancellationToken);

        Assert.Equal(1, idempotencyCacheOrder);
        Assert.Equal(2, secretResolverOrder);
        Assert.Equal(3, handlerOrder);
    }
}
