using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Gets the list of Bits products that belongs to the extension. 
/// The <paramref name="clientId"/> identifies the extension (this must be the same application that created the <paramref name="accessToken"/>).
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-extension-bits-products">get extension bits products</see> for more information.
/// </summary>
/// <remarks>
/// Requires an app access token. 
/// The client id that created the app access token must be the extension's client id.
/// </remarks>
/// <param name="clientId">The client id of the application. This also identifies the extension to get products from.</param>
/// <param name="accessToken">An app access token.</param>
/// <param name="shouldIncludeAll">Determines whether to include disabled or expired Bits products in the response. The default is <see langword="false"/>.</param>
public class GetExtensionBitsProductsRequest(string clientId, string accessToken, bool? shouldIncludeAll = null)
    : HelixApiRequest<GetExtensionBitsProductsResponse>(
        "/bits/extensions" + 
        new HttpQueryParameters()
            .Add("should_include_all", shouldIncludeAll?.ToString()),
        clientId,
        accessToken
        );
