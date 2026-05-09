using TwitchySharp.Api.Helix.Predictions;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Predictions;

[Collection("twitch")]
public class Test_Predictions(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_PredictionsRequests_ReturnSuccessResponses()
    {
        UserId broadcasterId = _fixture.UserIdentity.UserId;
        ITwitchClient client = TwitchClientFixture.Client;
        CancellationToken ct = TestContext.Current.CancellationToken;

        var createRespone = await CreatePrediction(client, broadcasterId, ct);
        ChatPrediction prediction = createRespone.Content.Data.Single();
        await Task.Delay(250, ct);

        var getResponse = await GetPredictions(client, broadcasterId, prediction.Id, ct);
        ChatPredictionOutcome outcome = getResponse.Content.Data.Single().Outcomes.First();

        await EndPrediction(client, broadcasterId, prediction.Id, outcome.Id, ct);
    }

    private static ValueTask<TwitchResponse<CreatePredictionResponse>> CreatePrediction(ITwitchClient client, UserId broadcasterId, CancellationToken ct)
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
        }, ct);

    private static ValueTask<TwitchResponse<GetPredictionsResponse>> GetPredictions(ITwitchClient client, UserId broadcasterId, PredictionId predictionId, CancellationToken ct)
        => client.SendAsync(new GetPredictionsRequest()
        {
            BroadcasterId = broadcasterId,
            PredictionIds = [predictionId]
        }, ct);

    private static ValueTask<TwitchResponse<EndPredictionResponse>> EndPrediction(ITwitchClient client, UserId broadcasterId, PredictionId predictionId, PredictionOutcomeId winningOutcomeId, CancellationToken ct)
        => client.SendAsync(new EndPredictionRequest()
        {
            Prediction = new ResolvePrediction(winningOutcomeId)
            {
                BroadcasterId = broadcasterId,
                Id = predictionId,
            }
        }, ct);
}
