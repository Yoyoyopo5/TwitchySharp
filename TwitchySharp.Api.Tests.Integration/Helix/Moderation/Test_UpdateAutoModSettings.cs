using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Moderation;

namespace TwitchySharp.Api.Tests.Integration.Helix.Moderation;
[Collection("helix")]
public class Test_UpdateAutoModSettings(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_UpdateAutoModSettingsOverallRequest_ReturnSuccessResponse()
    {
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        await UpdateAutoModSettings(broadcasterId, new UpdateAutoModOverallLevelData(AutoModFilteringLevel.Less));
    }

    [Fact]
    public async void Send_UpdateAutoModSettingsCustomRequest_ReturnSuccessResponse()
    {
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        await UpdateAutoModSettings(broadcasterId, new UpdateAutoModCustomLevelsData()
        {
            Aggression = AutoModFilteringLevel.Less,
            Swearing = AutoModFilteringLevel.None
        });
    }

    private ValueTask<UpdateAutoModSettingsResponse> UpdateAutoModSettings(string broadcasterId, UpdateAutoModSettingsRequestData settings)
        => _fixture.Api.SendRequestAsync(new UpdateAutoModSettingsRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            broadcasterId,
            settings
            ));
}
