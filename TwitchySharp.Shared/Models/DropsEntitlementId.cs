using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch Drops entitlement.
/// </summary>
/// <param name="Value">The string value of the id</param>
public readonly partial record struct DropsEntitlementId(string Value) : IWrapValue<string>;