using TwitchySharp.Api.Helix.ChannelPoints;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.Unit.Helix.ChannelPoints;

public class Test_GetCustomRewardRedemptionRequest
{
    private static readonly UserId TestBroadcasterId = new("broadcaster123");

    [Fact]
    public void QueryByRewardId_QueryString_ContainsRewardIdAndBroadcasterId()
    {
        var rewardId = new RewardId("reward123");
        var request = new GetCustomRewardRedemptionRequest
        {
            BroadcasterId = TestBroadcasterId,
            RewardId = rewardId
        };

        var uri = request.RequestUri;
        var queryString = uri.Query;

        Assert.Contains("broadcaster_id=broadcaster123", queryString);
        Assert.Contains("reward_id=reward123", queryString);
    }

    [Fact]
    public void QueryByStatus_QueryString_ContainsStatus()
    {
        var status = RewardRedemptionStatus.Unfulfilled;
        var request = new GetCustomRewardRedemptionRequest
        {
            BroadcasterId = TestBroadcasterId,
            Status = status
        };

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
        var request = new GetCustomRewardRedemptionRequest
        {
            BroadcasterId = TestBroadcasterId,
            RewardId = rewardId,
            Status = status
        };

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
        var request = new GetCustomRewardRedemptionRequest
        {
            BroadcasterId = TestBroadcasterId,
            RewardId = rewardId,
            Ids = redemptionIds
        };

        var uri = request.RequestUri;
        var queryString = uri.Query;

        Assert.Contains("id=redemption1", queryString);
        Assert.Contains("id=redemption2", queryString);
    }

    [Fact]
    public void QueryWithSort_QueryString_ContainsSort()
    {
        var rewardId = new RewardId("reward123");
        var request = new GetCustomRewardRedemptionRequest
        {
            BroadcasterId = TestBroadcasterId,
            RewardId = rewardId,
            Sort = CustomRewardRedemptionSortingMethod.Newest
        };

        var uri = request.RequestUri;
        var queryString = uri.Query;

        Assert.Contains("sort=NEWEST", queryString);
    }

    [Fact]
    public void QueryWithFirst_QueryString_ContainsFirstParam()
    {
        var rewardId = new RewardId("reward123");
        var request = new GetCustomRewardRedemptionRequest
        {
            BroadcasterId = TestBroadcasterId,
            RewardId = rewardId,
            First = new PaginationAmount(25)
        };

        var uri = request.RequestUri;
        var queryString = uri.Query;

        Assert.Contains("first=25", queryString);
    }

    [Fact]
    public void GetCustomRewardRedemptionRequest_RequestUri_HasCorrectPath()
    {
        var rewardId = new RewardId("reward123");
        var request = new GetCustomRewardRedemptionRequest
        {
            BroadcasterId = TestBroadcasterId,
            RewardId = rewardId
        };

        var uri = request.RequestUri;

        Assert.Equal("/helix/channel_points/custom_rewards/redemptions", uri.AbsolutePath);
    }

    [Fact]
    public void GetCustomRewardRedemptionRequest_Method_IsGet()
    {
        var rewardId = new RewardId("reward123");
        var request = new GetCustomRewardRedemptionRequest
        {
            BroadcasterId = TestBroadcasterId,
            RewardId = rewardId
        };

        Assert.Equal(System.Net.Http.HttpMethod.Get, request.Method);
    }

    [Fact]
    public void StatusCancelled_QueryString_ContainsCancelledStatus()
    {
        var status = RewardRedemptionStatus.Cancelled;
        var request = new GetCustomRewardRedemptionRequest
        {
            BroadcasterId = TestBroadcasterId,
            Status = status
        };

        var uri = request.RequestUri;
        var queryString = uri.Query;

        Assert.Contains("status=CANCELLED", queryString);
    }

    [Fact]
    public void GetCustomRewardRedemptionRequest_RequestUri_HasCorrectHost()
    {
        var rewardId = new RewardId("reward123");
        var request = new GetCustomRewardRedemptionRequest
        {
            BroadcasterId = TestBroadcasterId,
            RewardId = rewardId
        };

        var uri = request.RequestUri;

        Assert.Equal("api.twitch.tv", uri.Host);
    }
}
