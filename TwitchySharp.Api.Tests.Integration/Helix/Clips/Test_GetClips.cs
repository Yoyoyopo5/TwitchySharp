using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Clips;

namespace TwitchySharp.Api.Tests.Integration.Helix.Clips;
[Collection("helix")]
public class Test_GetClips(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_GetClipsRequestBroadcasterId_ReturnSuccessResponse()
    {
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        await _fixture.Api.SendRequestAsync(new GetClipsRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId
            ));
    }

    [Fact]
    public async void Send_GetClipsRequestGameId_ReturnSuccessResponse()
    {
        await _fixture.Api.SendRequestAsync(new GetClipsRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            null,
            "1"
            ));
    }

    [Fact]
    public async void Send_GetClipsRequestClipId_ReturnSuccessResponse()
    {
        await _fixture.Api.SendRequestAsync(new GetClipsRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            null,
            null,
            "ObservantVictoriousButterOSfrog-Uur1-ZdwTmNRzuQ7"
            ));
    }
}
