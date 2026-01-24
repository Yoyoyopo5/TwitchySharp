using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Schedule;
/// <summary>
/// Gets the broadcaster’s streaming schedule.
/// </summary>
/// <remarks>
/// You can get the entire schedule or specific segments of the schedule.
/// <br/>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-channel-stream-schedule">Get Channel Stream Schedule</see> for more information.
/// </remarks>
public record GetChannelStreamScheduleRequest : TwitchHelixRequest<GetChannelStreamScheduleResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An app or user access token.</param>
    /// <param name="parameters">The request parameters.</param>
    public GetChannelStreamScheduleRequest(
        ClientId clientId,
        AccessToken accessToken,
        GetChannelStreamScheduleRequestParameters parameters
        ) : base(
            "/schedule",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("id", parameters.Ids?.Select(x => x.Value))
                .Add("start_time", parameters.StartTime?.ToUniversalTwitchQueryString())
                .Add("first", parameters.First?.ToString())
                .Add("after", parameters.After?.Value)
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetChannelStreamScheduleRequest"/>.
/// </summary>
public record GetChannelStreamScheduleRequestParameters
    : IPageableRequest
{
    /// <summary>
    /// The user id of the broadcaster to get the streaming schedule for.
    /// </summary>
    public required UserId BroadcasterId { get; set; }
    /// <summary>
    /// The ids of the segments to get.
    /// </summary>
    /// <remarks>
    /// You may specify a maximum of 100 ids.
    /// </remarks>
    public IEnumerable<StreamScheduleSegmentId>? Ids { get; set; }
    /// <summary>
    /// The date and time that identifies when in the broadcaster’s schedule to start returning segments.
    /// </summary>
    /// <remarks>
    /// If not specified, the request returns segments starting after the current UTC date and time.
    /// </remarks>
    public DateTimeOffset? StartTime { get; set; }
    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 item per page and the maximum is 25 items per page. 
    /// The default is 20.
    /// </remarks>
    public PaginationAmount? First { get; set; }
    public PaginationCursor? After { get; set; }
}