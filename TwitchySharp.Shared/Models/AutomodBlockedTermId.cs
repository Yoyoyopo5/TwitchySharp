using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Automod blocked term.
/// </summary>
/// <param name="Value">The string value of the id.</param>
public readonly partial record struct AutomodBlockedTermId(string Value) : IWrapValue<string>;