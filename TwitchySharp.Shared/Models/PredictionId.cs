using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch chat prediction.
/// </summary>
/// <param name="Value">The string value of the id</param>
[JsonConverter(typeof(WrapperJsonConverter<PredictionId, string>))]
public readonly record struct PredictionId(string Value) : IWrapValue<string>
{
    public static implicit operator string(PredictionId id)
        => id.Value;
    public override string ToString()
        => Value;
}