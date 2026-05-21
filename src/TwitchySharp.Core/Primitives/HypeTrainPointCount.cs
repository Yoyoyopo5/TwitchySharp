using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// Represents points for a given Hype Train.
/// </summary>
/// <param name="Value">The integer value of the point count.</param>
[Wrapper<int>]
public readonly partial record struct HypeTrainPointCount(int Value);
