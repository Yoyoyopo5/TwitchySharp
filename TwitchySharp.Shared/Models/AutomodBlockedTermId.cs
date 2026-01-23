using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Automod blocked term.
/// </summary>
/// <param name="Value">The string value of the id.</param>
[JsonConverter(typeof(WrapperJsonConverter<AutomodBlockedTermId, string>))]
public readonly record struct AutomodBlockedTermId(string Value) : IWrapValue<string>
{
    public static implicit operator string(AutomodBlockedTermId id)
        => id.Value;
    public override string ToString()
        => Value;
}