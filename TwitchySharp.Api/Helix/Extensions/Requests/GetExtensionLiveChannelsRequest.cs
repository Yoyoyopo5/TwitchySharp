using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Gets a list of broadcasters that are streaming live and have installed or activated the extension.
/// It may take a few minutes for the list to include or remove broadcasters that have recently gone live or stopped broadcasting.
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-extension-live-channels">get extension live channels</see> for more information.
/// </summary>
/// <remarks>
/// Requires an app or user access token.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">An app or user access token.</param>
/// <param name="extensionId">
/// The id of the extension to get.
/// Returns the list of broadcasters that are live and that have installed or activated this extension.
/// </param>
/// <param name="first">
/// The maximum number of items to return per page in the response. 
/// The minimum page size is 1 item per page and the maximum is 100 items per page. 
/// The default is 20.
/// </param>
/// <param name="after">
/// The cursor used to get the next page of results. 
/// The <see cref="GetExtensionTransactionsResponse.Pagination"/> property in the response contains the cursor’s value.
/// </param>
public class GetExtensionLiveChannelsRequest(string clientId, string accessToken, string extensionId, int? first = null, string? after = null)
    : HelixApiRequest<GetExtensionLiveChannelsResponse>(
        "/extensions/live" +
        new HttpQueryParameters()
            .Add("extension_id", extensionId)
            .Add("first", first?.ToString())
            .Add("after", after),
        clientId,
        accessToken
        );
