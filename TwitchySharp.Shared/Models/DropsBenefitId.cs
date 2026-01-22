using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch Drops benefit.
/// </summary>
/// <param name="Value">The string value of the id</param>
[JsonConverter(typeof(WrapperJsonConverter<DropsBenefitId, string>))]
public readonly record struct DropsBenefitId(string Value) : IWrapValue<string>
{
    public static implicit operator string(DropsBenefitId id)
        => id.Value;
    public override string ToString()
        => Value;
}