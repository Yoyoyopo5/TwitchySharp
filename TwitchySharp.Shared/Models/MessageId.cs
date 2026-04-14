using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch chat message.
/// </summary>
/// <param name="Value">The string value of the id.</param>
[Wrapper<string>]
public readonly partial record struct MessageId(string Value);