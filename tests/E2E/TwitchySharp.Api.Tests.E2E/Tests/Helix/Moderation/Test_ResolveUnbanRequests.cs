using TwitchySharp.Api.Helix.Moderation;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Moderation;

[Collection("twitch")]
public class Test_ResolveUnbanRequests(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_ResolveUnbanRequestsRequest_ReturnSuccessResponse()
    {
        ITwitchClient client = TwitchClientFixture.Client;
        CancellationToken ct = TestContext.Current.CancellationToken;
        UserId broadcasterId = _fixture.UserIdentity.UserId;

        GetUnbanRequestsRequest getRequest = new()
        {
            BroadcasterId = broadcasterId,
            Status = UnbanRequestStatus.Pending,
            ModeratorId = broadcasterId
        };

        var getResponse = await client.SendAsync(getRequest, ct);
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

        await client.SendAsync(resolveRequest, ct);
    }
};
