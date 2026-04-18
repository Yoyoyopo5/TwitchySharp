using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Contains static definitions for possible Extension product types.
/// </summary>
/// <param name="Value">The string value of the product type.</param>
[Wrapper<string>]
public readonly partial record struct ExtensionProductType(string Value)
{
    public static ExtensionProductType BitsInExtension { get; } = new("BITS_IN_EXTENSION");
}
