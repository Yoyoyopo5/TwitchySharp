namespace TwitchySharp.Api.Helix.Search;
/// <summary>
/// Gets the channels that match the specified query and have streamed content within the past 6 months.
/// </summary>
/// <remarks>
/// <para>
/// Requires an app or user access token.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#search-channels">Search Channels</see> for more information.
/// </remarks>
public record SearchChannelsRequest
    : TwitchHelixRequest<SearchChannelsResponseContent>, IForwardPageableRequest,
    IAuthenticatedTwitchRequest<ITwitchRequestAuthenticationContext<TwitchIdentity>>
{
    protected override string Path => "/search/channels";
    public override HttpMethod Method => HttpMethod.Get;
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("query", Query)
            .Add("live_only", LiveOnly?.ToString())
            .Add("first", First?.ToString())
            .Add("after", After?.Value);
    public ITwitchRequestAuthenticationContext<TwitchIdentity> AuthenticationContext { get; init; }
        = TwitchRequestAuthenticationContext.Default;

    /// <summary>
    /// The query string to search channels with.
    /// </summary>
    /// <remarks>
    /// The request will return channels where the beginning of the broadcaster's name or category matches the query.
    /// The comparison is case insensitive.
    /// If query is <c>"angel_of_death"</c>, it matches all names that begin with angel_of_death.
    /// However, if query is a phrase like <c>"angel of death"</c>, it matches to names starting with angelofdeath or names starting with angel_of_death.
    /// </remarks>
    public required string Query { get; init; }

    /// <summary>
    /// Determines whether the response includes only channels that are currently streaming live.
    /// </summary>
    /// <remarks>
    /// Defaults to <see langword="false"/>.
    /// The comparison also depends on the value of this parameter.
    /// If it is <see langword="false"/>, the API matches on the broadcaster's login (username).
    /// However, if it is <see langword="true"/>, the API matches on the broadcaster's name and category name.
    /// </remarks>
    public bool? LiveOnly { get; init; }

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
