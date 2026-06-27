using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Serialization;
using TwitchySharp.Serialization;
using TwitchySharp.Tests.Unit;

namespace TwitchySharp.EventSub.Tests.Unit.Serialization;

public class Test_NotificationConverter
{
    private readonly NotificationConverter _converter = new(NotificationDeserializer.CreateDefaultMap());

    public static IEnumerable<TheoryDataRow<JsonConverterTestData<IEventSubNotification>>> ValidData => [
        new(new()
        {
            Value = new AutomodMessageHoldNotification()
            {
                Subscription = new()
                {
                    Id = new("f1c2a387-161a-49f9-a165-0f21d7a4e1c4"),
                    Type = new("automod.message.hold"),
                    Version = new("1"),
                    Status = EventSubSubscriptionStatus.Enabled,
                    Cost = 0,
                    Condition = new()
                    {
                        BroadcasterUserId = new("1337"),
                        ModeratorUserId = new("9001")
                    },
                    Transport = new()
                    {
                        Method = EventSubTransportMethod.Webhook,
                        Callback = new("https://example.com/webhooks/callback")
                    },
                    CreatedAt = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Event = new()
                {
                    BroadcasterUserId = new("1337"),
                    BroadcasterUserLogin = new("blahblah"),
                    BroadcasterUserName = new("blah"),
                    UserId = new("456789012"),
                    UserName = new("baduser"),
                    UserLogin = new("baduserbla"),
                    Category = new("aggressive"),
                    HeldAt = new DateTimeOffset(2024, 05, 02, 11, 2, 30, TimeSpan.Zero),
                    Level = new(1),
                    MessageId = new("bad-message-id"),
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
                                    Id = new("emote-123"),
                                    EmoteSetId = new("set-emote-1")
                                }
                            },
                            new()
                            {
                                Type = AutomodCaughtMessageFragmentType.Cheermote,
                                Text = "badcheermote",
                                Cheermote = new()
                                {
                                    Prefix = new("prefix"),
                                    Bits = 1000,
                                    Tier = new(1)
                                }
                            }
                        ]
                    }
                }
            },
            Json = """
            {
                "subscription": {
                    "id": "f1c2a387-161a-49f9-a165-0f21d7a4e1c4",
                    "type": "automod.message.hold",
                    "version": "1",
                    "status": "enabled",
                    "cost": 0,
                    "condition": {
                        "broadcaster_user_id": "1337",
                        "moderator_user_id": "9001"
                    },
                    "transport": {
                        "method": "webhook",
                        "callback": "https://example.com/webhooks/callback"
                    },
                    "created_at": "2024-01-01T00:00:00Z"
                },
                "event": {
                    "broadcaster_user_id": "1337",
                    "broadcaster_user_login": "blahblah",
                    "broadcaster_user_name": "blah",
                    "user_id": "456789012",
                    "user_name": "baduser",
                    "user_login": "baduserbla",
                    "category": "aggressive",
                    "held_at": "2024-05-02T11:02:30Z",
                    "level": 1,
                    "message_id": "bad-message-id",
                    "message": {
                        "text": "test-text",
                        "fragments": [
                            {
                                "type": "text",
                                "text": "badtext"
                            },
                            {
                                "type": "emote",
                                "text": "bademote",
                                "emote": {
                                    "id": "emote-123",
                                    "emote_set_id": "set-emote-1"
                                }
                            },
                            {
                                "type": "cheermote",
                                "text": "badcheermote",
                                "cheermote": {
                                    "prefix": "prefix",
                                    "bits": 1000,
                                    "tier": 1
                                }
                            }
                        ]
                    }
                }
            }
            """ }),
        ];
    public static IEnumerable<TheoryDataRow<string>> InvalidJson => [
        "null",
        "true",
        "[]",
        "{}",
        "{ \"type\": 23 }",
        "{ \"type\": \"automod.message.hold\", \"version\": 1 }",
        "{ \"type\": \"unsupported-type\", \"version\": \"unsupported-version\", \"payload\": {} }"
        ];

    [Theory]
    [MemberData(nameof(ValidData))]
    public async Task Deserialize_ValidJson_ReturnNotification(JsonConverterTestData<IEventSubNotification> validData)
    {
        IEventSubNotification? notification = _converter.Read(validData.Json, JsonConfig.ApiOptions);
        Assert.Equal(validData.Value.ToString(), notification?.ToString());
    }

    [Theory]
    [MemberData(nameof(InvalidJson))]
    public async Task Deserialize_InvalidJson_ThrowsException(string invalidJson)
        => Assert.Throws<NotSupportedException>(() => _converter.Read(invalidJson, JsonConfig.ApiOptions));
}
