using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api.Helix.Predictions;
/// <summary>
/// Contains static definitions for possible chat prediction outcome colors.
/// </summary>
/// <param name="Value">The string value of the chat prediction outcome color.</param>
[Wrapper<string>]
public readonly partial record struct ChatPredictionOutcomeColor(string Value)
{
    public static ChatPredictionOutcomeColor Blue { get; } = new("BLUE");
    public static ChatPredictionOutcomeColor Pink { get; } = new("PINK");
}
