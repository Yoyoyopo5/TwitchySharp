using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api.Helix.Streams;

/// <summary>
/// Contains static definitions for stream types.
/// </summary>
/// <remarks>
/// Dev note: not sure what the difference is between <see cref="All"/> and <see cref="Live"/> at this point.
/// </remarks>
/// <param name="Value">
/// The custom value for a stream type.
/// Don't use this unless you know what you're doing. 
/// Prefer using the static definitions on this class instead.
/// </param>
[Wrapper<string>]
public readonly partial record struct StreamType(string Value)
{
    public static StreamType All { get; } = new("all");
    public static StreamType Live { get; } = new("live");
}
