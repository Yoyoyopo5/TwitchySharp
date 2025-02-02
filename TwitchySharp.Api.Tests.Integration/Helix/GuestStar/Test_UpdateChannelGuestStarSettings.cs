using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.GuestStar;

namespace TwitchySharp.Api.Tests.Integration.Helix.GuestStar;
[Collection("helix")]
public class Test_UpdateChannelGuestStarSettings(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_UpdateChannelGuestStarSettingsRequest_ReturnSuccessResponse()
    {
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        await _fixture.Api.SendRequestAsync(new UpdateChannelGuestStarSettingsRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            new UpdateChannelGuestStarSettingsRequestData()
            {
                GroupLayout = GuestStarGroupLayout.Tiled,
                SlotCount = 2,
                IsBrowserSourceAudioEnabled = false,
                IsModeratorSendLiveEnabled = false,
                RegenerateBrowserSources = false,
            }
            ));
    }
}
