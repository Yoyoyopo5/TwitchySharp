using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// An SKU representing a specific Twitch extension Bits product.
/// </summary>
/// <param name="Value">The string value of the SKU.</param>
[Wrapper<string>]
public readonly partial record struct ExtensionProductSku(string Value);
