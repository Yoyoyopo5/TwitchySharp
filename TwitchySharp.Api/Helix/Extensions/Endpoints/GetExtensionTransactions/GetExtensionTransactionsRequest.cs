using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Gets an extension's list of transactions.
/// </summary>
/// <remarks>
/// A transaction records the exchange of a currency (for example, Bits) for a digital product.
/// <br/>
/// Requires an app access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-extension-transactions">Get Extension Transactions</see> for more information.
/// </remarks>
public record GetExtensionTransactionsRequest
    : TwitchHelixRequest<GetExtensionTransactionsResponse>, IPageableRequest
{
    protected override string Path => "/extensions/transactions";
    public override HttpMethod Method => HttpMethod.Get;
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("extension_id", ExtensionId)
            .Add("first", First?.ToString())
            .Add("after", After?.ToString())
            .Add("id", TransactionIds?.Select(x => x.ToString()));

    /// <summary>
    /// The id of the extension whose list of transactions you want to get.
    /// </summary>
    public required ExtensionId ExtensionId { get; init; }

    /// <summary>
    /// The transaction ids used to filter the list of transactions.
    /// </summary>
    /// <remarks>
    /// You may specify a maximum of 100 ids.
    /// </remarks>
    public IEnumerable<ExtensionTransactionId>? TransactionIds { get; init; }

    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 item per page and the maximum is 100 items per page.
    /// The default is 20.
    /// </remarks>
    public PaginationAmount? First { get; init; }

    /// <inheritdoc/>
    public PaginationCursor? After { get; init; }
}
