namespace TwitchySharp.Api.Helix.Entitlements;
/// <summary>
/// Gets an organization's list of entitlements that have been granted to a game, a user, or both.
/// </summary>
/// <remarks>
/// <para>
/// <b>Note:</b> Entitlements returned in the response body data are not guaranteed to be sorted by any field returned by the API.
/// To retrieve <see cref="DropsEntitlementStatus.Claimed"/> or <see cref="DropsEntitlementStatus.Fulfilled"/> entitlements,
/// use the <see cref="FulfillmentStatus"/> property to filter results.
/// To retrieve entitlements for a specific game, use the <see cref="GameId"/> property to filter results.
/// Parameter use varies based on the type of token used.
/// </para>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-drops-entitlements">Get Drops Entitlements</see> for more information.
/// </remarks>
public record GetDropsEntitlementsRequest
    : TwitchHelixRequest<GetDropsEntitlementsResponse>, IPageableRequest
{
    protected override string Path => "/entitlements/drops";
    public override HttpMethod Method => HttpMethod.Get;
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("id", Ids?.Select(x => x.ToString()))
            .Add("user_id", UserId)
            .Add("game_id", GameId)
            .Add("fulfillment_status", FulfillmentStatus?.Value)
            .Add("after", After?.ToString())
            .Add("first", First?.ToString());

    /// <summary>
    /// The ids of the specific entitlements to get.
    /// </summary>
    public IEnumerable<DropsEntitlementId>? Ids { get; init; }

    /// <summary>
    /// The user id to get entitlements for.
    /// </summary>
    /// <remarks>
    /// Use this parameter to get all entitlements for a specific user.
    /// You can combine this parameter with <see cref="GameId"/>.
    /// Requires the use of an app access token for the request.
    /// </remarks>
    public UserId? UserId { get; init; }

    /// <summary>
    /// The game id to get entitlements for.
    /// </summary>
    /// <remarks>
    /// Use this parameter to get all entitlements for a specific game.
    /// You can combine this parameter with <see cref="UserId"/> if using an app access token.
    /// </remarks>
    public GameId? GameId { get; init; }

    /// <summary>
    /// Filters the returned entitlements by a specified fulfillment status.
    /// </summary>
    public DropsEntitlementStatus? FulfillmentStatus { get; init; }

    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 entitlement per page and the maximum is 1000.
    /// The default is 20.
    /// </remarks>
    public PaginationAmount? First { get; init; }

    /// <inheritdoc/>
    public PaginationCursor? After { get; init; }
}
