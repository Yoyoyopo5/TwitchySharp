using TwitchySharp.Api;
using TwitchySharp.Api.Helix.EventSub;
using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Websocket;
using TwitchySharp.EventSub.Websocket.Clients;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.EventSub.Tests.E2E.NotificationTests;

public abstract class EventSubNotificationTest<TRequiredIdentity, TNotification>(EventSubWebsocketFixture fixture)
    where TRequiredIdentity : ITestIdentity<TwitchIdentity>
    where TNotification : IEventSubNotification
{
    protected abstract TestName TestName { get; }

    private readonly EventSubWebsocketFixture _fixture = fixture;

    private record DeleteSubscription(
        TestingTwitchClient Client,
        Api.Helix.EventSub.EventSubSubscription Subscription,
        TestName testName
        ) : IAsyncDisposable
    {
        public async ValueTask DisposeAsync()
            => await Client.SendAsync(new DeleteEventSubSubscriptionRequest(Subscription), testName, TestContext.Current.CancellationToken);
    }

    private record DisposeWebsocketClient(
        StopWebsocketClient StopClient
        ) : IAsyncDisposable
    {
        public async ValueTask DisposeAsync()
            => await StopClient();
    }

    protected abstract EventSubSubscriptionTypeSpecification CreateSubscription(TRequiredIdentity identityConfig);
    protected abstract Task RaiseNotification(TestingTwitchClient client, TRequiredIdentity identityConfig, CancellationToken ct = default);
    protected virtual void AssertNotification(IEventSubNotification notification) { }
    
    private async Task<IAsyncDisposable> CreateSubscription(
        TestingTwitchClient client,
        TRequiredIdentity identityConfig,
        EventSubWebsocketSession session,
        CancellationToken ct = default
        )
    {
        TwitchResponse<CreateEventSubSubscriptionResponseContent> response
            = await client.SendAsync(new CreateEventSubSubscriptionRequest()
            {
                Subscription = new()
                {
                    Type = CreateSubscription(identityConfig),
                    Transport = new WebsocketSubscriptionTransport(session.Id)
                }
            }, TestName, ct);

        return new DeleteSubscription(client, response.Content.Data.Single(), TestName);
    }

    [Fact]
    public async Task CreateSubscriptionAndRaiseNotification_WaitForNotification_NotificationReceived()
    {
        TRequiredIdentity identityConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<TRequiredIdentity>(TestName);

        TestingTwitchClient client = _fixture.GetTwitchApiClient();
        CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(TestContext.Current.CancellationToken);
        cts.CancelAfter(TimeSpan.FromSeconds(5));
        CancellationToken ct = cts.Token;

        TaskCompletionSource<EventSubWebsocketSession> welcomeReceived = new();
        TaskCompletionSource<TNotification> notificationReceived = new();

        await using DisposeWebsocketClient stopWebsocketClient = new(await _fixture.StartWebsocketClient(
            process => process
                .MapWelcome(async (session, ct) => welcomeReceived.TrySetResult(session))
                .MapNotification<TNotification>(async (notification, ct) => notificationReceived.TrySetResult(notification)),
                ct));

        EventSubWebsocketSession session = await welcomeReceived.Task.WaitAsync(ct);

        await using IAsyncDisposable deleteSubscription = await CreateSubscription(client, identityConfig, session, ct);
        await RaiseNotification(client, identityConfig, ct);

        IEventSubNotification notification = await notificationReceived.Task.WaitAsync(ct);

        AssertNotification(notification);
    }
}
