using System.Linq.Expressions;
using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Webhooks.Functional;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Webhooks.Tests.Unit;

public class Test_RequestHandling
{
    private static ProcessWebhookRequest CreateStubProcess<T>(Func<T> createResponse)
        where T : WebhookRequestContent
        => (_, _) => ValueTask.FromResult<Validation<WebhookRequestContent>>(createResponse());

    private static ProcessWebhookRequest CreateStubProcess(Func<Error> createResponse)
        => (_, _) => ValueTask.FromResult<Validation<WebhookRequestContent>>(createResponse());

    [Fact]
    public async Task ProcessWebhookRequest_MapNotification_FunctionCalled()
    {
        IEventSubNotification? receivedNotification = null;

        ProcessWebhookRequest stubProcess = CreateStubProcess<NotificationRequestContent>(() => new()
        {
            Notification = new StubEventSubNotification(),
            Subscription = ProcessStubs.FakeSubscription
        }).MapNotification<StubEventSubNotification>((notification, ct) =>
        {
            receivedNotification = notification;
            return ValueTask.CompletedTask;
        });

        await stubProcess(default!, TestContext.Current.CancellationToken);

        Assert.NotNull(receivedNotification);
    }

    [Fact]
    public async Task ProcessWebhookRequest_MapSubscriptionRevocation_FunctionCalled()
    {
        EventSubSubscription? receivedRevokedSubscription = null;

        ProcessWebhookRequest stubProcess = CreateStubProcess<RevocationRequestContent>(() => new()
        {
            Subscription = ProcessStubs.FakeSubscription
        }).MapSubscriptionRevoked((subscription, ct) =>
        {
            receivedRevokedSubscription = subscription;
            return ValueTask.CompletedTask;
        });

        await stubProcess(default!, TestContext.Current.CancellationToken);

        Assert.NotNull(receivedRevokedSubscription);
    }

    [Fact]
    public async Task ProcessWebhookRequest_MapCallbackVerification_FunctionCalled()
    {
        const string FAKE_CHALLENGE = "fake_challenge";

        string? receivedChallenge = null;

        ProcessWebhookRequest stubProcess = CreateStubProcess<CallbackVerificationRequestContent>(() => new()
        {
            Challenge = FAKE_CHALLENGE,
            Subscription = ProcessStubs.FakeSubscription
        }).MapCallbackVerification((subscription, challenge, ct) =>
        {
            receivedChallenge = challenge;
            return ValueTask.CompletedTask;
        });

        await stubProcess(default!, TestContext.Current.CancellationToken);

        Assert.Equal(FAKE_CHALLENGE, receivedChallenge);
    }

    [Fact]
    public async Task ProcessWebhookRequest_MapError_FunctionCalled()
    {
        Error fakeError = new();
        Error? receivedError = null;

        ProcessWebhookRequest stubProcess = CreateStubProcess(() => fakeError)
            .MapError((error, ct) =>
            {
                receivedError = error;
                return ValueTask.CompletedTask;
            });

        await stubProcess(default!, CancellationToken.None);

        Assert.Equal(fakeError, receivedError);
    }
}
