using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// A Twitch extension version.
/// </summary>
/// <param name="Value">The string value of the extension version.</param>
[Wrapper<string>]
public readonly partial record struct ExtensionVersion(string Value);