using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Predictions;
/// <summary>
/// Creates a Channel Points Prediction.
/// The prediction runs as soon as it’s created. 
/// The broadcaster may run only one prediction at a time.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#create-prediction">create prediction</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelManagePredictions"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManagePredictions"/>.</param>
/// <param name="prediction">The new prediction to create and start.</param>
public class CreatePredictionRequest(
    string clientId,
    string accessToken,
    CreatePredictionRequestData prediction
    )
    : HelixApiRequest<CreatePredictionResponse, CreatePredictionRequestData>(
        "/predictions",
        clientId,
        accessToken,
        prediction
        );

/// <summary>
/// Data used to create a new chat prediction.
/// </summary>
public record CreatePredictionRequestData
{
    /// <summary>
    /// The user id of the broadcaster (channel) to create the prediction for.
    /// This must be the same user that created the user access token in the <see cref="CreatePredictionRequest"/>.
    /// </summary>
    public required string BroadcasterId { get; set; }
    /// <summary>
    /// The question that the prediction is asking.
    /// This is limited to a maximum of 45 characters.
    /// </summary>
    public required string Title { get; set; }
    /// <summary>
    /// The list of possible outcomes that the viewers may choose from.
    /// This list must contain a minimum of 2 choices and up to a maximum of 10 choices.
    /// </summary>
    public required CreatePredictionOutcome[] Outcomes { get; set; }
    /// <summary>
    /// The length of time in <b>seconds</b> that the prediction will be active for.
    /// The minimum is 30 seconds and the maximum is 1800 seconds (30 minutes).
    /// </summary>
    public required int PredictionWindow { get; set; }
}

/// <summary>
/// Data used to create an individual outcome for a new prediction.
/// </summary>
public record CreatePredictionOutcome
{
    /// <summary>
    /// The text of one of the outcomes that the viewer may select. 
    /// The title is limited to a maximum of 25 characters.
    /// </summary>
    public required string Title { get; set; }
}
