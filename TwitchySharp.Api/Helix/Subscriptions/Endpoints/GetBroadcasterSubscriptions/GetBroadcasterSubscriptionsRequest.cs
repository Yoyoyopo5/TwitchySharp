using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Subscriptions;
/// <summary>
/// Gets a list of users that subscribe to the specified broadcaster.
/// </summary>
/// <remarks>
/// <para>
/// Requires a user access token that includes <see cref="Scope.ChannelReadSubscriptions"/>.
/// A Twitch extension may use an app access token if the broadcaster has granted <see cref="Scope.ChannelReadSubscriptions"/> from within the Twitch Extensions manager.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-broadcaster-subscriptions">Get Broadcaster Subscriptions</see> for more information.
/// </remarks>
public record GetBroadcasterSubscriptionsRequest
    : TwitchHelixRequest<GetBroadcasterSubscriptionsResponse>, IPageableRequest
{
    protected override string Path => "/subscriptions";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(BroadcasterId);
    public override IEnumerable<Scope> ValidScopes => [ Scope.ChannelReadSubscriptions ];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("user_id", UserIds?.Select(x => x.Value))
            .Add("first", First?.ToString())
            .Add("before", Before?.Value)
            .Add("after", After?.Value);

    /// <summary>
    /// The user id of the broadcaster to get subscribers for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }
    /// <summary>
    /// Filter the list of subscribers by user id.
    /// </summary>
    /// <remarks>
    /// You may specify a maximum of 100 subscribers.
    /// </remarks>
    public IEnumerable<UserId>? UserIds { get; init; }
    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 item per page and the maximum is 100 items per page.
    /// The default is 20.
    /// </remarks>
    public PaginationAmount? First { get; init; }
    /// <summary>
    /// The cursor of the result to get results before.
    /// </summary>
    /// <remarks>
    /// Do not specify if you set <see cref="UserIds"/>.
    /// The <see cref="Pagination"/> object in the response contains the cursor's value.
    /// </remarks>
    public PaginationCursor? Before { get; init; }
    /// <summary>
    /// <inheritdoc cref="IPageableRequest.After"/>
    /// </summary>
    /// <remarks>
    /// <inheritdoc cref="IPageableRequest.After"/>
    /// Do not specify if you set <see cref="UserIds"/>.
    /// </remarks>
    public PaginationCursor? After { get; init; }
}
