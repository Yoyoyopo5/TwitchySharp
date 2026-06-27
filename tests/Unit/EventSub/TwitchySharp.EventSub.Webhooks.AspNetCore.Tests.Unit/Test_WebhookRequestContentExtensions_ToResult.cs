using System.Collections.Immutable;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Webhooks.Crypto;
using TwitchySharp.EventSub.Webhooks.Functional;
using TwitchySharp.EventSub.Webhooks.Serialization;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore.Tests.Unit;

public class Test_WebhookRequestContentExtensions_ToResult
{
    private static EventSubSubscription FakeSubscription { get; } = new()
    {
        Id = new("f1c2a387-161a-49f9-a165-0f21d7a4e1c4"),
        Status = EventSubSubscriptionStatus.Enabled,
        Type = new("channel.follow"),
        Version = new("1"),
        Cost = 1,
        Condition = new Dictionary<string, object>() { { "broadcaster_user_id", "12826" } }.ToImmutableDictionary(),
        CreatedAt = DateTimeOffset.Parse("2019-11-16T10:11:12.634234626Z"),
        Transport = new() { Method = EventSubTransportMethod.Webhook, Callback = new("https://example.com/webhooks/callback") }
    };

    private record StubEventSubNotification : IEventSubNotification
    {
        public EventSubSubscription Subscription => FakeSubscription;
    }

    private record UnsupportedRequestContent : WebhookRequestContent;

    [Fact]
    public void ToResult_DeserializationError_ReturnBadRequest()
    {
        Validation<WebhookRequestContent> fakeDeserializationError
            = new WebhookRequestDeserializer.DeserializationError("test error");

        IResult result = fakeDeserializationError.ToResult();

        Assert.IsType<BadRequest>(result);
    }

    [Fact]
    public void ToResult_VerificationError_ReturnUnauthorized()
    {
        Validation<WebhookRequestContent> fakeVerificationError
            = new WebhookHashVerifier.VerificationError("test error", null!);

        IResult result = fakeVerificationError.ToResult();

        Assert.IsType<UnauthorizedHttpResult>(result);
    }

    [Fact]
    public void ToResult_NotificationRequestContent_ReturnOk()
    {
        Validation<WebhookRequestContent> fakeNotificationRequest
            = new NotificationRequestContent()
            {
                Notification = new StubEventSubNotification(),
                Subscription = FakeSubscription
            };

        IResult result = fakeNotificationRequest.ToResult();

        Assert.IsType<Ok>(result);
    }

    [Fact]
    public void ToResult_CallbackVerificationContent_ReturnOkWithChallenge()
    {
        const string FAKE_CHALLENGE = "challenge";

        Validation<WebhookRequestContent> fakeCallbackVerificationRequest
            = new CallbackVerificationRequestContent()
            {
                Challenge = FAKE_CHALLENGE,
                Subscription = FakeSubscription
            };

        IResult result = fakeCallbackVerificationRequest.ToResult();

        ContentHttpResult ok = Assert.IsType<ContentHttpResult>(result);
        Assert.Equal(FAKE_CHALLENGE, ok.ResponseContent);
        Assert.Equal("text/plain; charset=utf-8", ok.ContentType);
    }

    [Fact]
    public void ToResult_RevocationRequestContent_ReturnNoContent()
    {
        Validation<WebhookRequestContent> fakeRecovationRequest
            = new RevocationRequestContent()
            {
                Subscription = FakeSubscription
            };

        IResult result = fakeRecovationRequest.ToResult();

        Assert.IsType<NoContent>(result);
    }

    [Fact]
    public void ToResult_UnsupportedRequestContentType_ReturnInternalServerError()
    {
        Validation<WebhookRequestContent> fakeUnsupportedRequest
            = new UnsupportedRequestContent()
            {
                Subscription = FakeSubscription
            };

        IResult result = fakeUnsupportedRequest.ToResult();

        StatusCodeHttpResult error = Assert.IsType<StatusCodeHttpResult>(result);
        Assert.Equal(500, error.StatusCode);
    }
}
