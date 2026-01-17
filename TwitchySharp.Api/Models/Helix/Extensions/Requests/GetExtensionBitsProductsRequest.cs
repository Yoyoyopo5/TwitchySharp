using System.Net.Http;
using TwitchySharp.Api.Models.Helix.Extensions.Responses;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Extensions.Requests;
/// <summary>
/// Gets the list of Bits products that belongs to the extension.
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
    /// <param name="shouldIncludeAll">Determines whether to include disabled or expired Bits products in the response. The default is <see langword="false"/>.</param>
    public GetExtensionBitsProductsRequest(
        string clientId,
        string accessToken,
        bool? shouldIncludeAll = null
        ) : base(
            "/bits/extensions",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("should_include_all", shouldIncludeAll?.ToString().ToLower())
            )
    {
        Method = HttpMethod.Get;
    }
}
