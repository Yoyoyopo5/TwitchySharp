using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// An emote name.
/// </summary>
/// <remarks>
/// This is the string chatters type to use the emote in chat.
/// </remarks>
/// <param name="Value">The string name of the emote.</param>
[Wrapper<string>]
public readonly partial record struct EmoteName(string Value);
