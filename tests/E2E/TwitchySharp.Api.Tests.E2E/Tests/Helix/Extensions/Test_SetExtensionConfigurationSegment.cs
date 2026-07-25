using TwitchySharp.Api.Helix.Extensions;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Extensions;

public class Test_SetExtensionConfigurationSegment(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("set-extension-configuration-segment");

    [Fact]
    public async Task Send_SetGlobalExtensionConfigurationSegementRequest_ReturnSuccessResponse()
    {
        TestName testName = new(TestName.Value + "-global");

        ExtensionConfiguration extensionConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<ExtensionConfiguration>(testName);

        SetExtensionConfigurationSegmentRequest request = new()
        {
            ExtensionOwnerId = extensionConfig.ExtensionOwnerUserId,
            Configuration = new SetExtensionConfigurationGlobalSegmentData()
            {
                ExtensionId = extensionConfig.ExtensionId,
                Content = "Test Global Segment Content"
            }
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task Send_SetDeveloperExtensionConfigurationSegementRequest_ReturnSuccessResponse()
    {
        TestName testName = new(TestName.Value + "-developer");

        ExtensionConfiguration extensionConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<ExtensionConfiguration>(testName);

        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(testName);

        SetExtensionConfigurationSegmentRequest request = new()
        {
            ExtensionOwnerId = userConfig.UserId,
            Configuration = new SetExtensionConfigurationDeveloperSegmentData()
            {
                ExtensionId = extensionConfig.ExtensionId,
                BroadcasterId = userConfig.UserId,
                Content = "Test Developer Segment Content"
            }
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task Send_SetBroadcasterExtensionConfigurationSegementRequest_ReturnSuccessResponse()
    {
        TestName testName = new(TestName.Value + "-broadcaster");

        ExtensionConfiguration extensionConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<ExtensionConfiguration>(testName);

        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(testName);

        SetExtensionConfigurationSegmentRequest request = new()
        {
            ExtensionOwnerId = userConfig.UserId,
            Configuration = new SetExtensionConfigurationBroadcasterSegmentData()
            {
                ExtensionId = extensionConfig.ExtensionId,
                BroadcasterId = userConfig.UserId,
                Content = "Test Broadcaster Segment Content"
            }
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
