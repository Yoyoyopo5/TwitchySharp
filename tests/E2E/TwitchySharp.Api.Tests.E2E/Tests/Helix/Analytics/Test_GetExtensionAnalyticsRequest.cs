using TwitchySharp.Api.Helix.Analytics;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Analytics;

public class Test_GetExtensionAnalyticsRequest(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    private static readonly TestName TestName = new("get-extension-analytics");

    [Fact]
    public async Task Send_GetExtensionAnalyticsRequest_ReturnSuccessResponse()
    {
        UserConfiguration? userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        GetExtensionAnalyticsRequest request = new()
        {
            UserId = userConfig.UserId
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
