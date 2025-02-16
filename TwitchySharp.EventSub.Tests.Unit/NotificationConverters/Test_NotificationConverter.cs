using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using TwitchySharp.EventSub.NotificationConverters;
using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Notifications.EventModels;
using TwitchySharp.Shared;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Tests.Unit.NotificationConverters;
public class Test_NotificationConverter
{
    [Fact]
    public void Convert_AutomodMessageHoldNotificationJson_ReturnAutomodMessageHoldNotificationInstance()
    {
        AutomodMessageHoldNotification stubNotification = new()
        {
            Subscription = new()
            {
                Id = "f1c2a387-161a-49f9-a165-0f21d7a4e1c4",
                Type = "automod.message.hold",
                Version = "1",
                Status = EventSubSubscriptionStatus.Enabled,
                Cost = 0,
                Condition = new()
                {
                    BroadcasterUserId = "1337",
                    ModeratorUserId = "9001"
                },
                Transport = new()
                {
                    Method = EventSubTransportMethod.Webhook,
                    Callback = "https://example.com/webhooks/callback"
                },
                CreatedAt = DateTimeOffset.UtcNow
            },
            Event = new()
            {
                BroadcasterUserId = "1337",
                BroadcasterUserLogin = "blahblah",
                BroadcasterUserName = "blah",
                UserId = "456789012",
                UserName = "baduser",
                UserLogin = "baduserbla",
                Category = "aggressive",
                HeldAt = DateTimeOffset.UtcNow,
                Level = 1,
                MessageId = "bad-message-id",
                Message = new()
                {
                    Text = "test-text",
                    Fragments = [
                        new() 
                        {
                            Type = AutomodCaughtMessageFragmentType.Text,
                            Text = "badtext"
                        },
                        new()
                        {
                            Type = AutomodCaughtMessageFragmentType.Emote,
                            Text = "bademote",
                            Emote = new()
                            {
                                Id = "emote-123",
                                EmoteSetId = "set-emote-1"
                            }
                        },
                        new()
                        {
                            Type = AutomodCaughtMessageFragmentType.Cheermote,
                            Text = "badcheermote",
                            Cheermote = new()
                            {
                                Prefix = "prefix",
                                Bits = 1000,
                                Tier = 1
                            }
                        }
                    ]
                }
            }
        };

        INotificationConverter stubConverter = new NotificationConverter();

        using JsonDocument inputJson = JsonSerializer.SerializeToDocument<AutomodMessageHoldNotification>(stubNotification, JsonConfig.ApiOptions);
        string mockJson = JsonSerializer.Serialize(stubNotification, JsonConfig.ApiOptions);
        EventSubNotification preliminary = JsonSerializer.Deserialize<EventSubNotification>(inputJson, JsonConfig.ApiOptions)!;
        AutomodMessageHoldNotification actualNotification = stubConverter.Deserialize(inputJson, preliminary.Subscription) switch
        {
            AutomodMessageHoldNotification automodMessageHold => automodMessageHold,
            _ => throw new NotSupportedException("Notifcation failed to deserialize to correct type.")
        };
        string actualJson = JsonSerializer.Serialize(actualNotification, JsonConfig.ApiOptions);

        Assert.Equal(mockJson, actualJson);
    }
}
