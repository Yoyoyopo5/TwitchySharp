using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Streams;

/// <summary>
/// An id representing a specific Twitch stream key.
/// </summary>
/// <param name="Value">The string value of the id.</param>
[JsonConverter(typeof(WrapperJsonConverter<StreamMarkerId, string>))]
public readonly record struct StreamKey(string Value) : IWrapValue<string>
{
    public static implicit operator string(StreamKey key)
        => key.Value;
    public override string ToString()
        => Value;
}
