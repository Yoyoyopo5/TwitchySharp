using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Predictions;
/// <summary>
/// Locks, resolves, or cancels a Channel Points Prediction.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#end-prediction">end prediction</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelManagePredictions"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManagePredictions"/>.</param>
/// <param name="prediction">
/// Data used to update the prediction.
/// Use derived classes <see cref="ResolvePrediction"/>, <see cref="CancelPrediction"/>, and <see cref="LockPrediction"/>.
/// </param>
public class EndPredictionRequest(
    string clientId,
    string accessToken,
    EndPredictionRequestData prediction
    )
    : HelixApiRequest<EndPredictionResponse, EndPredictionRequestData>(
        "/predictions",
        clientId,
        accessToken,
        prediction
        )
{
    public override HttpMethod Method => HttpMethod.Patch;
}

/// <summary>
/// Resolves a specific prediction.
/// </summary>
public record ResolvePrediction
    : EndPredictionRequestData
{
    /// <inheritdoc cref="ResolvePrediction"/>
    /// <param name="winningOutcomeId">The id of the winning outcome to set.</param>
    public ResolvePrediction(string winningOutcomeId)
        : base(UpdateChatPredictionStatus.Resolved)
        => WinningOutcomeId = winningOutcomeId;
}
/// <summary>
/// Cancels a specific prediction.
/// </summary>
public record CancelPrediction() : EndPredictionRequestData(UpdateChatPredictionStatus.Cancelled);
/// <summary>
/// Locks a specific prediction.
/// </summary>
public record LockPrediction() : EndPredictionRequestData(UpdateChatPredictionStatus.Locked);

/// <summary>
/// Data used to update a prediction.
/// </summary>
public record EndPredictionRequestData
{
    /// <summary>
    /// The user id of the broadcaster (channel) that owns the prediction.
    /// This must be the same user that created the user access token in the <see cref="EndPredictionRequest"/>.
    /// </summary>
    public required string BroadcasterId { get; set; }
    /// <summary>
    /// The id of the prediction to update.
    /// </summary>
    public required string Id { get; set; }
    /// <summary>
    /// The status to set the prediction to.
    /// Only currently running predictions can be updated, and <see cref="ChatPredictionStatus.Locked"/> predictions can only be set to <see cref="UpdateChatPredictionStatus.Resolved"/> or <see cref="UpdateChatPredictionStatus.Cancelled"/> (a locked prediction cannot be unlocked).
    /// If setting a prediction to <see cref="UpdateChatPredictionStatus.Locked"/>, the broadcaster has 24 hours to cancel or resolve the prediction before it will be automatically cancelled.
    /// </summary>
    [JsonConverter(typeof(ValueBackedEnumJsonConverter<UpdateChatPredictionStatus, string>))]
    public UpdateChatPredictionStatus Status { get; protected set; }
    /// <summary>
    /// The id of the winning outcome.
    /// This must be set if <see cref="Status"/> is set to <see cref="UpdateChatPredictionStatus.Resolved"/>.
    /// </summary>
    public string? WinningOutcomeId { get; protected set; }
    /// <summary>
    /// <inheritdoc cref="EndPredictionRequestData"/>
    /// Use this constructor to use a custom update status (e.g., if a new status is added to Twitch API and isn't available on the <see cref="UpdateChatPredictionStatus"/> class).
    /// </summary>
    /// <param name="status">The status to set the prediction to.</param>

    protected EndPredictionRequestData(UpdateChatPredictionStatus status)
        => Status = status;
}

/// <summary>
/// Contains static definitions for possible statuses for API updated predictions.
/// </summary>
public record UpdateChatPredictionStatus(string Value)
    : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// The winning outcome is determined and the Channel Points are distributed to the viewers who predicted the correct outcome.
    /// </summary>
    public static UpdateChatPredictionStatus Resolved { get; } = new("RESOLVED");
    /// <summary>
    /// The broadcaster is canceling the prediction and sending refunds to the participants.
    /// </summary>
    public static UpdateChatPredictionStatus Cancelled { get; } = new("CANCELLED");
    /// <summary>
    /// The broadcaster is locking the prediction, which means viewers may no longer make predictions.
    /// </summary>
    public static UpdateChatPredictionStatus Locked { get; } = new("LOCKED");
}
