using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An SKU representing a specific Twitch extension Bits product.
/// </summary>
/// <param name="Value">The string value of the SKU.</param>
public readonly partial record struct ExtensionProductSku(string Value) : IWrapValue<string>;
