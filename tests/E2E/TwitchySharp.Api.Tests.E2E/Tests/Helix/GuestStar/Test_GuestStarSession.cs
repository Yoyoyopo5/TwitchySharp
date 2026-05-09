using TwitchySharp.Api.Helix.GuestStar;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.GuestStar;

[Collection("twitch")]
public class Test_GuestStarSession(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GuestStarSessionRequests_ReturnSuccessResponses()
    {
        const string GUEST_USER_ID = "52137752"; // Yoyoyopo5
        UserId guestUserId = new(GUEST_USER_ID);
        ITwitchClient client = TwitchClientFixture.Client;
        CancellationToken ct = TestContext.Current.CancellationToken;

        var createResponse = await CreateGuestStarSession(client, _fixture.UserIdentity.UserId, ct);
        GuestStarSessionId sessionId = createResponse.Content.Data.Single().Id;
        await Task.Delay(100, ct);

        try
        {
            await SendGuestStarInvite(client, _fixture.UserIdentity.UserId, sessionId, guestUserId, ct);
            await Task.Delay(100, ct);

            await GetGuestStarInvites(client, _fixture.UserIdentity.UserId, sessionId, ct);

            await DeleteGuestStarInvite(client, _fixture.UserIdentity.UserId, sessionId, guestUserId, ct);

            await SendGuestStarInvite(client, _fixture.UserIdentity.UserId, sessionId, guestUserId, ct);

            await Task.Delay(1000, ct); // Wait for join.
            if ((await GetGuestStarSession(client, _fixture.UserIdentity.UserId, ct)).Content.Data.FirstOrDefault()?.Guests.FirstOrDefault() is not GuestStarSessionGuest guest)
                return;

            GuestStarSlotId slot1 = new("1");
            GuestStarSlotId slot2 = new("2");

            await AssignGuestStarSlot(client, _fixture.UserIdentity.UserId, sessionId, guest.UserId, slot1, ct); // Cannot get this to work, not sure when it is acceptable to call.
            await UpdateGuestStarSlotSettings(client, _fixture.UserIdentity.UserId, sessionId, slot1, ct);
            await UpdateGuestStarSlot(client, _fixture.UserIdentity.UserId, sessionId, slot1, slot2, ct);
            await Task.Delay(100, ct);
            await DeleteGuestStarSlot(client, _fixture.UserIdentity.UserId, sessionId, guest.UserId, slot2, ct);
        }
        finally
        {
            await EndGuestStarSession(client, _fixture.UserIdentity.UserId, sessionId, ct);
        }
    }

    private static ValueTask<TwitchResponse<CreateGuestStarSessionResponse>> CreateGuestStarSession(ITwitchClient client, UserId broadcasterId, CancellationToken ct)
        => client.SendAsync(new CreateGuestStarSessionRequest()
        {
            BroadcasterId = broadcasterId
        }, ct);

    private static ValueTask<TwitchResponse<GetGuestStarSessionResponse>> GetGuestStarSession(ITwitchClient client, UserId broadcasterId, CancellationToken ct)
        => client.SendAsync(new GetGuestStarSessionRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId
        }, ct);

    private static ValueTask<TwitchResponse<SendGuestStarInviteResponse>> SendGuestStarInvite(ITwitchClient client, UserId broadcasterId, GuestStarSessionId sessionId, UserId guestId, CancellationToken ct)
        => client.SendAsync(new SendGuestStarInviteRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId,
            SessionId = sessionId,
            GuestId = guestId
        }, ct);

    private static ValueTask<TwitchResponse<GetGuestStarInvitesResponse>> GetGuestStarInvites(ITwitchClient client, UserId broadcasterId, GuestStarSessionId sessionId, CancellationToken ct)
        => client.SendAsync(new GetGuestStarInvitesRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId,
            SessionId = sessionId,
        }, ct);

    private static ValueTask<TwitchResponse<DeleteGuestStarInviteResponse>> DeleteGuestStarInvite(ITwitchClient client, UserId broadcasterId, GuestStarSessionId sessionId, UserId guestId, CancellationToken ct)
        => client.SendAsync(new DeleteGuestStarInviteRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId,
            SessionId = sessionId,
            GuestId = guestId
        }, ct);

    private static ValueTask<TwitchResponse<AssignGuestStarSlotResponse>> AssignGuestStarSlot(ITwitchClient client, UserId broadcasterId, GuestStarSessionId sessionId, UserId guestId, GuestStarSlotId slotId, CancellationToken ct)
        => client.SendAsync(new AssignGuestStarSlotRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId,
            SessionId = sessionId,
            GuestId = guestId,
            SlotId = slotId
        }, ct);

    private static ValueTask<TwitchResponse<UpdateGuestStarSlotSettingsResponse>> UpdateGuestStarSlotSettings(ITwitchClient client, UserId broadcasterId, GuestStarSessionId sessionId, GuestStarSlotId slotId, CancellationToken ct)
        => client.SendAsync(new UpdateGuestStarSlotSettingsRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId,
            SessionId = sessionId,
            SlotId = slotId,
            Settings = new()
            {
                IsAudioEnabled = true,
                IsLive = true,
                IsVideoEnabled = true,
                Volume = 50
            }
        }, ct);

    private static ValueTask<TwitchResponse<UpdateGuestStarSlotResponse>> UpdateGuestStarSlot(ITwitchClient client, UserId broadcasterId, GuestStarSessionId sessionId, GuestStarSlotId fromSlotId, GuestStarSlotId toSlotId, CancellationToken ct)
        => client.SendAsync(new UpdateGuestStarSlotRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId,
            SessionId = sessionId,
            SourceSlotId = fromSlotId,
            DestinationSlotId = toSlotId
        }, ct);

    private static ValueTask<TwitchResponse<DeleteGuestStarSlotResponse>> DeleteGuestStarSlot(ITwitchClient client, UserId broadcasterId, GuestStarSessionId sessionId, UserId guestId, GuestStarSlotId slotId, CancellationToken ct)
        => client.SendAsync(new DeleteGuestStarSlotRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId,
            SessionId = sessionId,
            GuestId = guestId,
            SlotId = slotId
        }, ct);

    private static ValueTask<TwitchResponse<EndGuestStarSessionResponse>> EndGuestStarSession(ITwitchClient client, UserId broadcasterId, GuestStarSessionId sessionId, CancellationToken ct)
        => client.SendAsync(new EndGuestStarSessionRequest()
        {
            BroadcasterId = broadcasterId,
            SessionId = sessionId
        }, ct);
}
