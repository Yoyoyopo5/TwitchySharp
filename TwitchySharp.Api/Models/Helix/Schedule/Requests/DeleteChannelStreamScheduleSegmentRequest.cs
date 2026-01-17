using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models.Helix;
using TwitchySharp.Api.Models.Helix.Schedule.Responses;

namespace TwitchySharp.Api.Models.Helix.Schedule.Requests;
/// <summary>
/// Removes a broadcast segment from the broadcaster’s streaming schedule.
/// </summary>
/// <remarks>
/// <b>Note:</b> For recurring segments, removing a segment removes all segments in the recurring schedule.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ChannelManageSchedule"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#delete-channel-stream-schedule-segment">Delete Channel Stream Schedule Segment</see> for more information.
/// </remarks>
public record DeleteChannelStreamScheduleSegmentRequest
    : TwitchHelixRequest<DeleteChannelStreamScheduleSegmentResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManageSchedule"/>.</param>
    /// <param name="broadcasterId">
    /// The user id of the broadcaster that owns the streaming schedule to delete a segment from.
    /// This must be the same user that created the <paramref name="accessToken"/>.
    /// </param>
    /// <param name="segmentId">The id of the segment to remove.</param>
    public DeleteChannelStreamScheduleSegmentRequest(
        string clientId,
        string accessToken,
        string broadcasterId,
        string segmentId
        ) : base(
            "/schedule/segment",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
                .Add("id", segmentId)
            )
    {
        Method = HttpMethod.Delete;
    }
}
