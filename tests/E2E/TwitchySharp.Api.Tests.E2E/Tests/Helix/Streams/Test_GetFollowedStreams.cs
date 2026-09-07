using TwitchySharp.Api.Helix.Streams;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Streams;

public class Test_GetFollowedStreams(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-followed-streams");

    [Fact]
    public async Task Send_GetFollowedStreamsRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        GetFollowedStreamsRequest request = new()
        {
            UserId = userConfig.UserId
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestName, TestContext.Current.CancellationToken);
    }
}
