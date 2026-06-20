using System.Collections.Immutable;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TwitchySharp.EventSub.Webhooks.Functional;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore.Tests.Unit;

public class Test_AspNetWebhookRequestHandler
{
    private readonly static EventSubWebhookRequestHeader FakeHeader = new()
    {
        TwitchEventsubMessageId = new("12345"),
        TwitchEventsubMessageType = EventSubWebhookMessageType.Notification,
        TwitchEventsubSubscriptionType = new("fake-subscription"),
        TwitchEventsubSubscriptionVersion = new("1"),
        TwitchEventsubMessageTimestamp = new("21234890"),
        TwitchEventsubMessageSignature = new("abcde")
    };

    private class StubDisposable : IDisposable
    {
        public void Dispose() { }
    }

    private class StubLogger : ILogger
    {
        public string? LastLog { get; private set; }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
            => new StubDisposable();
        public bool IsEnabled(LogLevel logLevel)
            => true;
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
            => LastLog = formatter(state, exception);
    }

    private class StubLoggerFactory : ILoggerFactory
    {
        public StubLogger Logger { get; } = new StubLogger();

        public void AddProvider(ILoggerProvider provider) { }
        public ILogger CreateLogger(string categoryName)
            => Logger;
        public void Dispose() { }
    }

    private record StubWebhookRequestContent : WebhookRequestContent;

    private static EventSubSubscription FakeSubscription { get; } = new()
    {
        Id = new("f1c2a387-161a-49f9-a165-0f21d7a4e1c4"),
        Status = EventSubSubscriptionStatus.Enabled,
        Type = new("channel.follow"),
        Version = new("1"),
        Cost = 1,
        Condition = new Dictionary<string, object>() { { "broadcaster_user_id", "12826" } }.ToImmutableDictionary(),
        CreatedAt = DateTimeOffset.Parse("2019-11-16T10:11:12.634234626Z"),
        Transport = new() { Method = EventSubTransportMethod.Webhook, Callback = new("https://example.com/webhooks/callback") }
    };

    private static StubWebhookRequestContent CreateRequestContent()
        => new() { Subscription = FakeSubscription };

    [Fact]
    public async Task Create_HandleAspNetWebhookRequest_ValidHeader_ReturnProcessResult()
    {
        StubLoggerFactory loggerFactory = new();

        HandleAspNetWebhookRequest stubProcess = AspNetWebhookRequestHandler.Create(
            _ => new Validation<EventSubWebhookRequestHeader>(FakeHeader),
            (_, _) => ValueTask.FromResult<Validation<WebhookRequestContent>>(CreateRequestContent()),
            loggerFactory
            );

        IResult result = await stubProcess(new DefaultHttpContext(), CancellationToken.None);

        Assert.Null(loggerFactory.Logger.LastLog);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task Create_HandleAspNetWebhookRequest_InvalidHeader_LogWithMissingHeaderAndSkipProcess()
    {
        const string FAKE_MISSING_HEADER = "example-header";

        StubLoggerFactory loggerFactory = new();
        bool ranProcess = false;

        HandleAspNetWebhookRequest stubProcess = AspNetWebhookRequestHandler.Create(
            _ => new Validation<EventSubWebhookRequestHeader>(new EventSubWebhookHeaderReader.MissingHeadersError([FAKE_MISSING_HEADER])),
            (_, _) =>
            {
                ranProcess = true;
                return ValueTask.FromResult<Validation<WebhookRequestContent>>(CreateRequestContent());
            },
            loggerFactory
            );

        IResult result = await stubProcess(new DefaultHttpContext(), CancellationToken.None);

        Assert.Contains(FAKE_MISSING_HEADER, loggerFactory.Logger.LastLog);
        Assert.NotNull(result);
        Assert.False(ranProcess);
    }
}
