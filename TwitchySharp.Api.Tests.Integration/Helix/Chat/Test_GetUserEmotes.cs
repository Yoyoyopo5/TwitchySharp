using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Chat;

namespace TwitchySharp.Api.Tests.Integration.Helix.Chat;
[Collection("helix")]
public class Test_GetUserEmotes(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_GetUserEmotesRequest_ReturnSuccessResponse()
    {
        string userId = await _fixture.GetUserIdFromAccessTokenAsync();

        await _fixture.Api.SendRequestAsync(new GetUserEmotesRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            userId
            ));
    }
}
