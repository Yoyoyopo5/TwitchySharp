using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Streams;

namespace TwitchySharp.Api.Tests.Integration.Helix.Streams;
[Collection("helix")]
public class Test_GetStreams(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_GetStreamsRequest_ReturnSuccessResponse()
    {
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        await _fixture.Api.SendRequestAsync(new GetStreamsRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            new StreamQuery()
            {
                UserIds = [ broadcasterId ]
            }
            ));
    }
}
