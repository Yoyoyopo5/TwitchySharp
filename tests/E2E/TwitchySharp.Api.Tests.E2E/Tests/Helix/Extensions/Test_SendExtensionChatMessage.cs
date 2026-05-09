using TwitchySharp.Api.Helix.Extensions;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Extensions;

[Collection("twitch")]
public class Test_SendExtensionChatMessage(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_SendExtensionChatMessageRequest_ReturnSuccessResponse()
    {
        const string TEST_EXTENSION_VERSION = "0.0.1";
        ExtensionVersion testExtensionVersion = new(TEST_EXTENSION_VERSION);
        SendExtensionChatMessageRequest request = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId,
            ExtensionOwnerId = _fixture.UserIdentity.UserId,
            Message = new()
            {
                ExtensionId = TwitchClientFixture.ExtensionConfig.ExtensionId,
                ExtensionVersion = testExtensionVersion,
                Text = "Test Extension Message"
            }
        };

        await TwitchClientFixture.Client.SendAsync(request, TestContext.Current.CancellationToken);
    }
}
