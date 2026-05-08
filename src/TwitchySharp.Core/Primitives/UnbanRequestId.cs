using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// An id representing a specific Twitch chat unban request.
/// </summary>
/// <param name="Value">The string value of the id.</param>
[Wrapper<string>]
public readonly partial record struct UnbanRequestId(string Value);
