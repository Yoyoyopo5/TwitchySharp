using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Moderation;
using TwitchySharp.Api.Tests.Integration.Helix.Channels;

namespace TwitchySharp.Api.Tests.Integration.Helix.Moderation;
[Collection("helix")]
public class Test_AddRemoveModerator(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_AddRemoveModeratorRequests_ReturnSuccessResponses()
    {
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();
        const string moderatorId = "52137750";

        await AddChannelModerator(broadcasterId, moderatorId);
        await RemoveChannelModerator(broadcasterId, moderatorId);
    }

    private ValueTask<AddChannelModeratorResponse> AddChannelModerator(string broadcasterId, string moderatorId)
        => _fixture.Api.SendRequestAsync(new AddChannelModeratorRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            moderatorId
            ));

    private ValueTask<RemoveChannelModeratorResponse> RemoveChannelModerator(string broadcasterId, string moderatorId)
        => _fixture.Api.SendRequestAsync(new RemoveChannelModeratorRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            moderatorId
            ));
}
