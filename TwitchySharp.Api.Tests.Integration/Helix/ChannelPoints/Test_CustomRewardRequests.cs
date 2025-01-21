using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.ChannelPoints;

namespace TwitchySharp.Api.Tests.Integration.Helix.ChannelPoints;
[Collection("helix")]
public class Test_CustomRewardRequests(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;
    private readonly Task<string> _broadcasterId = fixture.GetUserIdFromAccessTokenAsync();

    [Fact]
    public async void Send_CreateGetUpdateDeleteCustomRewardRequest_ReturnSuccessResponses()
    {
        CreateCustomRewardsResponse createdReward = await CreateRewardAsync();
        string rewardId = createdReward.Data.First().Id;
        await GetRewardAsync(rewardId);
        await UpdateRewardAsync(rewardId);
        await DeleteRewardAsync(rewardId);
    }

    private async ValueTask<CreateCustomRewardsResponse> CreateRewardAsync()
        => await _fixture.Api.SendRequestAsync(
            new CreateCustomRewardsRequest(
                _fixture.Secrets.ClientId,
                _fixture.Secrets.UserAccessToken,
                await _broadcasterId,
                new CreateCustomRewardsRequestData()
                {
                    Title = "test_reward_pls_delete",
                    Cost = 1
                }
                )
            );

    private async ValueTask<GetCustomRewardResponse> GetRewardAsync(string rewardId)
        => await _fixture.Api.SendRequestAsync(
            new GetCustomRewardRequest(
                _fixture.Secrets.ClientId,
                _fixture.Secrets.UserAccessToken,
                await _broadcasterId,
                [rewardId]
                ));

    private async ValueTask<UpdateCustomRewardResponse> UpdateRewardAsync(string rewardId)
        => await _fixture.Api.SendRequestAsync(
            new UpdateCustomRewardRequest(
                _fixture.Secrets.ClientId,
                _fixture.Secrets.UserAccessToken,
                await _broadcasterId,
                rewardId,
                new UpdateCustomRewardRequestData()
                {
                    Cost = 2
                }
                )
            );

    private async ValueTask<DeleteCustomRewardResponse> DeleteRewardAsync(string rewardId)
        => await _fixture.Api.SendRequestAsync(
            new DeleteCustomRewardRequest(
                _fixture.Secrets.ClientId,
                _fixture.Secrets.UserAccessToken,
                await _broadcasterId,
                rewardId
            ));
}
