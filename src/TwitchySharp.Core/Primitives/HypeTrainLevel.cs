using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// The level of a Hype Train.
/// </summary>
/// <param name="Value">The integer value of the level.</param>
[Wrapper<int>]
public readonly partial record struct HypeTrainLevel(int Value);
