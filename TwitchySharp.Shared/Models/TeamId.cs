using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch team.
/// </summary>
/// <param name="Value">The string value of the id</param>
[JsonConverter(typeof(WrapperJsonConverter<TeamId, string>))]
public readonly record struct TeamId(string Value) : IWrapValue<string>
{
    public static implicit operator string(TeamId id)
        => id.Value;
    public override string ToString()
        => Value;
}