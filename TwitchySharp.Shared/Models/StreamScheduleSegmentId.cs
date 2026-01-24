using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch stream schedule segment.
/// </summary>
/// <param name="Value">The string value of the id.</param>
[JsonConverter(typeof(WrapperJsonConverter<StreamScheduleSegmentId, string>))]
public readonly record struct StreamScheduleSegmentId(string Value) : IWrapValue<string>
{
    public static implicit operator string(StreamScheduleSegmentId id)
        => id.Value;
    public override string ToString()
        => Value;
}