using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// A Twitch user's email address.
/// </summary>
/// <param name="Value">The string value of the email address.</param>
[Wrapper<string>]
public readonly partial record struct UserEmail(string Value);
