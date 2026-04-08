using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch Channel Points reward.
/// </summary>
/// <param name="Value">The string value of the reward id.</param>
public readonly partial record struct RewardId(string Value) : IWrapValue<string>;
