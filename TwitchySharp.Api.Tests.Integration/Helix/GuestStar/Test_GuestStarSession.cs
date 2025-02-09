using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.GuestStar;

namespace TwitchySharp.Api.Tests.Integration.Helix.GuestStar;
[Collection("helix")]
public class Test_GuestStarSession(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_GuestStarSessionRequests_ReturnSuccessResponses()
    {
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();
        const string guestUserId = "52137752"; // Yoyoyopo5, maybe add this to config somewhere.

        string sessionId = (await CreateGuestStarSession(broadcasterId)).Data.First().Id;
        await SendGuestStarInvite(broadcasterId, sessionId, guestUserId);
        await DeleteGuestStarInvite(broadcasterId, sessionId, guestUserId);
        await SendGuestStarInvite(broadcasterId, sessionId, guestUserId);
        await GetGuestStarInvites(broadcasterId, sessionId);
        //await AssignGuestStarSlot(broadcasterId, sessionId, guestUserId); // Cannot get this to work, not sure when it is acceptable to call.
        await Task.Delay(5000); // Wait for join.
        await UpdateGuestStarSlotSettings(broadcasterId, sessionId);
        await UpdateGuestStarSlot(broadcasterId, sessionId);
        await DeleteGuestStarSlot(broadcasterId, sessionId, guestUserId);
        await EndGuestStarSession(broadcasterId, sessionId);
    }

    private ValueTask<CreateGuestStarSessionResponse> CreateGuestStarSession(string broadcasterId)
        => _fixture.Api.SendRequestAsync(new CreateGuestStarSessionRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId
            ));

    private ValueTask<GetGuestStarSessionResponse> GetGuestStarSession(string broadcasterId)
        => _fixture.Api.SendRequestAsync(new GetGuestStarSessionRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            broadcasterId
            ));

    private ValueTask<SendGuestStarInviteResponse> SendGuestStarInvite(string broadcasterId, string sessionId, string userId)
        => _fixture.Api.SendRequestAsync(new SendGuestStarInviteRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            broadcasterId,
            sessionId,
            userId
            ));

    private ValueTask<GetGuestStarInvitesResponse> GetGuestStarInvites(string broadcasterId, string sessionId)
        => _fixture.Api.SendRequestAsync(new GetGuestStarInvitesRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            broadcasterId,
            sessionId
            ));

    private ValueTask<DeleteGuestStarInviteResponse> DeleteGuestStarInvite(string broadcasterId, string sessionId, string guestId)
        => _fixture.Api.SendRequestAsync(new DeleteGuestStarInviteRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            broadcasterId,
            sessionId,
            guestId
            ));

    private ValueTask<AssignGuestStarSlotResponse> AssignGuestStarSlot(string broadcasterId, string sessionId, string guestId)
        => _fixture.Api.SendRequestAsync(new AssignGuestStarSlotRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            broadcasterId,
            sessionId,
            guestId,
            "1"
            ));

    private async ValueTask<UpdateGuestStarSlotSettingsResponse> UpdateGuestStarSlotSettings(string broadcasterId, string sessionId)
        => await _fixture.Api.SendRequestAsync(new UpdateGuestStarSlotSettingsRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            broadcasterId,
            sessionId,
            "1",
            new GuestStarSlotSettings()
            {
                IsAudioEnabled = false,
                IsVideoEnabled = false
            }
            ));

    private ValueTask<UpdateGuestStarSlotResponse> UpdateGuestStarSlot(string broadcasterId, string sessionId)
        => _fixture.Api.SendRequestAsync(new UpdateGuestStarSlotRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            broadcasterId,
            sessionId,
            "1",
            "2"
            ));

    private ValueTask<DeleteGuestStarSlotResponse> DeleteGuestStarSlot(string broadcasterId, string sessionId, string guestId)
        => _fixture.Api.SendRequestAsync(new DeleteGuestStarSlotRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            broadcasterId,
            sessionId,
            guestId,
            "2"
            ));

    private ValueTask<EndGuestStarSessionResponse> EndGuestStarSession(string broadcasterId, string sessionId)
        => _fixture.Api.SendRequestAsync(new EndGuestStarSessionRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            sessionId
            ));
}
