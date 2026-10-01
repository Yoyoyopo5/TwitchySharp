using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Serialization;
using TwitchySharp.EventSub.Webhooks.Functional;
using TwitchySharp.Infrastructure.Functional;
using System.Text.Json;
using TwitchySharp.Tests.Unit;

namespace TwitchySharp.EventSub.Webhooks.Tests.Unit.Serialization;

public class Test_WebhookRequestDeserializer
{
    private const string StubSubscriptionTypeName = "fake.subscription";
    private const string StubSubscriptionVersion = "1";
    private readonly static EventSubSubscriptionType StubSubscriptionType = new(new(StubSubscriptionTypeName), new(StubSubscriptionVersion));

    private static ProcessWebhookRequest CreateStubProcess()
        => ProcessWebhookRequest.ByJsonDeserialization(options => options with
        {
            DeserializeNotification = DeserializeNotification.ByPolymorphicJsonDeserialization(getDeserializer => type => type == StubSubscriptionType
                ? json => JsonSerializer.Deserialize<StubEventSubNotification>(json)!
                : getDeserializer(type))
        });

    private const string FAKE_SUBSCRIPTION_JSON = $$"""
        {
            "id": "0b7f3361-672b-4d39-b307-dd5b576c9b27",
            "status": "enabled",
            "type": "{{StubSubscriptionTypeName}}",
            "version": "{{StubSubscriptionVersion}}",
            "condition": {
                "broadcaster_user_id": "1971641",
                "user_id": "2914196"
            },
            "transport": {
                "method": "webhook",
                "callback": "https://fake-callback.com"
            },
            "created_at": "2023-11-06T18:11:47.492253549Z",
            "cost": 0
        }
        """;

    private static EventSubWebhookRequestHeader CreateFakeHeader(EventSubWebhookMessageType messageType)
    {
        const string FAKE_MESSAGE_ID = "12345";
        const string FAKE_MESSAGE_TIMESTAMP = "2024-06-01T12:00:00Z";
        const string FAKE_SUBSCRIPTION_TYPE = "fake.subscription";
        const string FAKE_SUBSCRIPTION_VERSION = "1";
        const string FAKE_SIGNATURE = "12345";

        return new()
        {
            TwitchEventsubMessageId = new(FAKE_MESSAGE_ID),
            TwitchEventsubMessageTimestamp = new(FAKE_MESSAGE_TIMESTAMP),
            TwitchEventsubMessageType = messageType,
            TwitchEventsubSubscriptionType = new(FAKE_SUBSCRIPTION_TYPE),
            TwitchEventsubSubscriptionVersion = new(FAKE_SUBSCRIPTION_VERSION),
            TwitchEventsubMessageSignature = new(FAKE_SIGNATURE)
        };
    }

    [Fact]
    public async Task ProcessWebhookRequest_ValidRevocationRequest_ReturnRevocationRequestContent()
    {
        const string FAKE_BODY = $$"""
            {
                "subscription": {{FAKE_SUBSCRIPTION_JSON}}
            }
            """;

        using MemoryStream bodyStream = FAKE_BODY.ToMemoryStream();
        WebhookRequestContentStream payloadStream = new(bodyStream);

        EventSubWebhookRequest fakeRequest = new()
        {
            Header = CreateFakeHeader(EventSubWebhookMessageType.Revocation),
            Content = payloadStream
        };

        ProcessWebhookRequest stubProcess = CreateStubProcess();

        Result<IWebhookRequestContent> result = await stubProcess(fakeRequest, TestContext.Current.CancellationToken);
        result.Match(
            onError: e => throw new Exception(e.Message),
            onValid: content => Assert.IsType<RevocationRequestContent>(content)
            );
    }

    [Fact]
    public async Task ProcessWebhookRequest_ValidCallbackVerificationRequest_ReturnsCallbackVerificationRequestContentWithChallenge()
    {
        const string FAKE_CHALLENGE = "test-challenge";
        const string FAKE_BODY = $$"""
            {
                "challenge": "{{FAKE_CHALLENGE}}",
                "subscription": {{FAKE_SUBSCRIPTION_JSON}}
            }
            """;

        using MemoryStream bodyStream = FAKE_BODY.ToMemoryStream();
        WebhookRequestContentStream payloadStream = new(bodyStream);

        EventSubWebhookRequest fakeRequest = new()
        {
            Header = CreateFakeHeader(EventSubWebhookMessageType.WebhookCallbackVerification),
            Content = payloadStream
        };

        ProcessWebhookRequest stubProcess = CreateStubProcess();

        Result<IWebhookRequestContent> result = await stubProcess(fakeRequest, TestContext.Current.CancellationToken);
        result.Match(
            onError: e => throw new Exception(e.Message),
            onValid: content =>
            {
                CallbackVerificationRequestContent callbackVerification = Assert.IsType<CallbackVerificationRequestContent>(content);
                Assert.Equal(FAKE_CHALLENGE, callbackVerification.Challenge);
                return content;
            }
            );
    }

    [Fact]
    public async Task ProcessWebhookRequest_ValidNotification_ReturnNotificationRequestContent()
    {
        const string FAKE_BODY = $$"""
            {
              "subscription": {{FAKE_SUBSCRIPTION_JSON}},
              "event": {}
            }
            """;
        using MemoryStream bodyStream = FAKE_BODY.ToMemoryStream();
        WebhookRequestContentStream payloadStream = new(bodyStream);

        EventSubWebhookRequest fakeRequest = new()
        {
            Header = CreateFakeHeader(EventSubWebhookMessageType.Notification),
            Content = payloadStream
        };

        ProcessWebhookRequest stubProcess = CreateStubProcess();

        Result<IWebhookRequestContent> actualResponse = await stubProcess(fakeRequest, TestContext.Current.CancellationToken);
        actualResponse.Match(
            onError: e => throw new Exception(e.Message),
            onValid: content =>
            {
                NotificationRequestContent notification = Assert.IsType<NotificationRequestContent>(content);
                Assert.IsType<StubEventSubNotification>(notification.Notification);
                return content;
            }
            );
    }

    [Fact]
    public async Task ProcessWebhookRequest_InvalidMessageType_ReturnsError()
    {
        using MemoryStream bodyStream = string.Empty.ToMemoryStream();
        WebhookRequestContentStream payloadStream = new(bodyStream);

        EventSubWebhookRequest fakeRequest = new()
        {
            Header = CreateFakeHeader(new("invalid-message-type")),
            Content = payloadStream
        };

        ProcessWebhookRequest process = CreateStubProcess();

        Result<IWebhookRequestContent> result = await process(fakeRequest, TestContext.Current.CancellationToken);
        result.Match(
            onError: e => e,
            onValid: _ => throw new Exception("The process result was valid (expected error).")
            );
    }
}
