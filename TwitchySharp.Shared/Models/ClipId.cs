using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch clip.
/// </summary>
/// <param name="Value">The string value of the id</param>
public readonly partial record struct ClipId(string Value) : IWrapValue<string>;