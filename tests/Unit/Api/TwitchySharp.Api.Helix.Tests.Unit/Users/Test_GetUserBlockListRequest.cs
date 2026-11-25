using TwitchySharp.Api.Helix.Users;

namespace TwitchySharp.Api.Tests.Unit.Helix.Users;

public class Test_GetUserBlockListRequest
{
    private const string MOCK_BROADCASTER_ID = "141981764";

    [Fact]
    public void RequestUri_Path_IsUsersBlocks()
    {
        GetUserBlockListRequest request = new()
        {
            BroadcasterId = new UserId(MOCK_BROADCASTER_ID)
        };

        Assert.EndsWith("/users/blocks", request.RequestUri.AbsolutePath);
    }

    [Fact]
    public void NoUserIds_QueryString_ContainsBroadcasterIdAndOmitsUserId()
    {
        GetUserBlockListRequest request = new()
        {
            BroadcasterId = new UserId(MOCK_BROADCASTER_ID)
        };

        string queryString = request.RequestUri.Query;

        Assert.Contains($"broadcaster_id={MOCK_BROADCASTER_ID}", queryString);
        Assert.DoesNotContain("user_id=", queryString);
    }

    [Fact]
    public void SingleUserId_QueryString_ContainsUserId()
    {
        GetUserBlockListRequest request = new()
        {
            BroadcasterId = new UserId(MOCK_BROADCASTER_ID),
            UserIds = [new UserId("123")]
        };

        string queryString = request.RequestUri.Query;

        Assert.Contains($"broadcaster_id={MOCK_BROADCASTER_ID}", queryString);
        Assert.Contains("user_id=123", queryString);
    }

    [Fact]
    public void MultipleUserIds_QueryString_ContainsAllUserIds()
    {
        GetUserBlockListRequest request = new()
        {
            BroadcasterId = new UserId(MOCK_BROADCASTER_ID),
            UserIds = [new UserId("123"), new UserId("456"), new UserId("789")]
        };

        string queryString = request.RequestUri.Query;

        Assert.Contains("user_id=123", queryString);
        Assert.Contains("user_id=456", queryString);
        Assert.Contains("user_id=789", queryString);
    }
}
