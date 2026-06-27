using System.Text;
using TwitchySharp.EventSub.Webhooks.Crypto;
using Xunit.Sdk;

namespace TwitchySharp.EventSub.Webhooks.Tests.Unit.Crypto;

public class Test_EventSubWebhookCrypto
{
    public class TestCryptoMessage : IXunitSerializable
    {
        public required string MessageBody { get; set; }
        public required string MessageTimestamp { get; set; }
        public required string MessageId { get; set; }
        public required string Secret { get; set; }
        public required string ExpectedHmac { get; set; }
        public void Deserialize(IXunitSerializationInfo info)
        {
            MessageBody = info.GetValue<string>(nameof(MessageBody)) ?? string.Empty;
            MessageTimestamp = info.GetValue<string>(nameof(MessageTimestamp)) ?? string.Empty;
            MessageId = info.GetValue<string>(nameof(MessageId)) ?? string.Empty;
            Secret = info.GetValue<string>(nameof(Secret)) ?? string.Empty;
            ExpectedHmac = info.GetValue<string>(nameof(ExpectedHmac)) ?? string.Empty;
        }
        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue(nameof(MessageBody), MessageBody);
            info.AddValue(nameof(MessageTimestamp), MessageTimestamp);
            info.AddValue(nameof(MessageId), MessageId);
            info.AddValue(nameof(Secret), Secret);
            info.AddValue(nameof(ExpectedHmac), ExpectedHmac);
        }
    }

    public static IEnumerable<TheoryDataRow<TestCryptoMessage>> TestMessages { get; } = [
        new(new() {
            MessageId = "12345",
            MessageTimestamp = "1781853859",
            MessageBody = "message_body",
            Secret = "super_secure_secret",
            ExpectedHmac = "8b2ee8080586cecca2b7e439ff8301393d908e9e1af2e346e7f8746528dac959"
        }) {
            TestDisplayName = "TestData01"
        },
        new(new() {
            MessageId = "12345",
            MessageTimestamp = "1781853859",
            MessageBody = "",
            Secret = "super_secure_secret",
            ExpectedHmac = "b07e02ec470e28720debe54f59e03ec4176858e0b439088ef22e9460ff8ba7fd"
        }) {
            TestDisplayName = "EmptyMessageBody"
        }
        ];

    [Theory]
    [MemberData(nameof(TestMessages))]
    public async Task ComputeSignature_ReturnsExpectedSignatureBytes(TestCryptoMessage message)
    {
        // The hex hmac string is case sensitive.
        // ComputeSignature returns an upper case hex string.
        byte[] expectedHmac = Encoding.UTF8.GetBytes("sha256=" + message.ExpectedHmac.ToUpper());

        byte[] secretBytes = Encoding.UTF8.GetBytes(message.Secret);
        using MemoryStream body = new(Encoding.UTF8.GetBytes(message.MessageBody));

        byte[] result = await EventSubWebhookCrypto.ComputeSignature(
            secretBytes,
            message.MessageId,
            message.MessageTimestamp,
            body,
            TestContext.Current.CancellationToken
            );

        Assert.Equal(Encoding.UTF8.GetString(expectedHmac), Encoding.UTF8.GetString(result));
        Assert.Equal(expectedHmac, result);
    }
}
