using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// An id representing a specific Twitch stream.
/// </summary>
/// <param name="Value">The string value of the id.</param>
[Wrapper<string>]
public readonly partial record struct StreamId(string Value)
{
    /// <summary>
    /// Use the <see cref="StreamId"/> as a <see cref="VideoId"/>.
    /// </summary>
    /// <param name="id">The <see cref="StreamId"/> to use.</param>
    public static implicit operator VideoId(StreamId id)
        => new(id);
}
