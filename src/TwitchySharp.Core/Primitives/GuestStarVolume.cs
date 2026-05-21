using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// The volume of a guest star session guest.
/// </summary>
/// <remarks>
/// Ranges from 0-100.
/// </remarks>
/// <param name="Value">The int value of the volume.</param>
[Wrapper<int>]
public readonly partial record struct GuestStarVolume(int Value);
