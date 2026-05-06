using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api.Helix.Videos;

/// <summary>
/// Contains static definitions for possible twitch video types.
/// </summary>
/// <param name="Value">The string value of the twitch video type.</param>
[Wrapper<string>]
public readonly partial record struct TwitchVideoType(string Value)
{
    /// <summary>
    /// An on-demand video (VOD) of one of the broadcaster's past streams.
    /// </summary>
    public static TwitchVideoType Archive { get; } = new("archive");
    /// <summary>
    /// A highlight reel of one of the broadcaster's past streams.
    /// </summary>
    public static TwitchVideoType Highlight { get; } = new("highlight");
    /// <summary>
    /// A video that the broadcaster uploaded to their video library.
    /// </summary>
    public static TwitchVideoType Upload { get; } = new("upload");
}
