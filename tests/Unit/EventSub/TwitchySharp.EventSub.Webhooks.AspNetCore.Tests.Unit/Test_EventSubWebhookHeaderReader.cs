using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using TwitchySharp.EventSub.Webhooks.Functional;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore.Tests.Unit;

public class Test_EventSubWebhookHeaderReader
{
    [Fact]
    public async Task Read_ValidHeaderCollection_ReturnsHeader()
    {
        EventSubWebhookRequestHeader mockHeader = new()
        {
            TwitchEventsubMessageId = new("12345"),
            TwitchEventsubMessageType = EventSubWebhookMessageType.Notification,
            TwitchEventsubMessageRetry = "false",
            TwitchEventsubSubscriptionType = new("fake-subscription"),
            TwitchEventsubSubscriptionVersion = new("1"),
            TwitchEventsubMessageTimestamp = new("12389174523"),
            TwitchEventsubMessageSignature = new("abcdef")
        };

        Dictionary<string, StringValues> fakeHeaders = new()
        {
            { "Twitch-Eventsub-Message-Id", new(mockHeader.TwitchEventsubMessageId) },
            { "Twitch-Eventsub-Message-Type", new(mockHeader.TwitchEventsubMessageType) },
            { "Twitch-Eventsub-Message-Retry", mockHeader.TwitchEventsubMessageRetry },
            { "Twitch-Eventsub-Message-Signature", new(mockHeader.TwitchEventsubMessageSignature) },
            { "Twitch-Eventsub-Message-Timestamp", new(mockHeader.TwitchEventsubMessageTimestamp) },
            { "Twitch-Eventsub-Subscription-Type", new(mockHeader.TwitchEventsubSubscriptionType) },
            { "Twitch-Eventsub-Subscription-Version", new(mockHeader.TwitchEventsubSubscriptionVersion) }
        };
        IHeaderDictionary fakeHeaderDictionary = new HeaderDictionary(fakeHeaders);

        EventSubWebhookRequestHeader header = EventSubWebhookHeaderReader.Read(fakeHeaderDictionary)
            .Match(
            onError: _ => throw new NotSupportedException("Read returned Error (expected Validation)."),
            onValid: header => header
            );

        Assert.Equal(mockHeader, header);
    }

    [Fact]
    public async Task Read_HeaderCollectionWithMissingHeader_ReturnsErrorWithMissingHeader()
    {
        const string MISSING_REQUIRED_HEADER = "Twitch-Eventsub-Message-Id";

        Dictionary<string, StringValues> fakeHeaders = new()
        {
            { "Twitch-Eventsub-Message-Type", new("") },
            { "Twitch-Eventsub-Message-Signature", new("") },
            { "Twitch-Eventsub-Message-Timestamp", new("") },
            { "Twitch-Eventsub-Subscription-Type", new("") },
            { "Twitch-Eventsub-Subscription-Version", new("") }
        };
        IHeaderDictionary fakeHeaderDictionary = new HeaderDictionary(fakeHeaders);

        EventSubWebhookHeaderReader.MissingHeadersError e = EventSubWebhookHeaderReader.Read(fakeHeaderDictionary)
            .Match(
            onError: e => Assert.IsType<EventSubWebhookHeaderReader.MissingHeadersError>(e),
            onValid: _ => throw new NotSupportedException("Read returned Validation (expected Error).")
            );

        Assert.Single(e.Headers, MISSING_REQUIRED_HEADER);
    }

    [Fact]
    public async Task Read_HeaderCollectionWithMultipleMissingHeaders_ReturnsErrorWithAllMissingHeaders()
    {
        IEnumerable<string> missingHeaders = [
            "Twitch-Eventsub-Message-Id",
            "Twitch-Eventsub-Message-Type",
            "Twitch-Eventsub-Message-Signature"
            ];

        Dictionary<string, StringValues> fakeHeaders = new()
        {
            { "Twitch-Eventsub-Message-Timestamp", new("123489716234") },
            { "Twitch-Eventsub-Subscription-Type", new("fake-subscription") },
            { "Twitch-Eventsub-Subscription-Version", new("1") }
        };
        IHeaderDictionary fakeHeaderDictionary = new HeaderDictionary(fakeHeaders);

        EventSubWebhookHeaderReader.MissingHeadersError e = EventSubWebhookHeaderReader.Read(fakeHeaderDictionary)
            .Match(
            onError: e => Assert.IsType<EventSubWebhookHeaderReader.MissingHeadersError>(e),
            onValid: _ => throw new NotSupportedException("Read returned Validation (expected Error).")
            );

        Assert.Equal(missingHeaders, e.Headers);
    }
}
