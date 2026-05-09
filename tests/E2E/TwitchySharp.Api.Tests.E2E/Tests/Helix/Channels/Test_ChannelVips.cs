using TwitchySharp.Api.Helix.Channels;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Channels;

[Collection("twitch")]
public class Test_ChannelVips(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_VipRequests_ReturnSuccessResponse()
    {
        const string TEST_USER_ID = "12345";
        UserId testUserId = new(TEST_USER_ID);
        ITwitchClient client = TwitchClientFixture.Client;
        CancellationToken ct = TestContext.Current.CancellationToken;

        AddChannelVipRequest addRequest = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId,
            UserId = testUserId
        };

        var addResponse = await client.SendAsync(addRequest, ct);
        await Task.Delay(250, ct);

        GetVipsRequest getRequest = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId,
            UserIds = [testUserId]
        };

        var getReponse = await client.SendAsync(getRequest, ct);
        ChannelVip vip = getReponse.Content.Data.First();

        RemoveChannelVipRequest removeRequest = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId,
            UserId = vip.UserId
        };

        await client.SendAsync(removeRequest, ct);
    }
}
