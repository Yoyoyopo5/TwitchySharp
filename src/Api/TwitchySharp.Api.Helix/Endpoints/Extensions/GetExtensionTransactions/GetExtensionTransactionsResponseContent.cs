namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Contains a list of extension transactions.
/// </summary>
public record GetExtensionTransactionsResponseContent
    : IPageableResponse
{
    /// <summary>
    /// The list of transactions.
    /// </summary>
    public required ExtensionTransactionData[] Data { get; init; }
    /// <summary>
    /// The cursor used to get the next page of results. Use the <see cref="Pagination.Cursor"/> property to set the request’s after parameter.
    /// </summary>
    public Pagination Pagination { get; init; } = new();
}
