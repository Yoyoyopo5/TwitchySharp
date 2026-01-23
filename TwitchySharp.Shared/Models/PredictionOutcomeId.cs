using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch chat prediction.
/// </summary>
/// <param name="Value">The string value of the id</param>
[JsonConverter(typeof(WrapperJsonConverter<PredictionOutcomeId, string>))]
public readonly record struct PredictionOutcomeId(string Value) : IWrapValue<string>
{
    public static implicit operator string(PredictionOutcomeId id)
        => id.Value;
    public override string ToString()
        => Value;
}