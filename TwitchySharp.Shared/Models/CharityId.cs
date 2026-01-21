using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific charity on Twitch.
/// </summary>
/// <param name="Value">The string value of the id</param>
[JsonConverter(typeof(WrapperJsonConverter<CharityId, string>))]
public readonly record struct CharityId(string Value) : IWrapValue<string>
{
    public static implicit operator string(CharityId id)
        => id.Value;
    public override string ToString()
        => Value;
}