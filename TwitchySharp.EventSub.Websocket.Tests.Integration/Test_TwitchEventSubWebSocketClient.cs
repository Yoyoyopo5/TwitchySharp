using NuGet.Frameworks;
using System.Diagnostics;
using TwitchySharp.Api;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Helix.Chat;
using TwitchySharp.Api.Helix.EventSub;
using TwitchySharp.Api.Helix.EventSub.Models.Types;
using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Notifications.Channel;
using TwitchySharp.EventSub.Websocket.Messages.Payloads;

namespace TwitchySharp.EventSub.Websocket.Tests.Integration;

public class Test_TwitchEventSubWebSocketClient(WebsocketFixture fixture) : IClassFixture<WebsocketFixture>
{
    private readonly WebsocketFixture _fixture = fixture;

    [Fact]
    public async void WaitFor_WelcomeMessage_ReturnNotNull()
    {
        await Task.Delay(2000);
        Assert.Null(_fixture.Handler.ReceivedException);
        Assert.NotNull(_fixture.Handler.ReceivedConnected);
    }

    [Fact]
    public async void WaitFor_ChannelChatMessageSubscriptionNotification_ReturnNotNull()
    {
        CancellationTokenSource cts = new(10000);

        string userId = (await _fixture.Api.SendRequestAsync(new ValidateAccessTokenRequest(_fixture.Secrets.UserAccessToken))).UserId;

        await Task.Delay(1000);
        if (_fixture.Handler.ReceivedConnected is null) throw new Exception("Welcome message not received.");

        string subscriptionId = string.Empty;

        try
        {
            subscriptionId = (await _fixture.Api.SendRequestAsync(new CreateEventSubSubscriptionRequest(_fixture.Secrets.ClientId, _fixture.Secrets.UserAccessToken, new()
            {
                Transport = new WebsocketSubscriptionTransport(_fixture.Handler.ReceivedConnected.Id),
                Type = new TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.Chat.ChannelChatMessage(userId, userId)
            }), cts.Token)).Data.First().Id;

            await _fixture.Api.SendRequestAsync(new SendChatMessageRequest(_fixture.Secrets.ClientId, _fixture.Secrets.UserAccessToken, new SendChatMessageRequestData()
            {
                BroadcasterId = userId,
                SenderId = userId,
                Message = "test message pls ignore"
            }));

            await Task.Delay(1000);

            Assert.Null(_fixture.Handler.ReceivedException);
            Assert.NotNull(_fixture.Handler.ReceivedNotification as Notifications.Channel.ChannelChatMessageNotification);
        }
        finally
        {
            await _fixture.Api.SendRequestAsync(new DeleteEventSubSubscriptionRequest(_fixture.Secrets.ClientId, _fixture.Secrets.UserAccessToken, subscriptionId));
        }
    }
}