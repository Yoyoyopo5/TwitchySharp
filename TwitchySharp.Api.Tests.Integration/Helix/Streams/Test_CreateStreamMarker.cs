using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Streams.Requests;

namespace TwitchySharp.Api.Tests.Integration.Helix.Streams;
[Collection("helix")]
public class Test_CreateStreamMarker(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_CreateStreamMarkerRequest_ReturnSuccessResponse()
    {
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        await _fixture.Api.SendRequestAsync(new CreateStreamMarkerRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            new CreateStreamMarkerRequestData()
            {
                UserId = broadcasterId,
                Description = "test-marker"
            }
            ));
    }
}
