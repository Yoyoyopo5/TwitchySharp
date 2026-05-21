using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// A charity name.
/// </summary>
/// <param name="Value">The string value of the charity name.</param>
[Wrapper<string>]
public readonly partial record struct CharityName(string Value);
