using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Predictions;

/// <summary>
/// Contains static definitions for possible statuses for API updated predictions.
/// </summary>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<UpdateChatPredictionStatus, string>))]
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
