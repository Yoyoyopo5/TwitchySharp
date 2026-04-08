using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch chat unban request.
/// </summary>
/// <param name="Value">The string value of the id.</param>
public readonly partial record struct UnbanRequestId(string Value) : IWrapValue<string>;
