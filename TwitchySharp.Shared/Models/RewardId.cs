using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch Channel Points reward.
/// </summary>
/// <param name="Value">The string value of the reward id.</param>
[Wrapper<string>]
public readonly partial record struct RewardId(string Value);
