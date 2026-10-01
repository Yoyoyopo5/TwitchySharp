using System.Collections.Immutable;
using System.Text;
using System.Text.Json;
using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Serialization;
using TwitchySharp.Infrastructure.Functional;
using TwitchySharp.Serialization;
using TwitchySharp.Tests.Unit;

namespace TwitchySharp.EventSub.Tests.Unit.Serialization;

public record StubNotification : IEventSubNotification
{
    public static EventSubSubscriptionType SubscriptionType { get; }
        = new(new("stub.notification"), EventSubSubscriptionTypeVersion.Version1);
    public IEventSubSubscription Subscription => new EventSubSubscription<ImmutableDictionary<string, object>>()
    {
        Id = new("123"),
        Type = SubscriptionType.Type,
        Version = SubscriptionType.Version,
        Status = EventSubSubscriptionStatus.Enabled,
        Cost = 1,
        CreatedAt = new DateTimeOffset(2024, 1, 1, 2, 10, 10, TimeSpan.Zero),
        Transport = new()
        {
            Method = EventSubTransportMethod.Webhook,
            Callback = new("https://fakecallbackurl.com")
        },
        Condition = new Dictionary<string, object>()
        {
            { "user_id", "1234" }
        }.ToImmutableDictionary()
    };
    public object Event { get; } = new();
}

public class Test_NotificationDeserializer
{
    [Fact]
    public async Task Deserialize_CustomConfiguration_ValidCustomNotification_ReturnsTypedNotification()
    {
        const string FAKE_JSON = "{ \"subscription\": { \"type\": \"stub.notification\", \"version\": \"1\" } }";

        DeserializeNotification deserialize
            = DeserializeNotification.ByPolymorphicJsonDeserialization(deserializers => type => type == StubNotification.SubscriptionType
                ? document => JsonSerializer.Deserialize<StubNotification>(document, JsonConfig.ApiOptions)!
                : deserializers(type));

        using MemoryStream fakeStream = new(Encoding.UTF8.GetBytes(FAKE_JSON));
        NotificationPayloadStream payloadStream = new(fakeStream);

        await deserialize(payloadStream, TestContext.Current.CancellationToken).MatchAsync(
            onError: e => throw new InvalidOperationException(e.Message),
            onValid: notification => { Assert.IsType<StubNotification>(notification); return ValueTask.CompletedTask; }
            );
    }

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
    public async Task Deserialize_ValidJsonNotification_ReturnTypedNotification(JsonConverterTestData<IEventSubNotification> validData)
    {
        DeserializeNotification deserialize
            = DeserializeNotification.ByPolymorphicJsonDeserialization();

        NotificationPayloadStream stream = new(validData.Json.ToMemoryStream());

        await deserialize(stream, TestContext.Current.CancellationToken).MatchAsync(
            e => throw new Exception(e.Message),
            n =>
            {
                Assert.Equal(JsonSerializer.Serialize(validData.Value), JsonSerializer.Serialize(n));
                return n;
            });
    }

    [Theory]
    [MemberData(nameof(InvalidJson))]
    public async Task Deserialize_InvalidJson_ReturnsError(string invalidJson)
    {
        DeserializeNotification deserialize
            = DeserializeNotification.ByPolymorphicJsonDeserialization();

        NotificationPayloadStream stream = new(invalidJson.ToMemoryStream());

        await deserialize(stream, TestContext.Current.CancellationToken).MatchAsync(
            e => e,
            n => throw new InvalidOperationException("Returned valid.")
            );
    }
}
