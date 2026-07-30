namespace TwitchySharp.EventSub.Webhooks.Functional;

/// <summary>
/// The content of an EventSub webhook request.
/// </summary>
public abstract record WebhookRequestContent
{
    /// <summary>
    /// The EventSub subscription that this webhook request content pertains to.
    /// </summary>
    public required EventSubSubscription Subscription { get; init; }
}
