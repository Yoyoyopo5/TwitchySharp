using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch Channel Points reward redemption.
/// </summary>
/// <param name="Value">The string value of the redemption id.</param>
public readonly partial record struct RewardRedemptionId(string Value) : IWrapValue<string>;