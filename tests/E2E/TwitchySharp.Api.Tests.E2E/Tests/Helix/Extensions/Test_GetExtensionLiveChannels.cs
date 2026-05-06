using TwitchySharp.Api.Helix.Extensions;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Extensions;

[Collection("twitch")]
public class Test_GetExtensionLiveChannels(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetExtensionLiveChannelsRequest_ReturnSuccessResponse()
    {
        // Not sure I can test this effectively unless we have an extension currently deployed on at least one channel.

        GetExtensionLiveChannelsRequest request = new()
        {
            ExtensionId = _fixture.Extension.Id
            // We may need a custom ClientIdentity here with the ExtensionId
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
