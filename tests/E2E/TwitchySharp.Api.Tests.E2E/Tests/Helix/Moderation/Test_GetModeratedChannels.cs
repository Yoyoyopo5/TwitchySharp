using TwitchySharp.Api.Helix.Moderation;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Moderation;

public class Test_GetModeratedChannels(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-moderated-channels");

    [Fact]
    public async Task Send_GetModeratedChannels_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        GetModeratedChannelsRequest request = new()
        {
            UserId = userConfig.UserId
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestName, TestContext.Current.CancellationToken);
    }
}
