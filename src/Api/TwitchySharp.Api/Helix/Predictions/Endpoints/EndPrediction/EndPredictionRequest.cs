using System.Collections.Immutable;
using System.Net.Http;

namespace TwitchySharp.Api.Helix.Predictions;
/// <summary>
/// Locks, resolves, or cancels a Channel Points Prediction.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelManagePredictions"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#end-prediction">End Prediction</see> for more information.
/// </remarks>
public record EndPredictionRequest
    : TwitchHelixRequest<EndPredictionResponse>
{
    protected override string Path => "/predictions";
    public override HttpMethod Method => HttpMethod.Patch;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.User(Prediction.BroadcasterId),
        ValidScopes = ImmutableHashSet.Create(Scope.ChannelManagePredictions)
    };
    public override object? ContentObject => Prediction;

    /// <summary>
    /// Data used to update the prediction.
    /// Use derived classes <see cref="ResolvePrediction"/>, <see cref="CancelPrediction"/>, and <see cref="LockPrediction"/>.
    /// </summary>
    public required EndPredictionRequestData Prediction { get; init; }
}

/// <summary>
/// Resolves a specific prediction.
/// </summary>
public record ResolvePrediction
    : EndPredictionRequestData
{
    /// <inheritdoc cref="ResolvePrediction"/>
    /// <param name="winningOutcomeId">The id of the winning outcome to set.</param>
    public ResolvePrediction(PredictionOutcomeId winningOutcomeId)
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
/// <remarks>
/// Use the <see cref="CancelPrediction"/>, <see cref="LockPrediction"/>, and <see cref="ResolvePrediction"/> derived types.
/// </remarks>
public record EndPredictionRequestData
{
    /// <summary>
    /// The user id of the broadcaster (channel) that owns the prediction.
    /// This must be the same user that created the user access token in the <see cref="EndPredictionRequest"/>.
    /// </summary>
    public required UserId BroadcasterId { get; init; }
    /// <summary>
    /// The id of the prediction to update.
    /// </summary>
    public required PredictionId Id { get; init; }
    /// <summary>
    /// The status to set the prediction to.
    /// Only currently running predictions can be updated, and <see cref="ChatPredictionStatus.Locked"/> predictions can only be set to <see cref="UpdateChatPredictionStatus.Resolved"/> or <see cref="UpdateChatPredictionStatus.Cancelled"/> (a locked prediction cannot be unlocked).
    /// If setting a prediction to <see cref="UpdateChatPredictionStatus.Locked"/>, the broadcaster has 24 hours to cancel or resolve the prediction before it will be automatically cancelled.
    /// </summary>
    public UpdateChatPredictionStatus Status { get; protected init; }
    /// <summary>
    /// The id of the winning outcome.
    /// This must be set if <see cref="Status"/> is set to <see cref="UpdateChatPredictionStatus.Resolved"/>.
    /// </summary>
    public PredictionOutcomeId? WinningOutcomeId { get; protected init; }
    /// <summary>
    /// <inheritdoc cref="EndPredictionRequestData"/>
    /// Use this constructor to use a custom update status (e.g., if a new status is added to Twitch API and isn't available on the <see cref="UpdateChatPredictionStatus"/> class).
    /// </summary>
    /// <param name="status">The status to set the prediction to.</param>

    protected EndPredictionRequestData(UpdateChatPredictionStatus status)
        => Status = status;
}
