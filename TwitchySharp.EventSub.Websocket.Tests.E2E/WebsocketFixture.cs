using Microsoft.Extensions.Configuration;
using System.Reflection;
using TwitchySharp.Api;
using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Notifications;
using TwitchySharp.EventSub.Websocket.Clients.Websocket.Client;
using TwitchySharp.EventSub.Websocket.Messages.Payloads;
using TwitchySharp.Api.AuthorizationResolution;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.EventSub.Websocket.Tests.E2E;

public sealed class WebsocketFixture : IAsyncLifetime
{
    public TestHandler Handler { get; }
    public WebsocketClientEventSubWebsocketClient Websocket { get; }
    public ITwitchClient Api { get; }
    
    public WebsocketConfig Config { get; }

    public TwitchIdentity.Client Client { get; }
    public TwitchIdentity.User AuthorizedBroadcaster { get; }
    private readonly AccessTokenDetails.User _broadcasterAccessTokenDetails;
    private readonly ITwitchClient _authClient = new TwitchClientBuilder().Build();

    private static readonly IConfiguration _config 
        = new ConfigurationBuilder()
        .AddUserSecrets(Assembly.GetExecutingAssembly())
        .Build();

    public WebsocketFixture()
    {
        Handler = new();
        Websocket = new(Handler);

        Config = _config.GetRequiredSection("WebsocketFixture").Get<WebsocketConfig>() ?? throw new InvalidOperationException($"Could not bind configuration to {nameof(WebsocketConfig)}");

        Client = new(Config.Client.Id);
        AuthorizedBroadcaster = new(Config.AuthorizedBroadcaster.Id, Config.Client.Id);
        _broadcasterAccessTokenDetails = Config.AuthorizedBroadcaster.AccessToken;

        Api = new TwitchClientBuilder()
            .WithAuthorizationResolution(new TwitchAuthorizationResolutionOptions()
            {
                FallbackClientIdResolver = (ctx, _) => ValueTask.FromResult(Client.ClientId),
            }
            .ConfigureIdentityTokenResolution(new AppAccessTokenResolutionOptions()
            {
                AuthenticationClient = _authClient,
                ClientSecretResolver = (ctx, _) => ValueTask.FromResult<ClientSecret?>(Config.Client.Secret)
            })
            .ConfigureIdentityTokenResolution(new UserAccessTokenResolutionOptions()
            {
                GetCachedToken = (ctx, _) => ValueTask.FromResult<AccessTokenDetails.User?>(_broadcasterAccessTokenDetails),
                AuthenticationClient = _authClient,
                ClientSecretResolver = (ctx, _) => ValueTask.FromResult<ClientSecret?>(Config.Client.Secret)
            }))
            .Build();
    }

    public async ValueTask DisposeAsync()
    {
        await Websocket.StopAsync(TestContext.Current.CancellationToken);
        Websocket.Dispose();
    }

    public async ValueTask InitializeAsync()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        using (TestContext.Current.CancellationToken.Register(() => Handler.Connected.TrySetCanceled(ct)))
        {
            await Websocket.StartAsync(ct);
            await Handler.Connected.Task;
        }
    }
}

public record WebsocketConfig
{
    public record ClientConfig
    {
        public required ClientId Id { get; set; }
        public required ClientSecret Secret { get; set; }
    }

    public record AuthorizedBroadcasterConfig
    {
        public required UserId Id { get; set; }
        public required AccessTokenDetails.User AccessToken { get; set; }
    }

    public required ClientConfig Client { get; set; }
    public required AuthorizedBroadcasterConfig AuthorizedBroadcaster { get; set; }
}

public class TestHandler : IWebsocketEventSubHandler
{
    public TaskCompletionSource<EventSubWebsocketSession> Connected { get; } = new();
    public EventSubWebsocketSession? ReceivedConnected { get; private set; }
    public bool ReceivedKeepalive { get; private set; } = false;
    public IEventSubNotification? ReceivedNotification { get; private set; }
    public EventSubSubscription? ReceivedRevocation { get; private set; }
    public Exception? ReceivedException { get; private set; }

    public ValueTask OnConnected(EventSubWebsocketSession session, CancellationToken ct = default)
    {
        Connected.TrySetResult(session);
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
