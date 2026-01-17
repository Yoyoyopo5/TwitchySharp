namespace TwitchySharp.Api.Models.Helix.Extensions.Models;

/// <summary>
/// Contains details about a digital product for an extension (bits used in extension).
/// </summary>
public record ExtensionProductData
{
    /// <summary>
    /// An ID that identifies the digital product.
    /// </summary>
    public required string Sku { get; init; }
    /// <summary>
    /// Set to: "twitch.ext.{the extension's ID}".
    /// </summary>
    public required string Domain { get; init; }
    /// <summary>
    /// Contains details about the digital product’s cost.
    /// </summary>
    public required ExtensionProductCost Cost { get; init; }
}
