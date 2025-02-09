using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Raids;

namespace TwitchySharp.Api.Tests.Integration.Helix.Raids;
[Collection("helix")]
public class Test_Raid(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_RaidRequests_ReturnSuccessResponses()
    {
        const string toBroadcasterId = "52137752";
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        await StartRaid(broadcasterId, toBroadcasterId);
    }

    private ValueTask<StartRaidResponse> StartRaid(string from, string to)
        => _fixture.Api.SendRequestAsync(new StartRaidRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            from, 
            to
            ));

    private ValueTask<CancelRaidResponse> CancelRaid(string broadcasterId)
        => _fixture.Api.SendRequestAsync(new CancelRaidRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId
            ));
}
