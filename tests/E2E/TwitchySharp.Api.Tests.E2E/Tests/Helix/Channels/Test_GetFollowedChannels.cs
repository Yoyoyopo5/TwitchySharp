using TwitchySharp.Api.Helix.Channels;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Channels;

public class Test_GetFollowedChannels(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private readonly static TestName TestName = new("get-followed-channels");

    [Fact]
    public async Task Send_GetFollowedChannelsRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        GetFollowedChannelsRequest request = new()
        {
            UserId = userConfig.UserId
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
