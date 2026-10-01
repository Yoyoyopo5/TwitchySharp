using System.Collections.Immutable;

namespace TwitchySharp.EventSub.Webhooks.Functional;

/// <summary>
/// The content of an EventSub webhook callback request.
/// </summary>
public record CallbackVerificationRequestContent : IWebhookRequestContent
{
    public required string Challenge { get; init; }
    /// <inheritdoc cref="IWebhookRequestContent.Subscription"/>
    public required EventSubSubscription<ImmutableDictionary<string, string>> Subscription { get; init; }
    IEventSubSubscription IWebhookRequestContent.Subscription => Subscription;
}
