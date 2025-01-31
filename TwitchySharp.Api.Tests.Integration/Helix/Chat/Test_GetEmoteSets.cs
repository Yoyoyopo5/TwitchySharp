using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Chat;
using TwitchySharp.Api.Helix.Chat.Requests;

namespace TwitchySharp.Api.Tests.Integration.Helix.Chat;
[Collection("helix")]
public class Test_GetEmoteSets(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_GetEmoteSetsRequest_ReturnSuccessResponse()
    {
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        string emoteSetId = await GetEmoteSetId(broadcasterId);

        await _fixture.Api.SendRequestAsync(new GetEmoteSetsRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            [emoteSetId]
            ));
    }

    private async ValueTask<string> GetEmoteSetId(string broadcasterId)
        => (await _fixture.Api.SendRequestAsync(new GetChannelEmotesRequest(
                _fixture.Secrets.Client.Id,
                _fixture.Secrets.User.AccessToken,
                broadcasterId
            ))).Data.First().EmoteSetId;
}
