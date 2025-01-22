using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.ChannelPoints;

namespace TwitchySharp.Api.Tests.Integration.Helix.ChannelPoints;
[Collection("helix")]
public class Test_CustomRewardRedemptionRequests(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_CustomRewardRedemptionRequests_ReturnSuccessResponses()
    {
        // Note that redemptions can only be updated from rewards created using the same client id.
        const string TEST_REWARD_TITLE = "Test Reward PLS Redeem";

        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        string rewardId = await FindOrCreateTestReward(TEST_REWARD_TITLE, broadcasterId);

        CustomRewardRedemption[] redemptions = await GetCustomRewardRedemptions(rewardId, broadcasterId);

        string testRedemptionId = redemptions.First().Id;

        await _fixture.Api.SendRequestAsync(
            new UpdateRedemptionStatusRequest(
                _fixture.Secrets.ClientId,
                _fixture.Secrets.UserAccessToken,
                broadcasterId,
                rewardId,
                [testRedemptionId],
                UpdatedRewardRedemptionStatus.Fulfilled
                )
            );
    }

    private async ValueTask<string> FindOrCreateTestReward(string rewardTitle, string broadcasterId)
        => await GetCustomRewardId(rewardTitle, broadcasterId) ?? await CreateTestReward(rewardTitle, broadcasterId);

    private async ValueTask<string?> GetCustomRewardId(string rewardTitle, string broadcasterId)
        => (await _fixture.Api.SendRequestAsync(
                new GetCustomRewardRequest(
                    _fixture.Secrets.ClientId,
                    _fixture.Secrets.UserAccessToken,
                    broadcasterId
                    )
                )
           ).Data.Where(reward => reward.Title == rewardTitle).FirstOrDefault()?.Id;

    private async ValueTask<CustomRewardRedemption[]> GetCustomRewardRedemptions(string rewardId, string broadcasterId)
        => (await _fixture.Api.SendRequestAsync(
                new GetCustomRewardRedemptionRequest(
                    _fixture.Secrets.ClientId,
                    _fixture.Secrets.UserAccessToken,
                    broadcasterId,
                    rewardId,
                    RewardRedemptionStatus.Unfulfilled
                    )
            )).Data;

    private async ValueTask<string> CreateTestReward(string rewardTitle, string broadcasterId)
        => (await _fixture.Api.SendRequestAsync(
                new CreateCustomRewardsRequest(
                    _fixture.Secrets.ClientId,
                    _fixture.Secrets.UserAccessToken,
                    broadcasterId,
                    new CreateCustomRewardsRequestData()
                    {
                        Title = rewardTitle,
                        Cost = 1
                    }
                    )
                )).Data.First().Id;

}
