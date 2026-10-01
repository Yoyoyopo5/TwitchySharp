using System.Collections.Immutable;

namespace TwitchySharp.EventSub.Webhooks.Functional;

/// <summary>
/// The content for an EventSub subscription revocation request.
/// </summary>
public record RevocationRequestContent : IWebhookRequestContent
{
    /// <inheritdoc cref="IWebhookRequestContent.Subscription"/>
    public required EventSubSubscription<ImmutableDictionary<string, string>> Subscription { get; init; }
    IEventSubSubscription IWebhookRequestContent.Subscription => Subscription;
}
