using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Entitlements;

/// <summary>
/// Contains static definitions for possible Drops Entitlement statues.
/// </summary>
/// <param name="Value">The value of the Drops Entitlement status.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<DropsEntitlementStatus, string>))]
public record DropsEntitlementStatus(string Value) : ValueBackedEnum<string>(Value)
{
    public static DropsEntitlementStatus Claimed { get; } = new("CLAIMED");
    public static DropsEntitlementStatus Fulfilled { get; } = new("FULFILLED");
}
