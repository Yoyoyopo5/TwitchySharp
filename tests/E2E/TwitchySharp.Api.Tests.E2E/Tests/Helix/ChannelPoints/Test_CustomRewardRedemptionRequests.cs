using TwitchySharp.Api.Helix.ChannelPoints;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.ChannelPoints;

public class Test_CustomRewardRedemptionRequests(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("custom-reward-redemptions");

    [Fact]
    public async Task Send_CustomRewardRedemptionRequests_ReturnSuccessResponses()
    {
        // Note that redemptions can only be updated from rewards created using the same client id.
        const string TEST_REWARD_NAME = "Test Reward";

        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        ITwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        GetCustomRewardRequest getRewardRequest = new()
        {
            BroadcasterId = userConfig.UserId,
            OnlyManageableRewards = true
        };

        TwitchResponse<GetCustomRewardResponse> getRewardResponse = await client.SendAsync(getRewardRequest, ct);
        CustomChannelPointsReward? reward = getRewardResponse.Content.Data.FirstOrDefault(r => r.Title == TEST_REWARD_NAME);

        // We can create a custom reward if the test award does not exist yet,
        // but the redemption must be created manually or via playwright.
        if (reward is null)
        {
            CreateCustomRewardsRequest createRewardRequest = new()
            {
                BroadcasterId = userConfig.UserId,
                Reward = new()
                {
                    Title = TEST_REWARD_NAME,
                    Cost = 1,
                    ShouldRedemptionsSkipRequestQueue = false
                }
            };
            TwitchResponse<CreateCustomRewardsResponse> createRewardResponse = await client.SendAsync(createRewardRequest, ct);
            reward = createRewardResponse.Content.Data.First();
        }

        GetCustomRewardRedemptionRequestByStatus getRedemptionsRequest = new()
        {
            BroadcasterId = userConfig.UserId,
            RewardId = reward.Id,
            Status = RewardRedemptionStatus.Unfulfilled
        };

        TwitchResponse<GetCustomRewardRedemptionResponse> getRedemptionsResponse = await client.SendAsync(getRedemptionsRequest, ct);
        CustomRewardRedemption[] redemptions = getRedemptionsResponse.Content.Data;
        Assert.SkipWhen(redemptions.Length == 0, $"No redemptions exist on the test reward \"{TEST_REWARD_NAME}\".");

        UpdateRedemptionStatusRequest updateRedemptionRequest = new()
        {
            BroadcasterId = userConfig.UserId,
            RewardId = reward.Id,
            Ids = [redemptions.First().Id],
            Status = RewardRedemptionStatus.Fulfilled
        };

        await client.SendAsync(updateRedemptionRequest, ct);
    }
}
