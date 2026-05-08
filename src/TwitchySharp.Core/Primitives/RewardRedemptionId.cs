using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// An id representing a specific Twitch Channel Points reward redemption.
/// </summary>
/// <param name="Value">The string value of the redemption id.</param>
[Wrapper<string>]
public readonly partial record struct RewardRedemptionId(string Value);
