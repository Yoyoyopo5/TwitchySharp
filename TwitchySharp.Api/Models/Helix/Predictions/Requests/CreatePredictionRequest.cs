using System;
using System.Net.Http;
using System.Text.Json.Serialization;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models.Helix.Predictions.Responses;
using TwitchySharp.Helpers.JsonConverters;

namespace TwitchySharp.Api.Models.Helix.Predictions.Requests;
/// <summary>
/// Creates a Channel Points Prediction.
/// </summary>
/// <remarks>
/// The prediction runs as soon as it’s created. 
/// The broadcaster may run only one prediction at a time.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ChannelManagePredictions"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#create-prediction">Create Prediction</see> for more information.
/// </remarks>
public record CreatePredictionRequest
    : TwitchHelixRequest<CreatePredictionResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManagePredictions"/>.</param>
    /// <param name="prediction">The new prediction to create and start.</param>
    public CreatePredictionRequest(
        string clientId,
        string accessToken,
        CreatePredictionRequestData prediction
        ) : base(
            "/predictions",
            clientId,
            accessToken
            )
    {
        Method = HttpMethod.Post;
        ContentObject = prediction;
    }
}

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
    /// The length of time that the prediction will be active for.
    /// The minimum is 30 seconds and the maximum is 1800 seconds (30 minutes).
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    public required TimeSpan PredictionWindow { get; set; }
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
