using TwitchySharp.Api.Helix.Extensions;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Extensions;

public class Test_SendExtensionChatMessage(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("send-extension-chat-message");

    [Fact]
    public async Task Send_SendExtensionChatMessageRequest_ReturnSuccessResponse()
    {
        ExtensionConfiguration extensionConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<ExtensionConfiguration>(TestName);

        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        SendExtensionChatMessageRequest request = new()
        {
            BroadcasterId = userConfig.UserId,
            Message = new()
            {
                ExtensionId = extensionConfig.ExtensionId,
                ExtensionVersion = extensionConfig.Version,
                Text = "Test Extension Message"
            }
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestName, TestContext.Current.CancellationToken);
    }
}
