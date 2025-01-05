using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Schedule;
/// <summary>
/// Updates a scheduled broadcast segment.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-channel-stream-schedule-segment">update channel stream schedule segment</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelManageSchedule"/>.
/// <para>
/// For recurring segments, updating a segment’s title, category, duration, and timezone, changes all segments in the recurring schedule, not just the specified segment.
/// </para>
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManageSchedule"/>.</param>
/// <param name="broadcasterId">
/// The user id of the broadcaster (channel) to update a schedule segment for.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="segmentId">The id of the segment to update.</param>
/// <param name="segmentSettings">The new settings to update the segment to.</param>
public class UpdateChannelStreamScheduleSegmentRequest(
    string clientId,
    string accessToken,
    string broadcasterId,
    string segmentId,
    UpdateChannelStreamScheduleSegmentRequestData segmentSettings
    )
    : HelixApiRequest<UpdateChannelStreamScheduleResponse, UpdateChannelStreamScheduleSegmentRequestData>(
        "/schedule/segment" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("id", segmentId),
        clientId,
        accessToken,
        segmentSettings
        )
{
    public override HttpMethod Method => HttpMethod.Patch;
}

/// <summary>
/// Used to update a specific stream schedule segment.
/// All properties are optional.
/// </summary>
public record UpdateChannelStreamScheduleSegmentRequestData
{
    /// <summary>
    /// The date and time that the broadcast segment starts.
    /// <b>Note:</b> Only partners and affiliates may update a broadcast’s start time and only for non-recurring segments.
    /// </summary>
    public DateTimeOffset? StartTime { get; set; }
    /// <summary>
    /// The length of time, in <b>minutes</b>, that the broadcast is scheduled to run. 
    /// The duration must be in the range 30 through 1380 (23 hours).
    /// </summary>
    public int? Duration { get; set; }
    /// <summary>
    /// The id of the category for the scheduled stream segment.
    /// </summary>
    public string? CategoryId { get; set; }
    /// <summary>
    /// The title for the scheduled broadcast.
    /// This may contain up to a maximum of 140 characters.
    /// </summary>
    public string? Title { get; set; }
    /// <summary>
    /// Determines whether the broadcast is canceled. 
    /// Set to <see langword="true"/> to cancel the segment.
    /// <para>
    /// <b>Note:</b> For recurring segments, the API cancels the first segment after the current UTC date and time and not the specified segment (unless the specified segment is the next segment after the current UTC date and time).
    /// </para>
    /// </summary>
    public bool? IsCancelled { get; set; }
    /// <summary>
    /// The time zone where the broadcast takes place. 
    /// Specify the time zone using <see href="https://www.iana.org/time-zones">IANA time zone database</see> format (for example, <c>"America/New_York"</c>).
    /// </summary>
    public string? Timezone { get; set; }
}
