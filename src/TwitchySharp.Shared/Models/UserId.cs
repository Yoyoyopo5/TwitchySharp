using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Shared.Models;
/// <summary>
/// An id representing a specific Twitch user.
/// </summary>
/// <remarks>
/// Note that while users can change their logins and display names, the id will never change.
/// </remarks>
/// <param name="Value">The string value of the user id.</param>
[Wrapper<string>]
public readonly partial record struct UserId(string Value);
