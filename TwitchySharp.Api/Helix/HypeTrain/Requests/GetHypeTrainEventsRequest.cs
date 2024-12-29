using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.HypeTrain;
/// <summary>
/// Gets information about the broadcaster’s current or most recent Hype Train event.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-hype-train-events">get Hype Train events</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadHypeTrain"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelReadHypeTrain"/>.</param>
/// <param name="broadcasterId">
/// The user id of the broadcaster that has the Hype Train to get events for.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="first">
/// The maximum number of items to return per page in the response. 
/// The minimum page size is 1 item per page and the maximum is 100 items per page. 
/// The default is 1.
/// </param>
/// <param name="after">
/// The cursor used to get the next page of results. 
/// The <see cref="Pagination"/> property in the response contains the cursor’s value.
/// </param>
public class GetHypeTrainEventsRequest(
    string clientId,
    string accessToken,
    string broadcasterId,
    int? first = null,
    string? after = null
    )
    : HelixApiRequest<GetHypeTrainEventsResponse>(
        "/hypetrain/events" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("first", first?.ToString())
            .Add("after", after),
        clientId,
        accessToken
        );