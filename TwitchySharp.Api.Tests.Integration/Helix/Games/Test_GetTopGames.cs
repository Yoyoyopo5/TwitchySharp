using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Games;

namespace TwitchySharp.Api.Tests.Integration.Helix.Games;
[Collection("helix")]
public class Test_GetTopGames(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_GetTopGamesRequest_ReturnSuccessResponse()
    {
        await _fixture.Api.SendRequestAsync(new GetTopGamesRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken
            ));
    }
}
