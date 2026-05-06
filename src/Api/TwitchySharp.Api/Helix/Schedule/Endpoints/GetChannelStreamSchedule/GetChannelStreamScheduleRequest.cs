using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Schedule;
/// <summary>
/// Gets the broadcaster's streaming schedule.
/// </summary>
/// <remarks>
/// You can get the entire schedule or specific segments of the schedule.
/// <br/>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-channel-stream-schedule">Get Channel Stream Schedule</see> for more information.
/// </remarks>
public record GetChannelStreamScheduleRequest
    : TwitchHelixRequest<GetChannelStreamScheduleResponse>, IPageableRequest
{
    protected override string Path => "/schedule";
    public override HttpMethod Method => HttpMethod.Get;
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("id", Ids?.Select(x => x.Value))
            .Add("start_time", StartTime?.UtcDateTime.ToRfc3339())
            .Add("first", First?.ToString())
            .Add("after", After?.Value);

    /// <summary>
    /// The user id of the broadcaster to get the streaming schedule for.
    /// </summary>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The ids of the segments to get.
    /// </summary>
    /// <remarks>
    /// You may specify a maximum of 100 ids.
    /// </remarks>
    public IEnumerable<StreamScheduleSegmentId>? Ids { get; init; }

    /// <summary>
    /// The date and time that identifies when in the broadcaster's schedule to start returning segments.
    /// </summary>
    /// <remarks>
    /// If not specified, the request returns segments starting after the current UTC date and time.
    /// </remarks>
    public DateTimeOffset? StartTime { get; init; }

    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 item per page and the maximum is 25 items per page.
    /// The default is 20.
    /// </remarks>
    public PaginationAmount? First { get; init; }

    /// <inheritdoc/>
    public PaginationCursor? After { get; init; }
}
