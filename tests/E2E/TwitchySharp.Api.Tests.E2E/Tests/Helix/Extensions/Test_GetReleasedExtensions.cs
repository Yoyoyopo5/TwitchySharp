using TwitchySharp.Api.Helix.Extensions;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Extensions;

[Collection("twitch")]
public class Test_GetReleasedExtensions(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetReleasedExtensionsRequest_ReturnSuccessResponse()
    {
        // Can't really test this one until we have a released extension.
        // Need to make sure an arbitary client id is allowed.

        GetReleasedExtensionsRequest request = new()
        {
            ExtensionId = TwitchClientFixture.ExtensionConfig.ExtensionId
        };

        await TwitchClientFixture.Client.SendAsync(request, TestContext.Current.CancellationToken);
    }
}
