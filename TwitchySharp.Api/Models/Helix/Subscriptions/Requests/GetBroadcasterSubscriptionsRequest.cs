using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models.Helix.Subscriptions.Responses;
using TwitchySharp.Api.Models.Shared;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Subscriptions.Requests;
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
    /// <param name="broadcasterId">
    /// The user id of the broadcaster to get subscribers for.
    /// This must be the same user that created the <paramref name="accessToken"/>.
    /// </param>
    /// <param name="userIds">
    /// Filter the list of subscribers by user id.
    /// You may specify a maximum of 100 subscribers.
    /// </param>
    /// <param name="first">
    /// The maximum number of items to return per page in the response. 
    /// The minimum page size is 1 item per page and the maximum is 100 items per page. 
    /// The default is 20.
    /// </param>
    /// <param name="before">
    /// The cursor used to get the previous page of results. 
    /// Do not specify if you set <paramref name="userIds"/>. 
    /// The <see cref="Pagination"/> object in the response contains the cursor’s value.
    /// </param>
    /// <param name="after">
    /// The cursor used to get the next page of results. 
    /// Do not specify if you set <paramref name="userIds"/>. 
    /// The <see cref="Pagination"/> object in the response contains the cursor’s value.
    /// </param>
    public GetBroadcasterSubscriptionsRequest(
        string clientId,
        string accessToken,
        string broadcasterId,
        IEnumerable<string>? userIds = null,
        int? first = null,
        string? before = null,
        string? after = null
        ) : base(
            "/subscriptions",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
                .Add("user_id", userIds)
                .Add("first", first?.ToString())
                .Add("before", before)
                .Add("after", after)
            )
    {
        Method = HttpMethod.Get;
    }
}
