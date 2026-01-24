using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Moderation;
using TwitchySharp.Api.Helix.Moderation.Enums;

namespace TwitchySharp.Api.Tests.E2E.Helix.Moderation;
[Collection("helix")]
public class Test_GetUnbanRequests(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_GetUnbanRequestsRequest_ReturnSuccessResponse()
    {
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        await _fixture.Api.SendRequestAsync(new GetUnbanRequestsRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            broadcasterId,
            UnbanRequestStatus.Pending
            ));
    }
}
