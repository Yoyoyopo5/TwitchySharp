using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Text;
using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Webhooks.SignatureComputers;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore.Tests.Integration;

public class Test_TwitchWebhooksRouteExtensions(WebhooksFixture fixture)
    : IClassFixture<WebhooksFixture>
{
    private readonly WebhooksFixture _fixture = fixture;

    [Fact]
    public async Task Respond_ValidCallbackRequest_ValidResponse()
    {
        const string FAKE_SUBSCRIPTION_ID = "f1c2a387-161a-49f9-a165-0f21d7a4e1c4";
        const string FAKE_CHALLENGE = "test_challenge";
        const string FAKE_MESSAGE_ID = "1234567890";
        const string FAKE_TIMESTAMP = "2019-11-16T10:11:12.634234626Z";
        const string FAKE_BODY = $$"""
            {
              "challenge": "{{FAKE_CHALLENGE}}",
              "subscription": {
                "id": "{{FAKE_SUBSCRIPTION_ID}}",
                "status": "webhook_callback_verification_pending",
                "type": "channel.follow",
                "version": "1",
                "cost": 1,
                "condition": {
                  "broadcaster_user_id": "12826"
                },
                "transport": {
                  "method": "webhook",
                  "callback": "http://localhost/test-webhook"
                },
                "created_at": "2019-11-16T10:11:12.634234626Z"
              }
            }
            """;

        DefaultTwitchWebhookCrypto stubCrypto = new();
        string fakeSignature = Encoding.UTF8.GetString(await stubCrypto.ComputeSignature(Encoding.UTF8.GetBytes(_fixture.Secret), FAKE_MESSAGE_ID, FAKE_TIMESTAMP, FAKE_BODY));

        IHeaderDictionary fakeHeaders = new EventSubWebhookRequestHeader()
        {
            TwitchEventsubMessageId = FAKE_MESSAGE_ID,
            TwitchEventsubMessageType = "webhook_callback_verification",
            TwitchEventsubMessageSignature = fakeSignature,
            TwitchEventsubMessageTimestamp = FAKE_TIMESTAMP,
            TwitchEventsubSubscriptionType = "channel.follow",
            TwitchEventsubSubscriptionVersion = "1"
        }.ToHeaderDictionary();

        HttpRequestMessage fakeCallbackRequest = new HttpRequestMessage(HttpMethod.Post, _fixture.Path)
        {
            Content = new StringContent(FAKE_BODY)
        }.AddHeaders(fakeHeaders);

        HttpClient stubClient = _fixture.CreateClient();
        HttpResponseMessage actualReponse = await stubClient.SendAsync(fakeCallbackRequest);
        EventSubSubscription? actualSubscription = _fixture.Handler.ActiveSubscription;

        HttpStatusCode actualResponseStatusCode = actualReponse.StatusCode;
        string actualResponseBody = await actualReponse.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, actualResponseStatusCode);
        Assert.Equal(FAKE_CHALLENGE, actualResponseBody);
        Assert.NotNull(actualSubscription);
        Assert.Equal(FAKE_SUBSCRIPTION_ID, actualSubscription.Id);
    }

    [Fact]
    public async Task Respond_ValidChannelChatMessageNotification_200Response()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public async Task Respond_ValidRevocationRequest_204Response()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public async Task Respond_InvalidHeadersRequest_400Response()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public async Task Respond_InvalidSecretRequest_401Response()
    {
        throw new NotImplementedException();
    }
}