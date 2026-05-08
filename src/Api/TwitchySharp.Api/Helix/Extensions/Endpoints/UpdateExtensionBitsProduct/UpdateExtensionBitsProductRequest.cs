using System;
using System.Net.Http;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Adds or updates a Bits product that the extension created.
/// </summary>
/// <remarks>
/// If the SKU doesn't exist, the product is added.
/// You may update all fields except the <see cref="UpdateExtensionBitsProductRequestData.Sku"/>.
/// <br/>
/// Requires an app access token.
/// The client id used to create the the app access token must match the extension's client id.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-extension-bits-product">Update Extension Bits Product</see> for more information.
/// </remarks>
public record UpdateExtensionBitsProductRequest
    : TwitchHelixRequest<UpdateExtensionBitsProductResponse>
{
    protected override string Path => "/bits/extensions";
    public override HttpMethod Method => HttpMethod.Put;
    public override object? ContentObject => Product;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.Client(ExtensionId)
    };

    /// <summary>
    /// The client id of the extension to update bits products for.
    /// </summary>
    public required ExtensionId ExtensionId { get; init; }

    /// <summary>
    /// The product data to update.
    /// </summary>
    public required UpdateExtensionBitsProductRequestData Product { get; init; }
}

/// <summary>
/// Contains data used to update a bits product for an extension.
/// </summary>
public record UpdateExtensionBitsProductRequestData
{
    /// <summary>
    /// The product's SKU.
    /// </summary>
    /// <remarks>
    /// The SKU must be unique within an extension. 
    /// The product's SKU cannot be changed. 
    /// The SKU may contain only alphanumeric characters, dashes (-), underscores (_), and periods (.) and is limited to a maximum of 255 characters. 
    /// No spaces.
    /// </remarks>
    public required ExtensionProductSku Sku { get; init; }
    /// <summary>
    /// The product's cost information.
    /// </summary>
    public required ExtensionProductCost Cost { get; init; }
    /// <summary>
    /// The product's name as displayed in the extension. 
    /// </summary>
    /// <remarks>
    /// The maximum length is 255 characters.
    /// </remarks>
    public required string DisplayName { get; init; }
    /// <summary>
    /// Determines whether the product is in development. 
    /// </summary>
    /// <remarks>
    /// Set to <see langword="true"/> if the product is in development and not available for public use. The default is <see langword="false"/>.
    /// </remarks>
    public bool? InDevelopment { get; init; }
    /// <summary>
    /// The date and time when the product expires. 
    /// </summary>
    /// <remarks>
    /// If <see langword="null"/>, the product does not expire. 
    /// To disable the product now, set the expiration date to a date in the past.
    /// </remarks>
    public DateTimeOffset? Expiration { get; init; }
    /// <summary>
    /// Determines whether Bits product purchase events are broadcast to all instances of the extension on a channel. 
    /// </summary>
    /// <remarks>
    /// The events are broadcast via the onTransactionComplete helper callback. 
    /// The default is <see langword="false"/>.
    /// </remarks>
    public bool? IsBroadcast { get; init; }
}
