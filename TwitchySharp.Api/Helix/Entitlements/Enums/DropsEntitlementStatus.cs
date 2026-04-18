using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Entitlements;

/// <summary>
/// Contains static definitions for possible Drops Entitlement statues.
/// </summary>
/// <param name="Value">The value of the Drops Entitlement status.</param>
[Wrapper<string>]
public readonly partial record struct DropsEntitlementStatus(string Value)
{
    public static DropsEntitlementStatus Claimed { get; } = new("CLAIMED");
    public static DropsEntitlementStatus Fulfilled { get; } = new("FULFILLED");
}
