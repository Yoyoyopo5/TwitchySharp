using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch Guest Star slot.
/// </summary>
/// <param name="Value">The string value of the id.</param>
[Wrapper<string>]
public readonly partial record struct GuestStarSlotId(string Value);
