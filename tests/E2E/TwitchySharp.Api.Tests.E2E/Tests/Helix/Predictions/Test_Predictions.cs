using TwitchySharp.Api.Helix.Predictions;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Predictions;

public class Test_Predictions(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("predictions");

    [Fact]
    public async Task Send_PredictionsRequests_ReturnSuccessResponses()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        UserId broadcasterId = userConfig.UserId;
        TestingTwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        TwitchResponse<CreatePredictionResponseContent> createRespone = await CreatePrediction(client, broadcasterId, ct);
        ChatPrediction prediction = createRespone.Content.Data.Single();
        await Task.Delay(250, ct);

        TwitchResponse<GetPredictionsResponseContent> getResponse = await GetPredictions(client, broadcasterId, prediction.Id, ct);
        ChatPredictionOutcome outcome = getResponse.Content.Data.Single().Outcomes.First();

        await EndPrediction(client, broadcasterId, prediction.Id, outcome.Id, ct);
    }

    private static Task<TwitchResponse<CreatePredictionResponseContent>> CreatePrediction(TestingTwitchClient client, UserId broadcasterId, CancellationToken ct)
        => client.SendAsync(new CreatePredictionRequest()
        {
            Prediction = new CreatePredictionRequestData()
            {
                BroadcasterId = broadcasterId,
                Title = "Test Prediction",
                PredictionWindow = TimeSpan.FromMinutes(2),
                Outcomes =
                [
                    new()
                    {
                        Title = "Test Outcome 1"
                    },
                    new()
                    {
                        Title = "Test Outcome 2"
                    }
                ]
            }
        }, TestName, ct);

    private static Task<TwitchResponse<GetPredictionsResponseContent>> GetPredictions(TestingTwitchClient client, UserId broadcasterId, PredictionId predictionId, CancellationToken ct)
        => client.SendAsync(new GetPredictionsRequest()
        {
            BroadcasterId = broadcasterId,
            PredictionIds = [predictionId]
        }, TestName, ct);

    private static Task<TwitchResponse<EndPredictionResponseContent>> EndPrediction(TestingTwitchClient client, UserId broadcasterId, PredictionId predictionId, PredictionOutcomeId winningOutcomeId, CancellationToken ct)
        => client.SendAsync(new EndPredictionRequest()
        {
            Prediction = new ResolvePrediction(winningOutcomeId)
            {
                BroadcasterId = broadcasterId,
                Id = predictionId,
            }
        }, TestName, ct);
}
