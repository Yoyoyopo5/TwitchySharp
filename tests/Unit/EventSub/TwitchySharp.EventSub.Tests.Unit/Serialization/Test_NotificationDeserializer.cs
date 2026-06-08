using System.Collections.Immutable;
using System.Text;
using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Serialization;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Tests.Unit.Serialization;

public record StubNotification : IEventSubNotification
{
    public EventSubSubscription Subscription => new()
    {
        Id = new("123"),
        Type = new("stub.notification"),
        Version = new("1"),
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
}

public class Test_NotificationDeserializer
{
    private readonly static DeserializeNotification _deserialize
        = NotificationDeserializer.CreateDeserializer((type) => (options, document) => new StubNotification());

    [Fact]
    public async Task Deserialize_ValidNotification_ReturnsTypedNotification()
    {
        const string FAKE_JSON = "{ \"subscription\": { \"type\": \"stub.notification\", \"version\": \"1\" } }";
        using MemoryStream fakeStream = new(Encoding.UTF8.GetBytes(FAKE_JSON));
        NotificationPayloadStream payloadStream = new(fakeStream);

        await _deserialize(payloadStream, TestContext.Current.CancellationToken).MatchAsync(
            onError: (e, ct) => throw new InvalidOperationException(e.Message),
            onValid: (notification, ct) => { Assert.IsType<StubNotification>(notification); return ValueTask.CompletedTask; },
            TestContext.Current.CancellationToken
            );
    }
}
