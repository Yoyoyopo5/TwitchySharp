using TwitchySharp.Api.Helix.Moderation;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Moderation;

public class Test_GetUnbanRequests(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-unban-requests");

    [Fact]
    public async Task Send_GetUnbanRequestsRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        GetUnbanRequestsRequest request = new()
        {
            BroadcasterId = userConfig.UserId,
            Status = UnbanRequestStatus.Pending,
            ModeratorId = userConfig.UserId
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestName, TestContext.Current.CancellationToken);
    }
}
