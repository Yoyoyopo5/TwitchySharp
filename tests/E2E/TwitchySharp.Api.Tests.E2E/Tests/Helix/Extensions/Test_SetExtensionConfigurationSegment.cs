using TwitchySharp.Api.Helix.Extensions;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Extensions;

[Collection("twitch")]
public class Test_SetExtensionConfigurationSegment(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_SetGlobalExtensionConfigurationSegementRequest_ReturnSuccessResponse()
    {
        SetExtensionConfigurationSegmentRequest request = new()
        {
            ExtensionOwnerId = _fixture.UserIdentity.UserId,
            Configuration = new SetExtensionConfigurationGlobalSegmentData()
            {
                ExtensionId = TwitchClientFixture.ExtensionConfig.ExtensionId,
                Content = "Test Global Segment Content"
            }
        };

        await TwitchClientFixture.Client.SendAsync(request, TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task Send_SetDeveloperExtensionConfigurationSegementRequest_ReturnSuccessResponse()
    {
        SetExtensionConfigurationSegmentRequest request = new()
        {
            ExtensionOwnerId = _fixture.UserIdentity.UserId,
            Configuration = new SetExtensionConfigurationDeveloperSegmentData()
            {
                ExtensionId = TwitchClientFixture.ExtensionConfig.ExtensionId,
                BroadcasterId = _fixture.UserIdentity.UserId,
                Content = "Test Developer Segment Content"
            }
        };

        await TwitchClientFixture.Client.SendAsync(request, TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task Send_SetBroadcasterExtensionConfigurationSegementRequest_ReturnSuccessResponse()
    {
        SetExtensionConfigurationSegmentRequest request = new()
        {
            ExtensionOwnerId = _fixture.UserIdentity.UserId,
            Configuration = new SetExtensionConfigurationBroadcasterSegmentData()
            {
                ExtensionId = TwitchClientFixture.ExtensionConfig.ExtensionId,
                BroadcasterId = _fixture.UserIdentity.UserId,
                Content = "Test Broadcaster Segment Content"
            }
        };

        await TwitchClientFixture.Client.SendAsync(request, TestContext.Current.CancellationToken);
    }
}
