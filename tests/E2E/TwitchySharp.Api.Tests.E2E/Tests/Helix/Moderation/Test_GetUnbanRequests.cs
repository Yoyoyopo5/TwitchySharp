using TwitchySharp.Api.Helix.Moderation;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Moderation;

[Collection("twitch")]
public class Test_GetUnbanRequests(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetUnbanRequestsRequest_ReturnSuccessResponse()
    {
        GetUnbanRequestsRequest request = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId,
            Status = UnbanRequestStatus.Pending,
            ModeratorId = _fixture.UserIdentity.UserId
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
