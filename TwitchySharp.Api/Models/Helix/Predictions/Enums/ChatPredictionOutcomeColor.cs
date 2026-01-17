using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Predictions.Enums;
/// <summary>
/// Contains static definitions for possible chat prediction outcome colors.
/// </summary>
/// <param name="Value">The string value of the chat prediction outcome color.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<ChatPredictionOutcomeColor, string>))]
public record ChatPredictionOutcomeColor(string Value) : ValueBackedEnum<string>(Value)
{
    public static ChatPredictionOutcomeColor Blue { get; } = new("BLUE");
    public static ChatPredictionOutcomeColor Pink { get; } = new("PINK");
}
