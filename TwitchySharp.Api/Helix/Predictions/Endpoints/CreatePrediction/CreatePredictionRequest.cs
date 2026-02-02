using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net.Http;
using System.Text.Json.Serialization;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers.JsonConverters;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Predictions;
/// <summary>
/// Creates a Channel Points Prediction.
/// </summary>
/// <remarks>
/// The prediction runs as soon as it's created.
/// The broadcaster may run only one prediction at a time.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ChannelManagePredictions"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#create-prediction">Create Prediction</see> for more information.
/// </remarks>
public record CreatePredictionRequest
    : TwitchHelixRequest<CreatePredictionResponse>
{
    protected override string Path => "/predictions";
    public override HttpMethod Method => HttpMethod.Post;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(Prediction.BroadcasterId);
    public override IReadOnlySet<Scope> ValidScopes => ImmutableHashSet.Create(Scope.ChannelManagePredictions);
    public override object? ContentObject => Prediction;

    /// <summary>
    /// The new prediction to create and start.
    /// </summary>
    public required CreatePredictionRequestData Prediction { get; init; }
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
    public required UserId BroadcasterId { get; init; }
    /// <summary>
    /// The question that the prediction is asking.
    /// This is limited to a maximum of 45 characters.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// The list of possible outcomes that the viewers may choose from.
    /// This list must contain a minimum of 2 choices and up to a maximum of 10 choices.
    /// </summary>
    public required CreatePredictionOutcome[] Outcomes { get; init; }
    /// <summary>
    /// The length of time that the prediction will be active for.
    /// The minimum is 30 seconds and the maximum is 1800 seconds (30 minutes).
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    public required TimeSpan PredictionWindow { get; init; }
}