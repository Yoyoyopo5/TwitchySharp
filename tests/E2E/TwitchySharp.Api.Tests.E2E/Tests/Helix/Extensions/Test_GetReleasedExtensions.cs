using TwitchySharp.Api.Helix.Extensions;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Extensions;

public class Test_GetReleasedExtensions(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-released-extensions");

    [Fact]
    public async Task Send_GetReleasedExtensionsRequest_ReturnSuccessResponse()
    {
        // Can't really test this one until we have a released extension.
        // Need to make sure an arbitary client id is allowed.

        ExtensionConfiguration extensionConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<ExtensionConfiguration>(TestName);

        GetReleasedExtensionsRequest request = new()
        {
            ExtensionId = extensionConfig.ExtensionId
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestName, TestContext.Current.CancellationToken);
    }
}
