using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch Drops entitlement.
/// </summary>
/// <param name="Value">The string value of the id</param>
[JsonConverter(typeof(WrapperJsonConverter<DropsEntitlementId, string>))]
public readonly record struct DropsEntitlementId(string Value) : IWrapValue<string>
{
    public static implicit operator string(DropsEntitlementId id)
        => id.Value;
    public override string ToString()
        => Value;
}