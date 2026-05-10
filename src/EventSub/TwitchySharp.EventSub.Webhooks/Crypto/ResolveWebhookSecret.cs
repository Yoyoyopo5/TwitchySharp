using TwitchySharp.EventSub.Models;
using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Webhooks;

/// <summary>
/// A function resolving a specific webhook secret for an EventSub subscription.
/// </summary>
/// <param name="subscription">The subscription to resolve a secret for.</param>
/// <param name="ct">Cancellation token.</param>
/// <returns>The secret associated with the <paramref name="subscription"/>, if any.</returns>
public delegate ValueTask<WebhookSecret?> ResolveWebhookSecret(EventSubSubscription subscription, CancellationToken ct);
