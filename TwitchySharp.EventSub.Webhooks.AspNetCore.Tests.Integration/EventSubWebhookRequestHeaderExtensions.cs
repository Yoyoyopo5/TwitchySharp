using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore.Tests.Integration;

internal static class EventSubWebhookRequestHeaderExtensions
{
    public static IHeaderDictionary ToHeaderDictionary(this EventSubWebhookRequestHeader header)
        => new HeaderDictionary
        {
            { "Twitch-Eventsub-Message-Id", header.TwitchEventsubMessageId },
            { "Twitch-Eventsub-Message-Retry", header.TwitchEventsubMessageRetry ?? string.Empty },
            { "Twitch-Eventsub-Message-Type", header.TwitchEventsubMessageType.Value },
            { "Twitch-Eventsub-Message-Signature", header.TwitchEventsubMessageSignature },
            { "Twitch-Eventsub-Message-Timestamp",  header.TwitchEventsubMessageTimestamp },
            { "Twitch-Eventsub-Subscription-Type", header.TwitchEventsubSubscriptionType },
            { "Twitch-Eventsub-Subscription-Version", header.TwitchEventsubSubscriptionVersion }
        };
}

internal static class HttpRequestMessageExtensions
{
    public static HttpRequestMessage AddHeaders(this HttpRequestMessage request, IHeaderDictionary headers)
    {
        foreach (var header in headers)
        {
            request.Headers.TryAddWithoutValidation(header.Key, [.. header.Value]);
        }
        return request;
    }
}
