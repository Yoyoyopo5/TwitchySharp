using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Streams;

/// <summary>
/// Contains static definitions for possible Twitch stream types.
/// </summary>
/// <param name="Value">The string value of the stream type.</param>
[Wrapper<string>]
public readonly partial record struct TwitchStreamType(string Value)
{
    public static TwitchStreamType Live { get; } = new("live");
}