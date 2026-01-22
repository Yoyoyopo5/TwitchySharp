using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Extensions;
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
    /// <param name="parameters">The request parameters.</param>
    public GetExtensionTransactionsRequest(
        ClientId clientId,
        AppAccessToken accessToken,
        GetExtensionTransactionsRequestParameters parameters
        ) : base(
            "/extensions/transactions",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("extension_id", parameters.ExtensionId)
                .Add("first", parameters.First?.ToString())
                .Add("after", parameters.After?.Value)
                .Add("id", parameters.TransactionIds?.Select(x => x.ToString()))
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetExtensionTransactionsRequest"/>.
/// </summary>
public record GetExtensionTransactionsRequestParameters
    : IPageableRequest
{
    /// <summary>
    /// The id of the extension whose list of transactions you want to get.
    /// </summary>
    public required ExtensionId ExtensionId { get; set; }
    /// <summary>
    /// The transaction ids used to filter the list of transactions
    /// </summary>
    /// <remarks>
    /// You may specify a maximum of 100 ids.
    /// </remarks>
    public IEnumerable<ExtensionTransactionId>? TransactionIds { get; set; }
    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 item per page and the maximum is 100 items per page. 
    /// The default is 20.
    /// </remarks>
    public PaginationAmount? First { get; set; }
    public PaginationCursor? After { get; set; }
}
