using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch emote set.
/// </summary>
/// <param name="Value">The string value of the id.</param>
public readonly partial record struct EmoteSetId(string Value) : IWrapValue<string>;