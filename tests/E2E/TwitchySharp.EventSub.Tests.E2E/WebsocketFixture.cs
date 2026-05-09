using System.Reflection;
using Microsoft.Extensions.Configuration;
using TwitchySharp.Api;
using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Notifications;
using TwitchySharp.EventSub.Websocket.Clients.Websocket.Client;
using TwitchySharp.EventSub.Websocket.Messages.Payloads;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.EventSub.Websocket.Tests.E2E;

public sealed class WebsocketFixture : IAsyncLifetime
{
    private static readonly HttpClient _httpClient = new();
    public TestHandler Handler { get; }
    public WebsocketClientEventSubWebsocketClient Websocket { get; }
    public ITwitchClient Client { get; }
    public WebsocketConfig Config { get; }

    public TwitchIdentity.Client ClientIdentity { get; }
    public TwitchIdentity.User AuthorizedBroadcaster { get; }
    private AccessTokenDetails.User _broadcasterAccessTokenDetails;
    private AccessTokenDetails.App? _appAccessTokenDetails;
    private readonly ITwitchClient _authClient = new TwitchClientBuilder() { HttpClient = _httpClient }.Build();

    private static readonly IConfiguration _config
        = new ConfigurationBuilder()
        .AddUserSecrets(Assembly.GetExecutingAssembly())
        .Build();

    public WebsocketFixture()
    {
        Handler = new();
        Websocket = new(Handler);

        Config = _config.GetRequiredSection("WebsocketFixture").Get<WebsocketConfig>() ?? throw new InvalidOperationException($"Could not bind configuration to {nameof(WebsocketConfig)}");

        ClientIdentity = new(Config.Client.Id);
        AuthorizedBroadcaster = Config.UserAccessTokenDetails.Identity;
        _broadcasterAccessTokenDetails = Config.UserAccessTokenDetails;

        Client = new TwitchClientBuilder() { HttpClient = _httpClient }
            .WithAuthorizationResolution(new TwitchAuthorizationResolutionOptions()
            {
                FallbackClientIdResolver = (ctx, _) => ValueTask.FromResult(ClientIdentity.ClientId),
            }
            .ConfigureIdentity<TwitchIdentity.User, AccessTokenDetails.User>(new TokenResolutionOptions<AccessTokenDetails.User>()
            {
                GetCachedToken = (_, ct) => ValueTask.FromResult<AccessTokenDetails.User?>(_broadcasterAccessTokenDetails),
                RefreshToken = (expiredToken, ct) => _authClient.RefreshUserAccessToken(expiredToken, Config.Client.Secret, ct),
                OnNewToken = (newToken, ct) =>
                {
                    _broadcasterAccessTokenDetails = newToken;
                    return ValueTask.CompletedTask;
                }
            })
            .ConfigureIdentity<TwitchIdentity.Client, AccessTokenDetails.App>(new TokenResolutionOptions<AccessTokenDetails.App>()
            {
                AcquireNewToken = (ctx, ct) => _authClient.GetNewAppAccessToken(ctx.Identity.ClientId, Config.Client.Secret, ct),
                GetCachedToken = (_, ct) => ValueTask.FromResult(_appAccessTokenDetails),
                RefreshToken = async (expiredToken, ct) => await _authClient.GetNewAppAccessToken(expiredToken.Identity.ClientId, Config.Client.Secret, ct) is { } newToken
                    ? new AccessTokenRefreshResult.Refreshed<AccessTokenDetails.App>(newToken)
                    : new AccessTokenRefreshResult.Expired<AccessTokenDetails.App>(expiredToken),
                OnNewToken = (token, ct) =>
                {
                    _appAccessTokenDetails = token;
                    return ValueTask.CompletedTask;
                }
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

    public required ClientConfig Client { get; set; }
    public required AccessTokenDetails.User UserAccessTokenDetails { get; set; }
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
