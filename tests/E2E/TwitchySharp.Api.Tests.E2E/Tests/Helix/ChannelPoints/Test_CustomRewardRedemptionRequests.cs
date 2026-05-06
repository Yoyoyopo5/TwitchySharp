using TwitchySharp.Api.Helix.ChannelPoints;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.ChannelPoints;

[Collection("twitch")]
public class Test_CustomRewardRedemptionRequests(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_CustomRewardRedemptionRequests_ReturnSuccessResponses()
    {
        // Note that redemptions can only be updated from rewards created using the same client id.
        const string TEST_REWARD_NAME = "Test Reward";
        ITwitchClient client = _fixture.CreateClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        GetCustomRewardRequest getRewardRequest = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId,
            OnlyManageableRewards = true
        };

        var getRewardResponse = await client.SendAsync(getRewardRequest, ct);
        CustomChannelPointsReward? reward = getRewardResponse.Content.Data.FirstOrDefault(r => r.Title == TEST_REWARD_NAME);

        // We can create a custom reward if the test award does not exist yet,
        // but the redemption must be created manually or via playwright.
        if (reward is null)
        {
            CreateCustomRewardsRequest createRewardRequest = new()
            {
                BroadcasterId = _fixture.UserIdentity.UserId,
                Reward = new()
                {
                    Title = TEST_REWARD_NAME,
                    Cost = 1,
                    ShouldRedemptionsSkipRequestQueue = false
                }
            };
            var createRewardResponse = await client.SendAsync(createRewardRequest, ct);
            reward = createRewardResponse.Content.Data.First();
        }

        GetCustomRewardRedemptionRequest getRedemptionsRequest = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId,
            RewardId = reward.Id
        };

        var getRedemptionsResponse = await client.SendAsync(getRedemptionsRequest, ct);
        CustomRewardRedemption[] redemptions = getRedemptionsResponse.Content.Data;
        if (redemptions.Length == 0)
            return;

        UpdateRedemptionStatusRequest updateRedemptionRequest = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId,
            RewardId = reward.Id,
            Ids = [redemptions.First().Id],
            Status = RewardRedemptionStatus.Fulfilled
        };

        await client.SendAsync(updateRedemptionRequest, ct);
    }
}
