using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Helpers.JsonConverters;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Schedule;
/// <summary>
/// Adds a single or recurring broadcast to the broadcaster's streaming schedule.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelManageSchedule"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#create-channel-stream-schedule-segment">Create Channel Stream Schedule Segment</see> for more information.
/// </remarks>
public record CreateChannelStreamScheduleSegmentRequest
    : TwitchHelixRequest<CreateChannelStreamScheduleSegmentResponse>
{
    protected override string Path => "/schedule/segment";
    public override HttpMethod Method => HttpMethod.Post;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(BroadcasterId);
    public override IEnumerable<Scope> ValidScopes => [ Scope.ChannelManageSchedule ];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId);
    public override object? ContentObject => ScheduleSegment;

    /// <summary>
    /// The user id of the broadcaster (channel) that owns the schedule to add the broadcast segment to.
    /// This must be the same user that created the access token.
    /// </summary>
    public required UserId BroadcasterId { get; set; }

    /// <summary>
    /// The segment to add.
    /// </summary>
    public required CreateChannelStreamScheduleSegmentRequestData ScheduleSegment { get; set; }
}

/// <summary>
/// Data used to create a new stream schedule segment.
/// </summary>
public record CreateChannelStreamScheduleSegmentRequestData
{
    /// <summary>
    /// The date and time that the broadcast segment starts.
    /// </summary>
    public required DateTimeOffset StartTime { get; set; }
    /// <summary>
    /// The time zone where the broadcast takes place.
    /// </summary>
    [JsonConverter(typeof(IanaTimeZoneJsonConverter))]
    public required TimeZoneInfo Timezone { get; set; }
    /// <summary>
    /// The length of time that the broadcast is scheduled to run. 
    /// </summary>
    /// <remarks>
    /// The duration can range from 30 minutes to 23 hours.
    /// </remarks>
    [JsonConverter(typeof(MinutesTimeSpanJsonConverter))]
    public required TimeSpan Duration { get; set; }
    /// <summary>
    /// Determines whether the broadcast recurs weekly. 
    /// Set to <see langword="true"/> if the broadcast will recur weekly. 
    /// Only partners and affiliates may add non-recurring broadcasts.
    /// </summary>
    public bool? IsRecurring { get; set; }
    /// <summary>
    /// The id of the category for the scheduled stream segment.
    /// </summary>
    public GameId? CategoryId { get; set; }
    /// <summary>
    /// The title for the scheduled broadcast.
    /// This may contain up to a maximum of 140 characters.
    /// </summary>
    public string? Title { get; set; }
}
