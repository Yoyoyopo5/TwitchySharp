using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Automod blocked term.
/// </summary>
/// <param name="Value">The string value of the id.</param>
[Wrapper<string>]
public readonly partial record struct AutomodBlockedTermId(string Value);