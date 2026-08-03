using TwitchySharp.Api;
using TwitchySharp.Api.Helix.Chat;
using TwitchySharp.Api.Helix.EventSub;
using TwitchySharp.EventSub.Notifications;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.EventSub.Tests.E2E.NotificationTests.Channel.Chat;

public class Test_ChannelChatMessage(EventSubWebsocketFixture fixture)
    : EventSubNotificationTest<UserConfiguration, ChannelChatMessageNotification>(fixture)
{
    protected override TestName TestName => new("channel-chat-message");

    protected override EventSubSubscriptionTypeSpecification CreateSubscription(UserConfiguration identityConfig)
        => new Api.Helix.EventSub.Subscriptions.ChannelChatMessage(identityConfig.UserId, identityConfig.UserId);
    protected override Task RaiseNotification(ITwitchClient client, UserConfiguration identityConfig, CancellationToken ct = default)
    {
        const string TEST_MESSAGE = "test message pls ignore";

        return client.SendAsync(new SendChatMessageRequest()
        {
            Message = new()
            {
                BroadcasterId = identityConfig.UserId,
                SenderId = identityConfig.UserId,
                Message = TEST_MESSAGE
            }
        }, ct);
    }
}
