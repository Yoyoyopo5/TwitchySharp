using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// An id representing a specific Twitch conduit transport.
/// </summary>
/// <param name="Value">The string value of the id.</param>
[Wrapper<string>]
public readonly partial record struct ConduitId(string Value);
