using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch chat unban request.
/// </summary>
/// <param name="Value">The string value of the id.</param>
[JsonConverter(typeof(WrapperJsonConverter<UnbanRequestId, string>))]
public readonly record struct UnbanRequestId(string Value) : IWrapValue<string>
{
    public static implicit operator string(UnbanRequestId id)
        => id.Value;
    public override string ToString()
        => Value;
}
