using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Contains a list of bits products that the extension created.
/// </summary>
public record GetExtensionBitsProductsResponse
{
    /// <summary>
    /// The list of bits products that the extension created.
    /// The list is in ascending SKU order. 
    /// The list is empty if the extension hasn’t created any products or they’re all expired or disabled.
    /// </summary>
    public required ExtensionBitsProduct[] Data { get; init; }
}

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
