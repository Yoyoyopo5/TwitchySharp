using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Schedule;
/// <summary>
/// Gets the broadcaster’s streaming schedule. 
/// You can get the entire schedule or specific segments of the schedule.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-channel-stream-schedule">get channel stream schedule</see> for more information.
/// </summary>
/// <remarks>
/// Requires an app or user access token.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">An app or user access token.</param>
/// <param name="broadcasterId">The user id of the broadcaster to get the streaming schedule for.</param>
/// <param name="ids">
/// The ids of the segments to get.
/// You may specify a maximum of 100 ids.
/// </param>
/// <param name="startTime">
/// The date and time that identifies when in the broadcaster’s schedule to start returning segments. 
/// If not specified, the request returns segments starting after the current UTC date and time.
/// </param>
/// <param name="first">
/// The maximum number of items to return per page in the response. 
/// The minimum page size is 1 item per page and the maximum is 25 items per page. 
/// The default is 20.
/// </param>
/// <param name="after">
/// The cursor used to get the next page of results. 
/// The <see cref="Pagination"/> object in the response contains the cursor’s value.
/// </param>
public class GetChannelStreamScheduleRequest(
    string clientId,
    string accessToken,
    string broadcasterId,
    IEnumerable<string>? ids = null,
    DateTimeOffset? startTime = null,
    int? first = null,
    string? after = null
    )
    : HelixApiRequest<GetChannelStreamScheduleResponse>(
        "/schedule" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("id", ids)
            .Add("start_time", startTime?.ToUniversalTwitchQueryString())
            .Add("first", first?.ToString())
            .Add("after", after),
        clientId,
        accessToken
        );
