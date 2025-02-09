using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Teams;

namespace TwitchySharp.Api.Tests.Integration.Helix.Teams;
[Collection("helix")]
public class Test_GetChannelTeams(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_GetChannelTeamsRequest_ReturnSuccessResponse()
    {
        // I don't have access to an account in a team, so not 100% sure this deserializes correctly.
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        await _fixture.Api.SendRequestAsync(new GetChannelTeamsRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId
            ));
    }
}
