using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Webhooks.Functional;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Webhooks.Tests.Unit;

public class Test_WithHandler
{
    private class StubHandler : IWebhookEventSubHandler
    {
        public string? Challenge { get; private set; }
        public Error? Error { get; private set; }
        public IEventSubNotification? Notification { get; private set; }
        public EventSubSubscription? Revoked { get; private set; }

        public ValueTask OnCallbackVerification(EventSubSubscription newSubscription, string challenge, CancellationToken ct = default)
        {
            Challenge = challenge;
            return ValueTask.CompletedTask;
        }
        public ValueTask OnError(Error error, CancellationToken ct = default)
        {
            Error = error;
            return ValueTask.CompletedTask;
        }
        public ValueTask OnNotified(IEventSubNotification notification, CancellationToken ct = default)
        {
            Notification = notification;
            return ValueTask.CompletedTask;
        }
        public ValueTask OnSubscriptionRevoked(EventSubSubscription revokedSubscription, CancellationToken ct = default)
        {
            Revoked = revokedSubscription;
            return ValueTask.CompletedTask;
        }
    }

    private static ProcessWebhookRequest CreateStubProcess<T>(Func<T> createResponse)
        where T : WebhookRequestContent
        => (_, _) => ValueTask.FromResult<Validation<WebhookRequestContent>>(createResponse());

    private static ProcessWebhookRequest CreateStubProcess(Func<Error> createResponse)
        => (_, _) => ValueTask.FromResult<Validation<WebhookRequestContent>>(createResponse());

    [Fact]
    public async Task ProcessWebhookRequest_WithHandler_Notification_OnNotifiedCalled()
    {
        StubHandler stubHandler = new();

        ProcessWebhookRequest stubProcess = CreateStubProcess<NotificationRequestContent>(() => new()
        {
            Notification = new StubEventSubNotification(),
            Subscription = ProcessStubs.FakeSubscription
        }).WithHandler(stubHandler);

        await stubProcess(default!, CancellationToken.None);

        Assert.NotNull(stubHandler.Notification);
    }

    [Fact]
    public async Task ProcessWebhookRequest_WithHandler_Revocation_OnSubscriptionRevokedCalled()
    {
        StubHandler stubHandler = new();

        ProcessWebhookRequest stubProcess = CreateStubProcess<RevocationRequestContent>(() => new()
        {
            Subscription = ProcessStubs.FakeSubscription
        }).WithHandler(stubHandler);

        await stubProcess(default!, CancellationToken.None);

        Assert.NotNull(stubHandler.Revoked);
    }

    [Fact]
    public async Task ProcessWebhookRequest_WithHandler_CallbackVerification_OnCallbackVerificationCalled()
    {
        const string FAKE_CHALLENGE = "fake_challenge";

        StubHandler stubHandler = new();

        ProcessWebhookRequest stubProcess = CreateStubProcess<CallbackVerificationRequestContent>(() => new()
        {
            Challenge = FAKE_CHALLENGE,
            Subscription = ProcessStubs.FakeSubscription
        }).WithHandler(stubHandler);

        await stubProcess(default!, CancellationToken.None);

        Assert.Equal(FAKE_CHALLENGE, stubHandler.Challenge);
    }

    [Fact]
    public async Task ProcessWebhookRequest_WithHandler_Error_OnErrorCalled()
    {
        StubHandler stubHandler = new();

        ProcessWebhookRequest stubProcess = CreateStubProcess(() => new Error()).WithHandler(stubHandler);

        await stubProcess(default!, CancellationToken.None);

        Assert.NotNull(stubHandler.Error);
    }
}
