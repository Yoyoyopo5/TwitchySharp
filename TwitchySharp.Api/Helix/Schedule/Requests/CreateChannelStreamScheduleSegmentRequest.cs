using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Schedule;
/// <summary>
/// Adds a single or recurring broadcast to the broadcaster’s streaming schedule.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#create-channel-stream-schedule-segment">create channel stream schedule segment</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelManageSchedule"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManageSchedule"/>.</param>
/// <param name="broadcasterId">
/// The user id of the broadcaster (channel) that owns the schedule to add the broadcast segment to.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="scheduleSegment">The segment to add.</param>
public class CreateChannelStreamScheduleSegmentRequest(
    string clientId,
    string accessToken,
    string broadcasterId,
    CreateChannelStreamScheduleSegmentRequestData scheduleSegment
    )
    : HelixApiRequest<CreateChannelStreamScheduleSegmentResponse, CreateChannelStreamScheduleSegmentRequestData>(
        "/schedule/segment" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId),
        clientId,
        accessToken,
        scheduleSegment
        );

/// <summary>
/// Data used to create a new stream schedule segment.
/// </summary>
public record CreateChannelStreamScheduleSegmentRequestData
{
    /// <summary>
    /// The date and time that the broadcast segment starts.
    /// </summary>
    public required DateTimeOffset StartTime { get; set; } // Need converter here?
    /// <summary>
    /// The time zone where the broadcast takes place. 
    /// Specify the time zone using <see href="https://www.iana.org/time-zones">IANA time zone database</see> format (for example, <c>"America/New_York"</c>).
    /// </summary>
    public required string Timezone { get; set; }
    /// <summary>
    /// The length of time, in <b>minutes</b>, that the broadcast is scheduled to run. 
    /// The duration must be in the range 30 through 1380 (23 hours).
    /// </summary>
    [JsonConverter(typeof(IntStringJsonConverter))]
    public required int Duration { get; set; }
    /// <summary>
    /// Determines whether the broadcast recurs weekly. 
    /// Set to <see langword="true"/> if the broadcast will recur weekly. 
    /// Only partners and affiliates may add non-recurring broadcasts.
    /// </summary>
    public bool? IsRecurring { get; set; }
    /// <summary>
    /// The id of the category for the scheduled stream segment.
    /// </summary>
    public string? CategoryId { get; set; }
    /// <summary>
    /// The title for the scheduled broadcast.
    /// This may contain up to a maximum of 140 characters.
    /// </summary>
    public string? Title { get; set; }
}
