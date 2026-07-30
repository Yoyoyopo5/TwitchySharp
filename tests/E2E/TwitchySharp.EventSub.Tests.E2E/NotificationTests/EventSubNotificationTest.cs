using TwitchySharp.Api;
using TwitchySharp.Api.Helix.EventSub;
using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Websocket;
using TwitchySharp.EventSub.Websocket.Clients;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.EventSub.Tests.E2E.NotificationTests;

public abstract class EventSubNotificationTest<TRequiredIdentity>(EventSubWebsocketFixture fixture)
    : IAsyncLifetime
    where TRequiredIdentity : ITestIdentity
{
    protected abstract TestName TestName { get; }

    private readonly EventSubWebsocketFixture _fixture = fixture;
    private readonly TestHandler _handler = new();
    private EventSubWebsocketSession _session = null!;
    private StopWebsocketClient? _stopClient = null;

    public WebsocketSubscriptionTransport Transport => new(_session.Id);

    public async ValueTask InitializeAsync()
    {
        _stopClient = await _fixture.StartWebsocketClient(_handler, TestContext.Current.CancellationToken);
        _session = await _handler.WaitForWelcome(TestContext.Current.CancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        if (_stopClient is not null)
            await _stopClient(TestContext.Current.CancellationToken);
    }

    private record DeleteSubscription(
        ITwitchClient Client,
        Api.Helix.EventSub.EventSubSubscription Subscription
        ) : IAsyncDisposable
    {
        public async ValueTask DisposeAsync()
            => await Client.SendAsync(new DeleteEventSubSubscriptionRequest(Subscription), TestContext.Current.CancellationToken);
    }

    protected abstract EventSubSubscriptionTypeSpecification CreateSubscription(TRequiredIdentity identityConfig);
    protected abstract Task RaiseNotification(ITwitchClient client, TRequiredIdentity identityConfig, CancellationToken ct = default);
    protected virtual void AssertNotification(IEventSubNotification notification) { }
    
    private async Task<IAsyncDisposable> CreateSubscription(
        ITwitchClient client,
        TRequiredIdentity identityConfig,
        CancellationToken ct = default
        )
    {
        TwitchResponse<CreateEventSubSubscriptionResponse> response
            = await client.SendAsync(new CreateEventSubSubscriptionRequest()
            {
                Subscription = new()
                {
                    Type = CreateSubscription(identityConfig),
                    Transport = Transport
                }
            }, ct);

        return new DeleteSubscription(client, response.Content.Data.Single());
    }

    [Fact]
    public async Task CreateSubscriptionAndRaiseNotification_WaitForNotification_NotificationReceived()
    {
        TRequiredIdentity identityConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<TRequiredIdentity>(TestName);

        ITwitchClient client = _fixture.GetTwitchApiClient();
        CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(TestContext.Current.CancellationToken);
        cts.CancelAfter(TimeSpan.FromSeconds(5));
        CancellationToken ct = cts.Token;

        await using IAsyncDisposable deleteSubscription = await CreateSubscription(client, identityConfig, ct);
        await RaiseNotification(client, identityConfig, ct);
        IEventSubNotification notification = await _handler.WaitForNotification(ct);

        AssertNotification(notification);
    }
}
