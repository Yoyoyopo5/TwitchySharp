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

        TwitchResponse<GetChannelFollowersResponseContent> response = await _fixture.GetTwitchApiClient().SendAsync(request, TestName, TestContext.Current.CancellationToken);
        
        Assert.Null(response.Error);
    }

    [Fact]
    public async Task Send_GetChannelFollowersRequest_ThenPage_ReturnSuccessResponses()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        TestingTwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;
        GetChannelFollowersRequest request = new()
        {
            BroadcasterId = userConfig.UserId,
            First = new(1)
        };

        TwitchResponse<GetChannelFollowersResponseContent> response = await client.SendAsync(request, TestName, ct);

        Assert.NotNull(response.Content);

        Assert.SkipWhen(
            response.Content.Pagination.Cursor is null,
            "Get channel followers request cannot be paged because the cursor was null."
            );

        response = await client.SendAsync(request with { After = response.Content.Pagination.Cursor }, TestName, ct);

        Assert.Null(response.Error);
    }
}
