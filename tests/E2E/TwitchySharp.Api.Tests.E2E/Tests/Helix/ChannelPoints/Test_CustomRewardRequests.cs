using TwitchySharp.Api.Helix.ChannelPoints;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.ChannelPoints;

[Collection("twitch")]
public class Test_CustomRewardRequests(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_CreateGetUpdateDeleteCustomRewardRequest_ReturnSuccessResponses()
    {
        ITwitchClient client = TwitchClientFixture.Client;
        CancellationToken ct = TestContext.Current.CancellationToken;

        CustomChannelPointsReward createdReward = await CreateReward(client, _fixture.UserIdentity.UserId, ct);
        await Task.Delay(250, TestContext.Current.CancellationToken);

        await UpdateReward(client, createdReward, ct);
        await Task.Delay(250, TestContext.Current.CancellationToken);

        await DeleteReward(client, createdReward, ct);
    }

    private async ValueTask<CustomChannelPointsReward> CreateReward(ITwitchClient client, UserId broadcasterId, CancellationToken ct)
        => (await client.SendAsync(new CreateCustomRewardsRequest
        {
            BroadcasterId = _fixture.UserIdentity.UserId,
            Reward = new()
            {
                Title = "Test Reward PLS Redeem",
                Cost = 1,
                ShouldRedemptionsSkipRequestQueue = true,
                IsMaxPerStreamEnabled = true,
                IsMaxPerUserPerStreamEnabled = true,
                MaxPerStream = 5,
                MaxPerUserPerStream = 1,
                BackgroundColor = new RgbColor(20, 255, 20),
                IsGlobalCooldownEnabled = true,
                GlobalCooldown = TimeSpan.FromSeconds(60),
                IsEnabled = false,
                IsUserInputRequired = true,
                Prompt = "Share your thoughts."
            }
        }, ct)).Content.Data.First();

    private ValueTask<TwitchResponse<UpdateCustomRewardResponse>> UpdateReward(
        ITwitchClient client,
        CustomChannelPointsReward reward,
        CancellationToken ct)
        => client.SendAsync(new UpdateCustomRewardRequest
        {
            BroadcasterId = _fixture.UserIdentity.UserId,
            RewardId = reward.Id,
            UpdatedReward = new()
            {
                BackgroundColor = new RgbColor(255, 20, 20),
                ShouldRedemptionsSkipRequestQueue = false,
                Cost = 1,
                IsGlobalCooldownEnabled = false,
                IsEnabled = true,
                IsMaxPerUserPerStreamEnabled = false,
                IsMaxPerStreamEnabled = false,
                IsUserInputRequired = false
            }
        }, ct);

    private ValueTask<TwitchResponse<DeleteCustomRewardResponse>> DeleteReward(
        ITwitchClient client,
        CustomChannelPointsReward reward,
        CancellationToken ct)
        => client.SendAsync(new DeleteCustomRewardRequest
        {
            BroadcasterId = _fixture.UserIdentity.UserId,
            RewardId = reward.Id
        }, ct);
}
