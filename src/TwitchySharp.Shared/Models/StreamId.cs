using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch stream.
/// </summary>
/// <param name="Value">The string value of the id.</param>
[Wrapper<string>]
public readonly partial record struct StreamId(string Value)
{
    public static implicit operator VideoId(StreamId id)
        => new(id);
}
