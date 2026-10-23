using TwitchySharp.Api.Helix.Bits;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Bits;

public class Test_GetCustomPowerUpsRequest(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-custom-power-ups");

    [Fact]
    public async Task Send_GetCustomPowerUpsRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        GetCustomPowerUpsRequest request = new()
        {
            BroadcasterId = userConfig.UserId
        };

        TwitchResponse<GetCustomPowerUpsResponseContent> response = await _fixture.GetTwitchApiClient().SendAsync(request, TestName, TestContext.Current.CancellationToken);
        
        Assert.Null(response.Error);
    }
}
