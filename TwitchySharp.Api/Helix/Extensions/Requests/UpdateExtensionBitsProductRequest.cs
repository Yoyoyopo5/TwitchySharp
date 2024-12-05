using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Adds or updates a Bits product that the extension created. 
/// If the SKU doesn’t exist, the product is added. 
/// You may update all fields except the <see cref="UpdateExtensionBitsProductRequestData.Sku"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-extension-bits-product">update extension bits product</see> for more information.
/// </summary>
/// <remarks>
/// Requires an app access token. 
/// The client id used to create the the app access token must match the extension’s client id.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">An app access token.</param>
/// <param name="bitsProductData">The product data to update.</param>
public class UpdateExtensionBitsProductRequest(string clientId, string accessToken, UpdateExtensionBitsProductRequestData bitsProductData)
    : HelixApiRequest<UpdateExtensionBitsProductResponse, UpdateExtensionBitsProductRequestData>(
        "/bits/extensions",
        clientId,
        accessToken,
        bitsProductData
        )
{
    public override HttpMethod Method => HttpMethod.Put;
}

/// <summary>
/// Contains data used to update a bits product for an extension.
/// </summary>
public record UpdateExtensionBitsProductRequestData
{
    /// <summary>
    /// The product's SKU. 
    /// The SKU must be unique within an extension. 
    /// The product's SKU cannot be changed. 
    /// The SKU may contain only alphanumeric characters, dashes (-), underscores (_), and periods (.) and is limited to a maximum of 255 characters. 
    /// No spaces.
    /// </summary>
    public required string Sku { get; set; }
    /// <summary>
    /// The product's cost information.
    /// </summary>
    public required ExtensionProductCost Cost { get; set; }
    /// <summary>
    /// The product's name as displayed in the extension. 
    /// The maximum length is 255 characters.
    /// </summary>
    public required string DisplayName { get; set; }
    /// <summary>
    /// Determines whether the product is in development. 
    /// Set to <see langword="true"/> if the product is in development and not available for public use. The default is <see langword="false"/>.
    /// </summary>
    public bool? InDevelopment { get; set; }
    /// <summary>
    /// The date and time when the product expires. 
    /// If not set, the product does not expire. 
    /// To disable the product, set the expiration date to a date in the past.
    /// </summary>
    public DateTimeOffset? Expiration { get; set; }
    /// <summary>
    /// Determines whether Bits product purchase events are broadcast to all instances of the extension on a channel. 
    /// The events are broadcast via the onTransactionComplete helper callback. 
    /// The default is <see langword="false"/>.
    /// </summary>
    public bool? IsBroadcast { get; set; }
}
