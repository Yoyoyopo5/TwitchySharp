using TwitchySharp.Api.Helix.ChannelPoints;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.ChannelPoints;

public class Test_CustomRewardRequests(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("custom-rewards");

    [Fact]
    public async Task Send_CreateUpdateDeleteCustomRewardRequest_ReturnSuccessResponses()
    {
        TestingTwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        CustomChannelPointsReward createdReward = await CreateReward(client, userConfig.UserId, ct);
        try
        {
            await Task.Delay(250, TestContext.Current.CancellationToken);

            await UpdateReward(client, userConfig.UserId, createdReward, ct);
            await Task.Delay(250, TestContext.Current.CancellationToken);
        }
        finally
        {
            await DeleteReward(client, userConfig.UserId, createdReward, ct);
        }
    }

    private static async Task<CustomChannelPointsReward> CreateReward(TestingTwitchClient client, UserId broadcasterId, CancellationToken ct)
        => (await client.SendAsync(new CreateCustomRewardsRequest
        {
            BroadcasterId = broadcasterId,
            Reward = new()
            {
                Title = Guid.NewGuid().ToString()[..6],
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
        }, TestName, ct)).Content.Data.First();

    private static Task<TwitchResponse<UpdateCustomRewardResponseContent>> UpdateReward(
        TestingTwitchClient client,
        UserId broadcasterId,
        CustomChannelPointsReward reward,
        CancellationToken ct)
        => client.SendAsync(new UpdateCustomRewardRequest
        {
            BroadcasterId = broadcasterId,
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
        }, TestName, ct);

    private static Task<TwitchResponse<DeleteCustomRewardResponseContent>> DeleteReward(
        TestingTwitchClient client,
        UserId broadcasterId,
        CustomChannelPointsReward reward,
        CancellationToken ct)
        => client.SendAsync(new DeleteCustomRewardRequest
        {
            BroadcasterId = broadcasterId,
            RewardId = reward.Id
        }, TestName, ct);
}
