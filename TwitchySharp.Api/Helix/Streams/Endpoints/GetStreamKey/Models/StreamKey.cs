using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Streams;

/// <summary>
/// An id representing a specific Twitch stream key.
/// </summary>
/// <param name="Value">The string value of the id.</param>
[Wrapper<string>]
public readonly partial record struct StreamKey(string Value);