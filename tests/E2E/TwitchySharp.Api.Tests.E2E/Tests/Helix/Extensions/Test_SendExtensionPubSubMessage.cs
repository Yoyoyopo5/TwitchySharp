using TwitchySharp.Api.Helix.Extensions;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Extensions;

public class Test_SendExtensionPubSubMessage(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("send-extension-pub-sub-message");

    [Fact]
    public async Task Send_GlobalExtensionPubSubMessageRequest_ReturnSuccessResponse()
    {
        TestName testName = new(TestName.Value + "-global");

        ExtensionConfiguration extensionConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<ExtensionConfiguration>(testName);

        SendExtensionPubSubMessageRequest globalRequest = new()
        {
            ExtensionId = extensionConfig.ExtensionId,
            Message = new GlobalPubSubMessageData() { Message = "Test global message." }
        };

        TestingTwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        await client.SendAsync(globalRequest, TestName, ct);
    }

    [Fact]
    public async Task Send_BroadcastExtensionPubSubMessageRequest_ReturnSuccessResponse()
    {
        TestName testName = new(TestName.Value + "-broadcaster");

        ExtensionConfiguration extensionConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<ExtensionConfiguration>(testName);

        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(testName);

        SendExtensionPubSubMessageRequest broadcastRequest = new()
        {
            ExtensionId = extensionConfig.ExtensionId,
            Message = new BroadcastPubSubMessageData() { Message = "Test broadcast message." }.To(userConfig.UserId)
        };

        TestingTwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        await client.SendAsync(broadcastRequest, TestName, ct);
    }

    [Fact]
    public async Task Send_WhisperExtensionPubSubMessageRequest_ReturnSuccessResponse()
    {
        TestName testName = new(TestName.Value + "-whisper");

        ExtensionConfiguration extensionConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<ExtensionConfiguration>(testName);

        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(testName);

        SendExtensionPubSubMessageRequest whisperRequest = new()
        {
            ExtensionId = extensionConfig.ExtensionId,
            Message = new BroadcastPubSubMessageData() { Message = "Test whisper message." }.WhisperTo(userConfig.UserId)
        };
        TestingTwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        await client.SendAsync(whisperRequest, TestName, ct);
    }
}
