using TwitchySharp.Api.Helix.GuestStar;

namespace TwitchySharp.Api.Tests.Unit.Helix.GuestStar;

public class Test_UpdateGuestStarSlotRequest
{
    [Fact]
    public void QueryString_ContainsAllRequiredParams()
    {
        UpdateGuestStarSlotRequest request = new()
        {
            BroadcasterId = new UserId("111"),
            ModeratorId = new UserId("222"),
            SessionId = new GuestStarSessionId("session-1"),
            SourceSlotId = new GuestStarSlotId("1"),
            DestinationSlotId = new GuestStarSlotId("2")
        };

        string queryString = request.RequestUri.Query;

        Assert.Contains("broadcaster_id=111", queryString);
        Assert.Contains("moderator_id=222", queryString);
        Assert.Contains("session_id=session-1", queryString);
        Assert.Contains("source_slot_id=1", queryString);
        Assert.Contains("destination_slot_id=2", queryString);
    }

    [Fact]
    public void RequestUri_Path_IsGuestStarSlot()
    {
        UpdateGuestStarSlotRequest request = new()
        {
            BroadcasterId = new UserId("111"),
            ModeratorId = new UserId("222"),
            SessionId = new GuestStarSessionId("session-1"),
            SourceSlotId = new GuestStarSlotId("1"),
            DestinationSlotId = new GuestStarSlotId("2")
        };

        Assert.EndsWith("/guest_star/slot", request.RequestUri.AbsolutePath);
    }
}
