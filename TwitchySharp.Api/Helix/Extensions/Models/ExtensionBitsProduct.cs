using System;

namespace TwitchySharp.Api.Helix.Extensions;

/// <summary>
/// Contains information about a specific extension bits product.
/// </summary>
public record ExtensionBitsProduct
{
    /// <summary>
    /// The product’s SKU. 
    /// The SKU is unique across an extension’s products.
    /// </summary>
    public required string Sku { get; init; }
    /// <summary>
    /// An object that contains the product’s cost information.
    /// </summary>
    public required ExtensionProductCost Cost { get; init; }
    /// <summary>
    /// Determines whether the product is in development. 
    /// If <see langword="true"/>, the product is not available for public use.
    /// </summary>
    public required bool InDevelopment { get; init; }
    /// <summary>
    /// The product’s name as displayed in the extension.
    /// </summary>
    public required string DisplayName { get; init; }
    /// <summary>
    /// The date and time when the product expires.
    /// </summary>
    public required DateTimeOffset Expiration { get; init; }
    /// <summary>
    /// Determines whether Bits product purchase events are broadcast to all instances of an extension on a channel. 
    /// The events are broadcast via the onTransactionComplete helper callback. 
    /// Is <see langword="true"/> if the event is broadcast to all instances.
    /// </summary>
    public required bool IsBroadcast { get; init; }
}
