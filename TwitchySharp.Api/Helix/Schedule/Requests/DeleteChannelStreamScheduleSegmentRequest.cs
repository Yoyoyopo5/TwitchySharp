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
/// Removes a broadcast segment from the broadcaster’s streaming schedule.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#delete-channel-stream-schedule-segment">delete channel stream schedule segment</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelManageSchedule"/>.
/// <para>
/// <b>Note:</b> For recurring segments, removing a segment removes all segments in the recurring schedule.
/// </para>
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManageSchedule"/>.</param>
/// <param name="broadcasterId">
/// The user id of the broadcaster that owns the streaming schedule to delete a segment from.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="segmentId">The id of the segment to remove.</param>
public class DeleteChannelStreamScheduleSegmentRequest(
    string clientId,
    string accessToken,
    string broadcasterId,
    string segmentId
    )
    : HelixApiRequest<DeleteChannelStreamScheduleSegmentResponse>(
        "/schedule/segment" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("id", segmentId),
        clientId,
        accessToken
        )
{
    public override HttpMethod Method => HttpMethod.Delete;
}
