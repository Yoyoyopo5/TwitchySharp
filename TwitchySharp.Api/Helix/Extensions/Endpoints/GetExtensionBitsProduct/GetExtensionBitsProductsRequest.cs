using System.Net.Http;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Gets the list of Bits products that belong to the extension.
/// </summary>
/// <remarks>
/// The client id identifies the extension (this must be the same application that created the access token).
/// <br/>
/// Requires an app access token. 
/// The client id that created the app access token must be the extension's client id.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-extension-bits-products">Get Extension Bits Products</see> for more information.
/// </remarks>
public record GetExtensionBitsProductsRequest
    : TwitchHelixRequest<GetExtensionBitsProductsResponse>
{
    /// <param name="clientId">The client id of the extension. This also identifies the extension to get products from.</param>
    /// <param name="accessToken">An app access token.</param>
    /// <param name="parameters">The request parameters.</param>
    public GetExtensionBitsProductsRequest(
        ClientId clientId,
        AppAccessToken accessToken,
        GetExtensionBitsProductsRequestParameters? parameters = null
        ) : base(
            "/bits/extensions",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("should_include_all", parameters?.ShouldIncludeAll?.ToString())
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetExtensionBitsProductsRequest"/>.
/// </summary>
public record GetExtensionBitsProductsRequestParameters
{
    /// <summary>
    /// Determines whether to include disabled or expired Bits products in the response. 
    /// </summary>
    /// <remarks>
    /// The default is <see langword="false"/>.
    /// </remarks>
    public bool? ShouldIncludeAll { get; set; }
}
