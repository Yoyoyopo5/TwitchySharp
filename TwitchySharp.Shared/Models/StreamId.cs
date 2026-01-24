using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch stream.
/// </summary>
/// <param name="Value">The string value of the id.</param>
[JsonConverter(typeof(WrapperJsonConverter<StreamId, string>))]
public readonly record struct StreamId(string Value) : IWrapValue<string>
{
    public static implicit operator VideoId(StreamId id)
        => new(id);
    public static implicit operator string(StreamId id)
        => id.Value;
    public override string ToString()
        => Value;
}