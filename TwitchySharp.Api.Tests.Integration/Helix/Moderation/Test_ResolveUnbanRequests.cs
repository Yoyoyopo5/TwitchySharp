using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Moderation;

namespace TwitchySharp.Api.Tests.Integration.Helix.Moderation;
[Collection("helix")]
public class Test_ResolveUnbanRequests(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_ResolveUnbanRequestsRequest_ReturnSuccessResponse()
    {
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        string unbanRequestId = (await GetUnbanRequests(broadcasterId)).Data.First().Id;
        await _fixture.Api.SendRequestAsync(new ResolveUnbanRequestsRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            broadcasterId,
            unbanRequestId,
            UnbanRequestResolutionStatus.Approved,
            "Lucky you :)"
            ));
    }

    private ValueTask<GetUnbanRequestsResponse> GetUnbanRequests(string broadcasterId)
        => _fixture.Api.SendRequestAsync(new GetUnbanRequestsRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
             broadcasterId,
             broadcasterId,
             UnbanRequestStatus.Pending
            ));
};
