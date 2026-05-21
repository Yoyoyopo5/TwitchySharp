using TwitchySharp.EventSub.Notifications;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore;

internal class EmptyHandler : IWebhookEventSubHandler
{
    public readonly static EmptyHandler Instance = new();

    public ValueTask OnCallbackVerification(EventSubSubscription newSubscription, string challenge, CancellationToken ct = default) => ValueTask.CompletedTask;
    public ValueTask OnError(Error error, CancellationToken ct = default) => ValueTask.CompletedTask;
    public ValueTask OnNotified(IEventSubNotification notification, CancellationToken ct = default) => ValueTask.CompletedTask;
    public ValueTask OnSubscriptionRevoked(EventSubSubscription revokedSubscription, CancellationToken ct = default) => ValueTask.CompletedTask;
}
