using TwitchySharp.Api.Helix.Moderation;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Moderation;

public class Test_ResolveUnbanRequests(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("resolve-unban-requests");

    [Fact]
    public async Task Send_ResolveUnbanRequestsRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        TestingTwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;
        UserId broadcasterId = userConfig.UserId;

        GetUnbanRequestsRequest getRequest = new()
        {
            BroadcasterId = broadcasterId,
            Status = UnbanRequestStatus.Pending,
            ModeratorId = broadcasterId
        };

        TwitchResponse<GetUnbanRequestsResponseContent> getResponse = await client.SendAsync(getRequest, TestName, ct);
        if (getResponse.Content.Data.FirstOrDefault() is not UnbanRequest unbanRequest)
            return; // Can only test this if we have pending request.

        ResolveUnbanRequestsRequest resolveRequest = new()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId,
            Status = UnbanRequestResolutionStatus.Approved,
            UnbanRequestId = unbanRequest.Id,
            ResolutionText = "test resolution"
        };

        await client.SendAsync(resolveRequest, TestName, ct);
    }
};
