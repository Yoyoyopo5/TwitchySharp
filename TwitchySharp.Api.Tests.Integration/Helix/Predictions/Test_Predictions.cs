using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Predictions;

namespace TwitchySharp.Api.Tests.Integration.Helix.Predictions;
[Collection("helix")]
public class Test_Predictions(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_PredictionsRequests_ReturnSuccessResponses()
    {
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        string predictionId = (await CreatePrediction(broadcasterId)).Data.First().Id;
        string outcomeId = (await GetPredictions(broadcasterId, predictionId)).Data.First().Outcomes.First().Id;
        await EndPrediction(broadcasterId, predictionId, outcomeId);
    }

    private ValueTask<CreatePredictionResponse> CreatePrediction(string broadcasterId)
        => _fixture.Api.SendRequestAsync(new CreatePredictionRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            new CreatePredictionRequestData()
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
            ));

    private ValueTask<GetPredictionsResponse> GetPredictions(string broadcasterId, string predictionId)
        => _fixture.Api.SendRequestAsync(new GetPredictionsRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            [ predictionId ]
            ));

    private ValueTask<EndPredictionResponse> EndPrediction(string broadcasterId, string predictionId, string winningOutcomeId)
        => _fixture.Api.SendRequestAsync(new EndPredictionRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            new ResolvePrediction(winningOutcomeId)
            {
                BroadcasterId = broadcasterId,
                Id = predictionId
            }
            ));
}
