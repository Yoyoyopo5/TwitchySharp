using System.Collections.Immutable;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using TwitchySharp.EventSub.Webhooks.Functional;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore;

internal delegate Validation<EventSubWebhookRequestHeader> ReadWebhookHeader(IHeaderDictionary headers);

internal static class EventSubWebhookHeaderReader
{
    public record MissingHeadersError(IReadOnlyCollection<string> Headers) : Error($"Missing required header: {string.Join(", ", Headers)}");

    private static class WebhookHeaderKeys
    {
        public const string MessageId = "Twitch-Eventsub-Message-Id";
        public const string MessageType = "Twitch-Eventsub-Message-Type";
        public const string MessageRetry = "Twitch-Eventsub-Message-Retry";
        public const string MessageSignature = "Twitch-Eventsub-Message-Signature";
        public const string MessageTimestamp = "Twitch-Eventsub-Message-Timestamp";
        public const string SubscriptionType = "Twitch-Eventsub-Subscription-Type";
        public const string SubscriptionVersion = "Twitch-Eventsub-Subscription-Version";
    }

    private readonly record struct WebhookHeaderKey(string Value, bool IsRequired)
    {
        public static readonly WebhookHeaderKey MessageId = new(WebhookHeaderKeys.MessageId, true);
        public static readonly WebhookHeaderKey MessageType = new(WebhookHeaderKeys.MessageType, true);
        public static readonly WebhookHeaderKey MessageRetry = new(WebhookHeaderKeys.MessageRetry, false);
        public static readonly WebhookHeaderKey MessageSignature = new(WebhookHeaderKeys.MessageSignature, true);
        public static readonly WebhookHeaderKey MessageTimestamp = new(WebhookHeaderKeys.MessageTimestamp, true);
        public static readonly WebhookHeaderKey SubscriptionType = new(WebhookHeaderKeys.SubscriptionType, true);
        public static readonly WebhookHeaderKey SubscriptionVersion = new(WebhookHeaderKeys.SubscriptionVersion, true);

        public static implicit operator string(WebhookHeaderKey key) => key.Value;

        public static WebhookHeaderKey[] All { get; } = [
            MessageId,
            MessageType,
            MessageRetry,
            MessageSignature,
            MessageTimestamp,
            SubscriptionType,
            SubscriptionVersion
        ];
    }

    private static string? GetFirstValueOrDefault(this IHeaderDictionary headers, WebhookHeaderKey headerKey)
        => headers[headerKey.Value] switch
        {
            { Count: 0 } => null,
            StringValues values => values.First() switch
            {
                string value => value,
                _ => null
            }
        };

    private record HeaderReadContext(IHeaderDictionary HeaderDictionary)
    {
        public ImmutableArray<string> MissingHeaders { get; init; } = [];
        public ImmutableDictionary<string, string> Headers { get; init; } = ImmutableDictionary.Create<string, string>();
    }

    public static Validation<EventSubWebhookRequestHeader> Read(IHeaderDictionary headerDictionary)
        => WebhookHeaderKey.All.Aggregate(new HeaderReadContext(headerDictionary), static (context, key) => context.HeaderDictionary.GetFirstValueOrDefault(key) switch
        {
            null when key.IsRequired => context with { MissingHeaders = context.MissingHeaders.Add(key) },
            string value => context with { Headers = context.Headers.Add(key, value) },
            _ => context
        }) switch
        {
            { MissingHeaders.IsEmpty: false } context => new MissingHeadersError(context.MissingHeaders),
            { } context => new EventSubWebhookRequestHeader
            {
                TwitchEventsubMessageId = new(context.Headers[WebhookHeaderKeys.MessageId]),
                TwitchEventsubMessageRetry = context.Headers.GetValueOrDefault(WebhookHeaderKeys.MessageRetry),
                TwitchEventsubMessageTimestamp = new(context.Headers[WebhookHeaderKeys.MessageTimestamp]),
                TwitchEventsubMessageSignature = new(context.Headers[WebhookHeaderKeys.MessageSignature]),
                TwitchEventsubMessageType = new(context.Headers[WebhookHeaderKeys.MessageType]),
                TwitchEventsubSubscriptionType = new(context.Headers[WebhookHeaderKeys.SubscriptionType]),
                TwitchEventsubSubscriptionVersion = new(context.Headers[WebhookHeaderKeys.SubscriptionVersion])
            }
        };
}
