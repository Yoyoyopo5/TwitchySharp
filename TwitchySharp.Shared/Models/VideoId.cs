using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch VOD.
/// </summary>
/// <param name="Value">The string value of the id</param>
public readonly partial record struct VideoId(string Value) : IWrapValue<string>;