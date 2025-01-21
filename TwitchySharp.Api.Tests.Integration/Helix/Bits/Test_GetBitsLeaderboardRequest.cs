using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Bits;

namespace TwitchySharp.Api.Tests.Integration.Helix.Bits;
[Collection("helix")]
public class Test_GetBitsLeaderboardRequest(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_GetBitsLeaderboardRequest_ReturnSuccessResponse()
    {
        GetBitsLeaderboardRequest stubRequest = new(_fixture.Secrets.ClientId, _fixture.Secrets.UserAccessToken, 5, LeaderboardPeriod.All);

        GetBitsLeaderboardResponse actualResponse = await _fixture.Api.SendRequestAsync(stubRequest);
    }
}
