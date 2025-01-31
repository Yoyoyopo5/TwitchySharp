using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Channels;

namespace TwitchySharp.Api.Tests.Integration.Helix.Channels;
[Collection("helix")]
public class Test_ChannelVips(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_VipRequests_ReturnSuccessResponse()
    {
        const string TEST_USER_ID = "52137752";

        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        await AddVip(TEST_USER_ID, broadcasterId);
        ChannelVip addedVip = await GetVip(TEST_USER_ID, broadcasterId);
        await RemoveVip(addedVip, broadcasterId);
    }

    private async ValueTask AddVip(string userId, string broadcasterId)
        => await _fixture.Api.SendRequestAsync(
                new AddChannelVipRequest(
                    _fixture.Secrets.Client.Id,
                    _fixture.Secrets.User.AccessToken,
                    broadcasterId,
                    userId
                    )
                );

    private async ValueTask<ChannelVip> GetVip(string userId, string broadcasterId)
        => (await _fixture.Api.SendRequestAsync(
                new GetVipsRequest(
                    _fixture.Secrets.Client.Id,
                    _fixture.Secrets.User.AccessToken,
                    broadcasterId,
                    [userId]
                    )
            )).Data.First();

    private async ValueTask RemoveVip(ChannelVip vip, string broadcasterId)
        => await _fixture.Api.SendRequestAsync(
                new RemoveChannelVipRequest(
                    _fixture.Secrets.Client.Id,
                    _fixture.Secrets.User.AccessToken,
                    broadcasterId,
                    vip.UserId
                    )
            );
}
