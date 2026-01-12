using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api;
using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Notifications;
using TwitchySharp.EventSub.Websocket.Clients.Websocket.Client;
using TwitchySharp.EventSub.Websocket.Messages.Payloads;

namespace TwitchySharp.EventSub.Websocket.Tests.E2E;
public class WebsocketFixture : IDisposable
{
    public TestHandler Handler { get; }
    public WebsocketClientEventSubWebsocketClient Client { get; }
    public TwitchApi Api { get; }
    public WebsocketSecrets Secrets { get; }

    public WebsocketFixture()
    {
        Handler = new TestHandler();
        Client = new WebsocketClientEventSubWebsocketClient(Handler);
        Api = new TwitchApi(new());
        Secrets = new ConfigurationBuilder().AddUserSecrets(Assembly.GetExecutingAssembly()).Build().GetRequiredSection("Secrets").Get<WebsocketSecrets>()!;

        CancellationTokenSource cts = new(2000);
        Client.StartAsync(cts.Token).GetAwaiter().GetResult();
    }

    public void Dispose()
    {
        CancellationTokenSource cts = new(1000);
        Client.StopAsync(cts.Token).GetAwaiter().GetResult();
        Client.Dispose();
    }
}

public record WebsocketSecrets
{
    public required string ClientId { get; init; }
    public required string UserAccessToken { get; init; }
}

public class TestHandler : IWebsocketEventSubHandler
{
    public EventSubWebsocketSession? ReceivedConnected { get; private set; }
    public bool ReceivedKeepalive { get; private set; } = false;
    public IEventSubNotification? ReceivedNotification { get; private set; }
    public EventSubSubscription? ReceivedRevocation { get; private set; }
    public Exception? ReceivedException { get; private set; }

    public ValueTask OnConnected(EventSubWebsocketSession session, CancellationToken ct = default)
    {
        ReceivedConnected = session;
        return ValueTask.CompletedTask;
    }

    public ValueTask OnException(Exception exception, CancellationToken ct = default)
    {
        ReceivedException = exception;
        return ValueTask.CompletedTask;
    }

    public ValueTask OnKeepalive(CancellationToken ct = default)
    {
        ReceivedKeepalive = true;
        return ValueTask.CompletedTask;
    }

    public ValueTask OnNotified(IEventSubNotification notification, CancellationToken ct = default)
    {
        ReceivedNotification = notification;
        return ValueTask.CompletedTask;
    }

    public ValueTask OnSubscriptionRevoked(EventSubSubscription subscription, CancellationToken ct = default)
    {
        ReceivedRevocation = subscription;
        return ValueTask.CompletedTask;
    }

    public ValueTask OnReconnected(EventSubReconnectSession reconnect, CancellationToken ct = default)
        => ValueTask.CompletedTask;
}
