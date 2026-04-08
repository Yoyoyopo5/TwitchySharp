using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// A Twitch extension version.
/// </summary>
/// <param name="Value">The string value of the extension version.</param>
public readonly partial record struct ExtensionVersion(string Value) : IWrapValue<string>;