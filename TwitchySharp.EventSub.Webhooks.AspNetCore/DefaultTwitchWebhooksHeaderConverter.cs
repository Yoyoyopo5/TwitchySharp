using Microsoft.AspNetCore.Http;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore;

internal class DefaultTwitchWebhooksHeaderConverter : ITwitchWebhooksHeaderConverter
{
    private static IEnumerable<string> RequiredHeaders { get; } = [
        "Twitch-Eventsub-Message-Id",
        "Twitch-Eventsub-Message-Type",
        "Twitch-Eventsub-Message-Signature",
        "Twitch-Eventsub-Message-Timestamp",
        "Twitch-Eventsub-Subscription-Type",
        "Twitch-Eventsub-Subscription-Version"
        ];
    public TwitchWebhooksRequestHeaderConversionResult Convert(IHeaderDictionary headers)
        => new()
            {
                MissingHeaders = RequiredHeaders.Where(headerName => !headers.ContainsKey(headerName)),
                ConvertedHeader = new()
                {
                    TwitchEventsubMessageId = headers["Twitch-Eventsub-Message-Id"].FirstOrDefault() ?? string.Empty,
                    TwitchEventsubMessageRetry = headers["Twitch-Eventsub-Message-Retry"].FirstOrDefault(),
                    TwitchEventsubMessageType = headers["Twitch-Eventsub-Message-Type"].FirstOrDefault() ?? string.Empty,
                    TwitchEventsubMessageSignature = headers["Twitch-Eventsub-Message-Signature"].FirstOrDefault() ?? string.Empty,
                    TwitchEventsubMessageTimestamp = headers["Twitch-Eventsub-Message-Timestamp"].FirstOrDefault() ?? string.Empty,
                    TwitchEventsubSubscriptionType = headers["Twitch-Eventsub-Subscription-Type"].FirstOrDefault() ?? string.Empty,
                    TwitchEventsubSubscriptionVersion = headers["Twitch-Eventsub-Subscription-Version"].FirstOrDefault() ?? string.Empty
                }
            };
}
