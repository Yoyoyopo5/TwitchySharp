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
    : TwitchHelixRequest<GetBroadcasterSubscriptionsResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelReadSubscriptions"/>, or an app access token if this application is an extension and the broadcaster has granted <see cref="Scope.ChannelReadSubscriptions"/> from within the Twitch Extensions manager.</param>
    /// <param name="parameters">The request parameters.</param>
    public GetBroadcasterSubscriptionsRequest(
        ClientId clientId,
        AccessToken accessToken,
        GetBroadcasterSubscriptionsRequestParameters parameters
        ) : base(
            "/subscriptions",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("user_id", parameters.UserIds?.Select(x => x.Value))
                .Add("first", parameters.First?.ToString())
                .Add("before", parameters.Before?.Value)
                .Add("after", parameters.After?.Value)
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetBroadcasterSubscriptionsRequest"/>.
/// </summary>
public record GetBroadcasterSubscriptionsRequestParameters
    : IPageableRequest
{
    /// <summary>
    /// The user id of the broadcaster to get subscribers for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; set; }
    /// <summary>
    /// Filter the list of subscribers by user id.
    /// </summary>
    /// <remarks>
    /// You may specify a maximum of 100 subscribers.
    /// </remarks>
    public IEnumerable<UserId>? UserIds { get; set; }
    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 item per page and the maximum is 100 items per page. 
    /// The default is 20.
    /// </remarks>
    public PaginationAmount? First { get; set; }
    /// <summary>
    /// The cursor of the result to get results before.
    /// </summary>
    /// <remarks>
    /// Do not specify if you set userIds. 
    /// The <see cref="Pagination"/> object in the response contains the cursor’s value.
    /// </remarks>
    public PaginationCursor? Before { get; set; }
    /// <summary>
    /// <inheritdoc cref="IPageableRequest.After"/>
    /// </summary>
    /// <remarks>
    /// <inheritdoc cref="IPageableRequest.After"/>
    /// Do not specify if you set userIds. 
    /// </remarks>
    public PaginationCursor? After { get; set; }
}
