using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Moderation;

namespace TwitchySharp.Api.Tests.Integration.Helix.Moderation;
[Collection("helix")]
public class Test_ShieldMode(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_ShieldModeRequests_ReturnSuccessResponses()
    {
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        await UpdateShieldModeStatus(broadcasterId, true);
        await Task.Delay(500);
        bool isActive = (await GetShieldModeStatus(broadcasterId)).Data.First().IsActive;

        Assert.True(isActive);

        await UpdateShieldModeStatus(broadcasterId, false);
        await Task.Delay(500);
        isActive = (await GetShieldModeStatus(broadcasterId)).Data.First().IsActive;

        Assert.False(isActive);
    }

    private ValueTask<UpdateShieldModeStatusResponse> UpdateShieldModeStatus(string broadcasterId, bool isActive)
        => _fixture.Api.SendRequestAsync(new UpdateShieldModeStatusRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            broadcasterId,
            new UpdateShieldModeStatusRequestData()
            {
                IsActive = isActive
            }
            ));

    private ValueTask<GetShieldModeStatusResponse> GetShieldModeStatus(string broadcasterId)
        => _fixture.Api.SendRequestAsync(new GetShieldModeStatusRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            broadcasterId
            ));
}
