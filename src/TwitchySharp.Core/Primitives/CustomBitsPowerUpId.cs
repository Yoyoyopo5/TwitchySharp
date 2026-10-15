using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// An id of a custom bits power up reward.
/// </summary>
/// <param name="Value">The value of the id.</param>
[Wrapper<string>]
public readonly partial record struct CustomBitsPowerUpId(string Value);
