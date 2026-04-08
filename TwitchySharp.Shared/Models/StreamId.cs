using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch stream.
/// </summary>
/// <param name="Value">The string value of the id.</param>
public readonly partial record struct StreamId(string Value) : IWrapValue<string>
{
    public static implicit operator VideoId(StreamId id)
        => new(id);
}