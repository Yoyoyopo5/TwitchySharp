using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// An id for a specific community subscription gift event in a Twitch chat.
/// </summary>
/// <param name="Value">The string value of the event.</param>
[Wrapper<string>]
public readonly partial record struct CommunityGiftEventId(string Value);
