using TwitchySharp.Api.Helix.GuestStar;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.GuestStar;

public class Test_GuestStarSession(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("guest-star-session");

    [Fact]
    public async Task Send_GuestStarSessionRequests_ReturnSuccessResponses()
    {
        // This test is jank as hell, pls fix.

        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        const string GUEST_USER_ID = "52137752"; // Yoyoyopo5
        UserId guestUserId = new(GUEST_USER_ID);

        TestingTwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        TwitchResponse<CreateGuestStarSessionResponseContent> createResponse
            = await CreateGuestStarSession(client, userConfig.UserId, ct);
        Assert.NotNull(createResponse.Content);
        GuestStarSessionId sessionId = createResponse.Content.Data.Single().Id;

        try
        {
            await Task.Delay(100, ct);

            Assert.Null((await SendGuestStarInvite(
                client,
                userConfig.UserId,
                sessionId,
                guestUserId,
                ct
                )).Error);

            await Task.Delay(100, ct);

            Assert.Null((await GetGuestStarInvites(
                client,
                userConfig.UserId,
                sessionId,
                ct
                )).Error);

            Assert.Null((await DeleteGuestStarInvite(
                client,
                userConfig.UserId,
                sessionId,
                guestUserId,
                ct
                )).Error);

            Assert.Null((await SendGuestStarInvite(
                client,
                userConfig.UserId,
                sessionId,
                guestUserId,
                ct
                )).Error);

            await Task.Delay(TimeSpan.FromSeconds(5), ct); // Wait for join.

            TwitchResponse<GetGuestStarSessionResponseContent> getSessionResponse = await GetGuestStarSession(client, userConfig.UserId, ct);
            Assert.NotNull(getSessionResponse.Content);

            GuestStarSessionGuest? guest = getSessionResponse.Content.Data
                .FirstOrDefault()?
                .Guests
                .ElementAtOrDefault(1);

            Assert.SkipWhen(
                guest is null,
                "The invited guest did not join the session."
                );

            GuestStarSlotId slot1 = new("1");
            GuestStarSlotId slot2 = new("2");

            Assert.Null((await AssignGuestStarSlot(client,
                userConfig.UserId,
                sessionId,
                guest.UserId,
                slot1,
                ct
                )).Error); // Cannot get this to work, not sure when it is acceptable to call.

            Assert.Null((await UpdateGuestStarSlotSettings(
                client,
                userConfig.UserId,
                sessionId,
                slot1,
                ct
                )).Error);

            Assert.Null((await UpdateGuestStarSlot(
                client,
                userConfig.UserId,
                sessionId,
                slot1,
                slot2,
                ct
                )).Error);

            await Task.Delay(100, ct);
            Assert.Null((await DeleteGuestStarSlot(
                client,
                userConfig.UserId,
                sessionId,
                guest.UserId,
                slot2,
                ct
                )).Error);
        }
        finally
        {
            Assert.Null((await EndGuestStarSession(client, userConfig.UserId, sessionId, ct)).Error);
        }
    }

    private static Task<TwitchResponse<CreateGuestStarSessionResponseContent>> CreateGuestStarSession(TestingTwitchClient client, UserId broadcasterId, CancellationToken ct)
        => client.SendAsync(new CreateGuestStarSessionRequest()
        {
            BroadcasterId = broadcasterId
        }, TestName, ct);

    private static Task<TwitchResponse<GetGuestStarSessionResponseContent>> GetGuestStarSession(TestingTwitchClient client, UserId broadcasterId, CancellationToken ct)
        => client.SendAsync(new GetGuestStarSessionRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId
        }, TestName, ct);

    private static Task<TwitchResponse<SendGuestStarInviteResponseContent>> SendGuestStarInvite(TestingTwitchClient client, UserId broadcasterId, GuestStarSessionId sessionId, UserId guestId, CancellationToken ct)
        => client.SendAsync(new SendGuestStarInviteRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId,
            SessionId = sessionId,
            GuestId = guestId
        }, TestName, ct);

    private static Task<TwitchResponse<GetGuestStarInvitesResponseContent>> GetGuestStarInvites(TestingTwitchClient client, UserId broadcasterId, GuestStarSessionId sessionId, CancellationToken ct)
        => client.SendAsync(new GetGuestStarInvitesRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId,
            SessionId = sessionId,
        }, TestName, ct);

    private static Task<TwitchResponse<DeleteGuestStarInviteResponseContent>> DeleteGuestStarInvite(TestingTwitchClient client, UserId broadcasterId, GuestStarSessionId sessionId, UserId guestId, CancellationToken ct)
        => client.SendAsync(new DeleteGuestStarInviteRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId,
            SessionId = sessionId,
            GuestId = guestId
        }, TestName, ct);

    private static Task<TwitchResponse<AssignGuestStarSlotResponseContent>> AssignGuestStarSlot(TestingTwitchClient client, UserId broadcasterId, GuestStarSessionId sessionId, UserId guestId, GuestStarSlotId slotId, CancellationToken ct)
        => client.SendAsync(new AssignGuestStarSlotRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId,
            SessionId = sessionId,
            GuestId = guestId,
            SlotId = slotId
        }, TestName, ct);

    private static Task<TwitchResponse<UpdateGuestStarSlotSettingsResponseContent>> UpdateGuestStarSlotSettings(TestingTwitchClient client, UserId broadcasterId, GuestStarSessionId sessionId, GuestStarSlotId slotId, CancellationToken ct)
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
                Volume = new(50)
            }
        }, TestName, ct);

    private static Task<TwitchResponse<UpdateGuestStarSlotResponseContent>> UpdateGuestStarSlot(TestingTwitchClient client, UserId broadcasterId, GuestStarSessionId sessionId, GuestStarSlotId fromSlotId, GuestStarSlotId toSlotId, CancellationToken ct)
        => client.SendAsync(new UpdateGuestStarSlotRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId,
            SessionId = sessionId,
            SourceSlotId = fromSlotId,
            DestinationSlotId = toSlotId
        }, TestName, ct);

    private static Task<TwitchResponse<DeleteGuestStarSlotResponseContent>> DeleteGuestStarSlot(TestingTwitchClient client, UserId broadcasterId, GuestStarSessionId sessionId, UserId guestId, GuestStarSlotId slotId, CancellationToken ct)
        => client.SendAsync(new DeleteGuestStarSlotRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId,
            SessionId = sessionId,
            GuestId = guestId,
            SlotId = slotId
        }, TestName, ct);

    private static Task<TwitchResponse<EndGuestStarSessionResponseContent>> EndGuestStarSession(TestingTwitchClient client, UserId broadcasterId, GuestStarSessionId sessionId, CancellationToken ct)
        => client.SendAsync(new EndGuestStarSessionRequest()
        {
            BroadcasterId = broadcasterId,
            SessionId = sessionId
        }, TestName, ct);
}
