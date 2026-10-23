using TwitchySharp.Api.Helix.Extensions;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Extensions;

public class Test_GetExtensionLiveChannels(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-extension-live-channels");

    [Fact]
    public async Task Send_GetExtensionLiveChannelsRequest_ReturnSuccessResponse()
    {
        // Not sure I can test this effectively unless we have an extension currently deployed on at least one channel.
        ExtensionConfiguration extensionConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<ExtensionConfiguration>(TestName);

        GetExtensionLiveChannelsRequest request = new()
        {
            ExtensionId = extensionConfig.ExtensionId
        };

        TwitchResponse<GetExtensionLiveChannelsResponseContent> response = await _fixture.GetTwitchApiClient().SendAsync(request, TestName, TestContext.Current.CancellationToken);

        Assert.Null(response.Error);
        Assert.NotNull(response.Content);

        Assert.SkipWhen(response.Content.Data.Length == 0, "The extension was not found or is not being used on any live channels.");
    }
}
