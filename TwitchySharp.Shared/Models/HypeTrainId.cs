using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch chat Hype Train.
/// </summary>
/// <param name="Value">The string value of the id.</param>
[JsonConverter(typeof(WrapperJsonConverter<HypeTrainId, string>))]
public readonly record struct HypeTrainId(string Value) : IWrapValue<string>
{
    public static implicit operator string(HypeTrainId id)
        => id.Value;
    public override string ToString()
        => Value;
}