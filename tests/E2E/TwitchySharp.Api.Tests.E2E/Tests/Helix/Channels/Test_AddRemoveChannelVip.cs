using TwitchySharp.Api.Helix.Channels;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Channels;

public class Test_AddRemoveChannelVip(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("add-remove-channel-vip");

    [Fact]
    public async Task Send_AddChannelVipRequestAndRemoveChannelVipRequest_ReturnSuccessResponses()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        ITwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        UserId testUser = new("12345");

        GetVipsRequest getRequest = new()
        {
            BroadcasterId = userConfig.UserId
        };
        TwitchResponse<GetVipsResponse> getResponse = await client.SendAsync(getRequest, ct);

        Assert.SkipWhen(
            getResponse.Content.Data.Any(vip => vip.UserId == testUser),
            $"Test user id {testUser} is already a VIP on the broadcaster's channel."
            );

        AddChannelVipRequest addRequest = new()
        {
            BroadcasterId = userConfig.UserId,
            UserId = testUser
        };
        await client.SendAsync(addRequest, ct);
        await Task.Delay(250, ct);

        RemoveChannelVipRequest removeRequest = new()
        {
            BroadcasterId = userConfig.UserId,
            UserId = testUser
        };
        await client.SendAsync(removeRequest, ct);
    }
}
