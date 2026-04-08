using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch Drops campaign.
/// </summary>
/// <param name="Value">The string value of the id</param>
public readonly partial record struct DropsCampaignId(string Value) : IWrapValue<string>;