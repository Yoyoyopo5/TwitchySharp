using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch channel goal.
/// </summary>
/// <param name="Value">The string value of the id</param>
[JsonConverter(typeof(WrapperJsonConverter<ChannelGoalId, string>))]
public readonly record struct ChannelGoalId(string Value) : IWrapValue<string>
{
    public static implicit operator string(ChannelGoalId id)
        => id.Value;
    public override string ToString()
        => Value;
}