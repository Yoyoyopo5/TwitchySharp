using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch chat poll choice.
/// </summary>
/// <param name="Value">The string value of the id</param>
[JsonConverter(typeof(WrapperJsonConverter<PollChoiceId, string>))]
public readonly record struct PollChoiceId(string Value) : IWrapValue<string>
{
    public static implicit operator string(PollChoiceId id)
        => id.Value;
    public override string ToString()
        => Value;
}