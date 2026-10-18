using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// A URL for a chat GIF.
/// </summary>
/// <param name="Value">The string value of the url.</param>
[Wrapper<string>]
public readonly partial record struct GifUrl(string Value);
