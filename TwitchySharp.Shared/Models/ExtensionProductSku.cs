using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An SKU representing a specific Twitch extension Bits product.
/// </summary>
/// <param name="Value">The string value of the SKU.</param>
[Wrapper<string>]
public readonly partial record struct ExtensionProductSku(string Value);
