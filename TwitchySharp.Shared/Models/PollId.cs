using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch chat poll.
/// </summary>
/// <param name="Value">The string value of the id</param>
[JsonConverter(typeof(WrapperJsonConverter<PollId, string>))]
public readonly record struct PollId(string Value) : IWrapValue<string>
{
    public static implicit operator string(PollId id)
        => id.Value;
    public override string ToString()
        => Value;
}