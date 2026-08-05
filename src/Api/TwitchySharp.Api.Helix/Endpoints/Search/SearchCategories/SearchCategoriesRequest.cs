namespace TwitchySharp.Api.Helix.Search;
/// <summary>
/// Gets the games or categories that match the specified query.
/// </summary>
/// <remarks>
/// To match, the category's name must contain all parts of the query string.
/// For example, if the query string is 42, the response includes any category name that contains 42 in the title.
/// If the query string is a phrase like love computer, the response includes any category name that contains the words love and computer anywhere in the name.
/// The comparison is case insensitive.
/// <br/>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#search-categories">Search Categories</see> for more information.
/// </remarks>
public record SearchCategoriesRequest
    : TwitchHelixRequest<SearchCategoriesResponse>, IForwardPageableRequest
{
    protected override string Path => "/search/categories";
    public override HttpMethod Method => HttpMethod.Get;
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("query", Query)
            .Add("first", First?.ToString())
            .Add("after", After?.Value);

    /// <summary>
    /// The search string.
    /// </summary>
    public required string Query { get; init; }

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
