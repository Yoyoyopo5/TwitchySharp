using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitchySharp.EventSub.Webhooks.MessageVerifiers;
/// <summary>
/// Determines if a Twitch EventSub webhook message is from Twitch by verifying its signature.
/// See <see href="https://dev.twitch.tv/docs/eventsub/handling-webhook-events/#verifying-the-event-message">Verifying the Event Message</see> for more information.
/// </summary>
public interface ITwitchWebhookMessageVerifier
{
    /// <summary>
    /// Determines if the Twitch EventSub webhook message is from Twitch by verifying its signature.
    /// </summary>
    /// <param name="requestHeader">The headers of the webhook message.</param>
    /// <param name="body">The body of the webhook message.</param>
    /// <returns>A <see langword="bool"/> indicating if the message is verified to be from Twitch.</returns>
    ValueTask<bool> IsValid(EventSubWebhookRequestHeader requestHeader, string body, CancellationToken ct = default);
    /// <inheritdoc cref="IsValid(EventSubWebhookRequestHeader, string, CancellationToken)"/>
    ValueTask<bool> IsValid(EventSubWebhookRequestHeader requestHeader, Stream body, CancellationToken ct = default);
}
