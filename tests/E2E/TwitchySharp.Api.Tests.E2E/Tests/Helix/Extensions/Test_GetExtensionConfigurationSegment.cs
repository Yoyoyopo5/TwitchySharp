using TwitchySharp.Api.Helix.Extensions;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Extensions;

[Collection("twitch")]
public class Test_GetExtensionConfigurationSegment(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetExtensionConfigurationSegmentRequest_ReturnSuccessResponse()
    {
        GetExtensionConfigurationSegmentRequest request = new GetExtensionConfigurationSegmentRequest()
        {
            ExtensionId = _fixture.Extension.Id,
            ExtensionIdentity = _fixture.ExtensionIdentity with { BroadcasterId = _fixture.UserIdentity.UserId }
        }
            .WithGlobal()
            .WithDeveloper(_fixture.UserIdentity.UserId)
            .WithBroadcaster(_fixture.UserIdentity.UserId);

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
