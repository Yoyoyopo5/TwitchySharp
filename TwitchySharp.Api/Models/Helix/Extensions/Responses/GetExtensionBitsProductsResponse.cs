using TwitchySharp.Api.Models.Helix.Extensions.Models;

namespace TwitchySharp.Api.Models.Helix.Extensions.Responses;
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
