using TwitchySharp.Api.Helix.ChannelPoints;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.Unit.Helix.ChannelPoints;

public class Test_GetCustomRewardRedemptionRequest
{
    private static readonly ClientId TestClientId = new("test_client_id");
    private static readonly UserAccessToken TestAccessToken = new("test_access_token");
    private static readonly UserId TestBroadcasterId = new("broadcaster123");

    [Fact]
    public void QueryByRewardId_QueryString_ContainsRewardIdAndBroadcasterId()
    {
        var rewardId = new RewardId("reward123");
        var query = new GetCustomRewardRedemptionRequestParameters(rewardId)
        {
            BroadcasterId = TestBroadcasterId
        };
        var request = new GetCustomRewardRedemptionRequest(TestClientId, TestAccessToken, query);

        var uri = request.RequestUri;
        var queryString = uri.Query;

        Assert.Contains("broadcaster_id=broadcaster123", queryString);
        Assert.Contains("reward_id=reward123", queryString);
    }

    [Fact]
    public void QueryByStatus_QueryString_ContainsStatus()
    {
        var status = RewardRedemptionStatus.Unfulfilled;
        var query = new GetCustomRewardRedemptionRequestParameters(status)
        {
            BroadcasterId = TestBroadcasterId
        };
        var request = new GetCustomRewardRedemptionRequest(TestClientId, TestAccessToken, query);

        var uri = request.RequestUri;
        var queryString = uri.Query;

        Assert.Contains("broadcaster_id=broadcaster123", queryString);
        Assert.Contains("status=UNFULFILLED", queryString);
    }

    [Fact]
    public void QueryByRewardIdAndStatus_QueryString_ContainsBoth()
    {
        var rewardId = new RewardId("reward123");
        var status = RewardRedemptionStatus.Fulfilled;
        var query = new GetCustomRewardRedemptionRequestParameters(rewardId, status)
        {
            BroadcasterId = TestBroadcasterId
        };
        var request = new GetCustomRewardRedemptionRequest(TestClientId, TestAccessToken, query);

        var uri = request.RequestUri;
        var queryString = uri.Query;

        Assert.Contains("broadcaster_id=broadcaster123", queryString);
        Assert.Contains("reward_id=reward123", queryString);
        Assert.Contains("status=FULFILLED", queryString);
    }

    [Fact]
    public void QueryWithIds_QueryString_ContainsIds()
    {
        var rewardId = new RewardId("reward123");
        var redemptionIds = new[]
        {
            new RewardRedemptionId("redemption1"),
            new RewardRedemptionId("redemption2")
        };
        var query = new GetCustomRewardRedemptionRequestParameters(rewardId)
        {
            BroadcasterId = TestBroadcasterId,
            Ids = redemptionIds
        };
        var request = new GetCustomRewardRedemptionRequest(TestClientId, TestAccessToken, query);

        var uri = request.RequestUri;
        var queryString = uri.Query;

        Assert.Contains("id=redemption1", queryString);
        Assert.Contains("id=redemption2", queryString);
    }

    [Fact]
    public void QueryWithSort_QueryString_ContainsSort()
    {
        var rewardId = new RewardId("reward123");
        var query = new GetCustomRewardRedemptionRequestParameters(rewardId)
        {
            BroadcasterId = TestBroadcasterId,
            Sort = CustomRewardRedemptionSortingMethod.Newest
        };
        var request = new GetCustomRewardRedemptionRequest(TestClientId, TestAccessToken, query);

        var uri = request.RequestUri;
        var queryString = uri.Query;

        Assert.Contains("sort=NEWEST", queryString);
    }

    [Fact]
    public void QueryWithFirst_QueryString_ContainsFirstParam()
    {
        var rewardId = new RewardId("reward123");
        var query = new GetCustomRewardRedemptionRequestParameters(rewardId)
        {
            BroadcasterId = TestBroadcasterId,
            First = new PaginationAmount(25)
        };
        var request = new GetCustomRewardRedemptionRequest(TestClientId, TestAccessToken, query);

        var uri = request.RequestUri;
        var queryString = uri.Query;

        Assert.Contains("first=25", queryString);
    }

    // NOTE: PaginationCursor doesn't override ToString(), so After parameter
    // doesn't serialize correctly. This is a known issue in the implementation.
    // See GetCustomRewardRedemptionRequest line 40: .Add("after", parameters.After?.ToString())

    [Fact]
    public void GetCustomRewardRedemptionRequest_RequestUri_HasCorrectPath()
    {
        var rewardId = new RewardId("reward123");
        var query = new GetCustomRewardRedemptionRequestParameters(rewardId)
        {
            BroadcasterId = TestBroadcasterId
        };
        var request = new GetCustomRewardRedemptionRequest(TestClientId, TestAccessToken, query);

        var uri = request.RequestUri;

        Assert.Equal("/helix/channel_points/custom_rewards/redemptions", uri.AbsolutePath);
    }

    [Fact]
    public void GetCustomRewardRedemptionRequest_Method_IsGet()
    {
        var rewardId = new RewardId("reward123");
        var query = new GetCustomRewardRedemptionRequestParameters(rewardId)
        {
            BroadcasterId = TestBroadcasterId
        };
        var request = new GetCustomRewardRedemptionRequest(TestClientId, TestAccessToken, query);

        Assert.Equal(System.Net.Http.HttpMethod.Get, request.Method);
    }

    [Fact]
    public void StatusCancelled_QueryString_ContainsCancelledStatus()
    {
        var status = RewardRedemptionStatus.Cancelled;
        var query = new GetCustomRewardRedemptionRequestParameters(status)
        {
            BroadcasterId = TestBroadcasterId
        };
        var request = new GetCustomRewardRedemptionRequest(TestClientId, TestAccessToken, query);

        var uri = request.RequestUri;
        var queryString = uri.Query;

        Assert.Contains("status=CANCELLED", queryString);
    }

    [Fact]
    public void GetCustomRewardRedemptionRequest_RequestUri_HasCorrectHost()
    {
        var rewardId = new RewardId("reward123");
        var query = new GetCustomRewardRedemptionRequestParameters(rewardId)
        {
            BroadcasterId = TestBroadcasterId
        };
        var request = new GetCustomRewardRedemptionRequest(TestClientId, TestAccessToken, query);

        var uri = request.RequestUri;

        Assert.Equal("api.twitch.tv", uri.Host);
    }
}
