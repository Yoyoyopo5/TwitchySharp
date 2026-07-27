using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Websocket;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Tests.E2E;

public class TestHandler : IWebsocketEventSubHandler
{
    private readonly TaskCompletionSource<EventSubWebsocketSession> _welcomeReceived = new();
    private TaskCompletionSource<IEventSubNotification> _notificationReceived = new();
    public EventSubWebsocketSession? Session { get; private set; }
    public bool ReceivedKeepalive { get; private set; } = false;
    public IEventSubNotification? ReceivedNotification { get; private set; }
    public EventSubSubscription? ReceivedRevocation { get; private set; }
    public Error? ReceivedError { get; private set; }

    public Task<EventSubWebsocketSession> WaitForWelcome(CancellationToken ct = default)
        => _welcomeReceived.Task.WaitAsync(ct);

    public async Task<IEventSubNotification> WaitForNotification(CancellationToken ct = default)
    {
        IEventSubNotification notification = await _notificationReceived.Task.WaitAsync(ct);
        // Thundering herd issue here.
        _notificationReceived = new();
        return notification;
    }

    public ValueTask OnWelcome(EventSubWebsocketSession session, CancellationToken ct = default)
    {
        _welcomeReceived.TrySetResult(session);
        Session = session;
        return ValueTask.CompletedTask;
    }

    public ValueTask OnError(Error exception, CancellationToken ct = default)
    {
        ReceivedError = exception;
        return ValueTask.CompletedTask;
    }

    public ValueTask OnKeepalive(CancellationToken ct = default)
    {
        ReceivedKeepalive = true;
        return ValueTask.CompletedTask;
    }

    public ValueTask OnNotified(IEventSubNotification notification, CancellationToken ct = default)
    {
        _notificationReceived.TrySetResult(notification);
        ReceivedNotification = notification;
        return ValueTask.CompletedTask;
    }

    public ValueTask OnSubscriptionRevoked(EventSubSubscription subscription, CancellationToken ct = default)
    {
        ReceivedRevocation = subscription;
        return ValueTask.CompletedTask;
    }

    public ValueTask OnReconnect(EventSubReconnectSession reconnect, CancellationToken ct = default)
        => ValueTask.CompletedTask;
}
