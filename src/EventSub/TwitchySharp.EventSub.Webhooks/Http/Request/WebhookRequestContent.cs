using TwitchySharp.EventSub.Models;

namespace TwitchySharp.EventSub.Webhooks.Http;

public abstract record WebhookRequestContent
{
    public required EventSubSubscription Subscription { get; init; }
}
