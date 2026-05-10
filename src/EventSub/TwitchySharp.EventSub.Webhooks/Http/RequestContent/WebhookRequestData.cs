using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Notifications;

namespace TwitchySharp.EventSub.Webhooks.Requests;

/// <summary>
/// Data associated with a Twitch EventSub webhook request.
/// </summary>
public interface IWebhookRequestData : IEventSubNotification
{
    /// <summary>
    /// The EventSub subscription associated with the webhook request data.
    /// </summary>
    new EventSubSubscription Subscription { get; }
}

internal abstract record WebhookRequestData : IWebhookRequestData
{
    public required EventSubSubscription Subscription { get; init; }
}
