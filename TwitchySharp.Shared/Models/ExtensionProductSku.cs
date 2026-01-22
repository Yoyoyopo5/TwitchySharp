using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An SKU representing a specific Twitch extension Bits product.
/// </summary>
/// <param name="Value">The string value of the SKU.</param>
[JsonConverter(typeof(WrapperJsonConverter<ExtensionProductSku, string>))]
public readonly record struct ExtensionProductSku(string Value) : IWrapValue<string>
{
    public static implicit operator string(ExtensionProductSku id)
        => id.Value;
    public override string ToString()
        => Value;
}
