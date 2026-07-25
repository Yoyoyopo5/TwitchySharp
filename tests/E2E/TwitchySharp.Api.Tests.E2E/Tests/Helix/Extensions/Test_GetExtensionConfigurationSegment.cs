using TwitchySharp.Api.Helix.Extensions;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Extensions;

public class Test_GetExtensionConfigurationSegment(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-extension-configuration-segment");

    [Fact]
    public async Task Send_GetExtensionConfigurationSegmentRequest_ReturnSuccessResponse()
    {
        ExtensionConfiguration extensionConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<ExtensionConfiguration>(TestName);

        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        GetExtensionConfigurationSegmentRequest request = new GetExtensionConfigurationSegmentRequest()
        {
            ExtensionId = extensionConfig.ExtensionId,
            ExtensionIdentity = extensionConfig.ToIdentity() with { BroadcasterId = userConfig.UserId }
        }
            .WithGlobal()
            .WithDeveloper(userConfig.UserId)
            .WithBroadcaster(userConfig.UserId);

        await _fixture.GetTwitchApiClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
