namespace TwitchySharp.EventSub.Models.Events.Extension;

/// <summary>
/// Contains information about a specific Extension Bits product.
/// </summary>
public record ExtensionBitsProduct
{
    /// <summary>
    /// The name of the product.
    /// </summary>
    public required string Name { get; init; }
    /// <summary>
    /// The amount of Bits involved in the transaction.
    /// </summary>
    public required int Bits { get; init; }
    /// <summary>
    /// Unique identifier for the product.
    /// </summary>
    public required string Sku { get; init; }
    /// <summary>
    /// Indicates whether the product is in development.
    /// </summary>
    public required bool InDevelopment { get; init; }
}
