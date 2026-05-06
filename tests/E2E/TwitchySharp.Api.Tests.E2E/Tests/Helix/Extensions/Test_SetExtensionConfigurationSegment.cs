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
                ExtensionId = _fixture.Extension.Id,
                Content = "Test Global Segment Content"
            }
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task Send_SetDeveloperExtensionConfigurationSegementRequest_ReturnSuccessResponse()
    {
        SetExtensionConfigurationSegmentRequest request = new()
        {
            ExtensionOwnerId = _fixture.UserIdentity.UserId,
            Configuration = new SetExtensionConfigurationDeveloperSegmentData()
            {
                ExtensionId = _fixture.Extension.Id,
                BroadcasterId = _fixture.UserIdentity.UserId,
                Content = "Test Developer Segment Content"
            }
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task Send_SetBroadcasterExtensionConfigurationSegementRequest_ReturnSuccessResponse()
    {
        SetExtensionConfigurationSegmentRequest request = new()
        {
            ExtensionOwnerId = _fixture.UserIdentity.UserId,
            Configuration = new SetExtensionConfigurationBroadcasterSegmentData()
            {
                ExtensionId = _fixture.Extension.Id,
                BroadcasterId = _fixture.UserIdentity.UserId,
                Content = "Test Broadcaster Segment Content"
            }
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
