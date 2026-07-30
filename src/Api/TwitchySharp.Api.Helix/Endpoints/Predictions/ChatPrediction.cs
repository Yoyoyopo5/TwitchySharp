using System.Text.Json.Serialization;
using TwitchySharp.Serialization;

namespace TwitchySharp.Api.Helix.Predictions;

/// <summary>
/// Contains information about a specific prediction.
/// </summary>
public record ChatPrediction
{
    /// <summary>
    /// The id of the prediction.
    /// </summary>
    public required PredictionId Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that the prediction belongs to.
    /// </summary>
    public required UserId BroadcasterId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the prediction belongs to.
    /// </summary>
    public required UserName BroadcasterName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the prediction belongs to.
    /// </summary>
    public required UserLogin BroadcasterLogin { get; init; }
    /// <summary>
    /// The question that the prediction asks.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// The id of the winning outcome.
    /// This property is <see langword="null"/> if <see cref="Status"/> is not <see cref="ChatPredictionStatus.Resolved"/>.
    /// </summary>
    public PredictionOutcomeId? WinningOutcomeId { get; init; }
    /// <summary>
    /// The list of possible outcomes for the prediction.
    /// </summary>
    public required ChatPredictionOutcome[] Outcomes { get; init; }
    /// <summary>
    /// The length of time that the prediction will run for.
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    public required TimeSpan PredictionWindow { get; init; }
    /// <summary>
    /// The prediction’s status.
    /// </summary>
    public required ChatPredictionStatus Status { get; init; }
    /// <summary>
    /// The date and time when the prediction began.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }
    /// <summary>
    /// The date and time when the prediction ended.
    /// This is <see langword="null"/> if <see cref="Status"/> is set to <see cref="ChatPredictionStatus.Active"/>.
    /// </summary>
    public DateTimeOffset? EndedAt { get; init; }
    /// <summary>
    /// The date and time when the prediction was locked.
    /// This is <see langword="null"/> if <see cref="Status"/> is not <see cref="ChatPredictionStatus.Locked"/>.
    /// </summary>
    public DateTimeOffset? LockedAt { get; init; }
}
