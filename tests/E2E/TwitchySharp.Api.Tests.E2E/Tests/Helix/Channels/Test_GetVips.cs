using TwitchySharp.Api.Helix.Channels;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Channels;

public class Test_GetVips(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-vips");

    [Fact]
    public async Task Send_GetVipsRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        GetVipsRequest request = new()
        {
            BroadcasterId = userConfig.UserId
        };

        TwitchResponse<GetVipsResponseContent> response = await _fixture.GetTwitchApiClient().SendAsync(request, TestName, TestContext.Current.CancellationToken);

        Assert.Null(response.Error);
    }
}
