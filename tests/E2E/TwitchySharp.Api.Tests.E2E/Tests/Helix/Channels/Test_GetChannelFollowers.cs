using TwitchySharp.Api.Helix.Channels;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Channels;

public class Test_GetChannelFollowers(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-channel-followers");

    [Fact]
    public async Task Send_GetChannelFollowersRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        GetChannelFollowersRequest request = new()
        {
            BroadcasterId = userConfig.UserId,
            First = new(10)
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task Send_GetChannelFollowersRequest_ThenPage_ReturnSuccessResponses()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        ITwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;
        GetChannelFollowersRequest request = new()
        {
            BroadcasterId = userConfig.UserId,
            First = new(1)
        };

        TwitchResponse<GetChannelFollowersResponse> response = await client.SendAsync(request, ct);

        Assert.SkipWhen(
            response.Content.Pagination.Cursor is null,
            "Get channel followers request cannot be paged because the cursor was null."
            );

        await client.SendAsync(request with { After = response.Content.Pagination.Cursor }, ct);
    }
}
