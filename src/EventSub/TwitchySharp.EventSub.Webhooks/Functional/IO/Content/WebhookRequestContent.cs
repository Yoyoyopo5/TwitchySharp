namespace TwitchySharp.EventSub.Webhooks.Functional;

/// <summary>
/// The content of an EventSub webhook request.
/// </summary>
public interface IWebhookRequestContent
{
    /// <summary>
    /// The EventSub subscription that this webhook request content pertains to.
    /// </summary>
    IEventSubSubscription Subscription { get; }
}
