using TwitchySharp.Api;
using TwitchySharp.Api.Helix.Chat;
using TwitchySharp.Api.Helix.EventSub;
using TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
using TwitchySharp.EventSub.Models.Notifications.Channel.Chat;
using TwitchySharp.Api.Authorization;
using System.Text;

namespace TwitchySharp.EventSub.Websocket.Tests.E2E;

public class Test_TwitchEventSubWebSocketClient(WebsocketFixture fixture) : IClassFixture<WebsocketFixture>
{
    private readonly WebsocketFixture _fixture = fixture;

    [Fact]
    public async Task Send_ValidateAccessTokenRequest_ReturnSuccessResponseWithUserReadChatScope()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        var response = await _fixture.Api.SendAsync(new ValidateAccessTokenRequest() { UserId = _fixture.AuthorizedBroadcaster.UserId }, ct);
        Assert.Contains(Scope.UserReadChat, response.Content.Scopes);
    }

    [Fact]
    public async Task WaitFor_WelcomeMessage_ReturnNotNull()
    {
        Assert.Null(_fixture.Handler.ReceivedException);
        Assert.NotNull(_fixture.Handler.ReceivedConnected);
    }

    [Fact]
    public async Task WaitFor_ChannelChatMessageSubscriptionNotification_ReturnNotNull()
    {
        const string TEST_MESSAGE = "test message pls ignore";
        CancellationToken ct = TestContext.Current.CancellationToken;
        EventSubSubscription? subscription = null;

        Assert.NotNull(_fixture.Handler.ReceivedConnected);

        try
        {
            // Create subscription
            subscription = (await _fixture.Api.SendAsync(new CreateEventSubSubscriptionRequest()
            {
                Subscription = new()
                {
                    Transport = new WebsocketSubscriptionTransport(_fixture.Handler.ReceivedConnected.Id),
                    Type = new ChannelChatMessage(_fixture.AuthorizedBroadcaster.UserId, _fixture.AuthorizedBroadcaster.UserId)
                }
            }, ct)).Content.Data.First();

            // Send chat message
            await _fixture.Api.SendAsync(new SendChatMessageRequest()
            {
                Message = new()
                {
                    BroadcasterId = _fixture.AuthorizedBroadcaster.UserId,
                    SenderId = _fixture.AuthorizedBroadcaster.UserId,
                    Message = TEST_MESSAGE
                }
            }, ct);

            await Task.Delay(1000, ct);

            ChannelChatMessageNotification? notification = _fixture.Handler.ReceivedNotification as ChannelChatMessageNotification;

            Assert.Null(_fixture.Handler.ReceivedException);
            Assert.NotNull(notification);
            Assert.Equal(TEST_MESSAGE, notification.Event.Message.Text);
        }
        catch (TwitchApiException ex)
        {
            string content = Encoding.UTF8.GetString(ex.Content);
            TestContext.Current.AddWarning("An API request failed with content: " + content);
        }
        finally
        {
            if (subscription is not null)
                await _fixture.Api.SendAsync(new DeleteEventSubSubscriptionRequest(subscription), ct);
        }
    }
}