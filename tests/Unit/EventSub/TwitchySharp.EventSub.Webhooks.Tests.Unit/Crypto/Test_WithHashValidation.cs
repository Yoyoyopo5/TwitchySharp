using System.Text;
using TwitchySharp.EventSub.Serialization;
using TwitchySharp.EventSub.Webhooks.Crypto;
using TwitchySharp.EventSub.Webhooks.Functional;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Webhooks.Tests.Unit.Crypto;

public class Test_WithHashValidation
{
    private const string FAKE_REQUEST_BODY = "request_data";

    private readonly EventSubWebhookRequest _fakeRequest = new()
    {
        Header = new EventSubWebhookRequestHeader()
        {
            TwitchEventsubMessageId = new("12345"),
            TwitchEventsubSubscriptionType = new("fake-subscription"),
            TwitchEventsubSubscriptionVersion = new("1"),
            TwitchEventsubMessageType = EventSubWebhookMessageType.Notification,
            TwitchEventsubMessageTimestamp = new("12387447"),
            TwitchEventsubMessageSignature = new("very-legit-signature")
        },
        Content = new(new MemoryStream(Encoding.UTF8.GetBytes(FAKE_REQUEST_BODY)))
    };

    private record FakeWebhookRequestContent(string Body) : WebhookRequestContent;

    private static FakeWebhookRequestContent CreateFakeContent(NotificationPayloadStream bodyStream)
    {
        using StreamReader sr = new(bodyStream);
        return new FakeWebhookRequestContent(sr.ReadToEnd())
        {
            Subscription = new EventSubSubscription()
            {
                Id = new("123"),
                Status = EventSubSubscriptionStatus.Enabled,
                CreatedAt = DateTimeOffset.MinValue,
                Cost = 1,
                Transport = new EventSubTransport()
                {
                    Method = EventSubTransportMethod.Webhook,
                    Callback = new("https://fake-callback.com")
                },
                Type = new("fake-subscription"),
                Version = new("1")
            }
        };
    }

    private readonly ProcessWebhookRequest _stubProcess
        = (request, ct) => ValueTask.FromResult<Validation<WebhookRequestContent>>(CreateFakeContent(request.Content));

    [Fact]
    public async Task ProcessWebhookRequest_WithHashValidation_VerifyAndNextGetSameBytes()
    {
        string verifyInputBody = string.Empty;

        ProcessWebhookRequest process = _stubProcess.WithHashValidation((_, request, ct) =>
        {
            using StreamReader sr = new(request.Content);
            verifyInputBody = sr.ReadToEnd();
            return ValueTask.FromResult(new Validation());
        });

        await process(_fakeRequest, TestContext.Current.CancellationToken)
            .MatchAsync(
            onError: (_, _) => throw new NotImplementedException(),
            onValid: (result, _) =>
            {
                Assert.Equal(((FakeWebhookRequestContent)result).Body, verifyInputBody);
                return ValueTask.CompletedTask;
            },
            CancellationToken.None
            );
    }

    [Fact]
    public async Task ProcessWebhookRequest_WithHashValidationError_ReturnsError()
    {
        ProcessWebhookRequest process = _stubProcess.WithHashValidation((_, request, ct) => ValueTask.FromResult<Validation>(new Error()));

        await process(_fakeRequest, TestContext.Current.CancellationToken)
            .MatchAsync(
            onError: (e, _) => ValueTask.CompletedTask,
            onValid: (result, _) => throw new Exception("Verify hash returned Validation (expected Error)."),
            CancellationToken.None
            );
    }

    [Fact]
    public async Task ProcessWebhookRequest_WithHashValidationSuccess_ReturnsNext()
    {
        ProcessWebhookRequest process = _stubProcess.WithHashValidation((_, request, ct) => ValueTask.FromResult(new Validation()));

        await process(_fakeRequest, TestContext.Current.CancellationToken)
            .MatchAsync(
            onError: (e, _) => throw new Exception("Verify hash returned Error (expected Validation)."),
            onValid: (result, _) => ValueTask.CompletedTask,
            CancellationToken.None
            );
    }
}
