using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Models.Helix.Extensions.Responses;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Extensions.Requests;
/// <summary>
/// Gets an extension’s list of transactions.
/// </summary>
/// <remarks>
/// A transaction records the exchange of a currency (for example, Bits) for a digital product.
/// <br/>
/// Requires an app access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-extension-transactions">Get Extension Transactions</see> for more information.
/// </remarks>
public record GetExtensionTransactionsRequest
    : TwitchHelixRequest<GetExtensionTransactionsResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An app access token.</param>
    /// <param name="extensionId">The ID of the extension whose list of transactions you want to get.</param>
    /// <param name="transactionIds">
    /// The transaction IDs used to filter the list of transactions. 
    /// You may specify a maximum of 100 IDs.
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
    public GetExtensionTransactionsRequest(
        string clientId,
        string accessToken,
        string extensionId,
        IEnumerable<string>? transactionIds = null,
        int? first = null,
        string? after = null
        ) : base(
            "/extensions/transactions",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("extension_id", extensionId)
                .Add("first", first?.ToString())
                .Add("after", after)
                .Add("id", transactionIds)
            )
    {
        Method = HttpMethod.Get;
    }
}
