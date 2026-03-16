using System.Collections.Immutable;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Schedule;
/// <summary>
/// Removes a broadcast segment from the broadcaster's streaming schedule.
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
    protected override string Path => "/schedule/segment";
    public override HttpMethod Method => HttpMethod.Delete;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.User(BroadcasterId),
        ValidScopes = ImmutableHashSet.Create(Scope.ChannelManageSchedule)
    };
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("id", SegmentId);

    /// <summary>
    /// The user id of the broadcaster that owns the streaming schedule to delete a segment from.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The id of the segment to remove.
    /// </summary>
    public required StreamScheduleSegmentId SegmentId { get; init; }

    protected override ValueTask<DeleteChannelStreamScheduleSegmentResponse> ConvertResponseContent(Stream contentStream, CancellationToken ct = default)
        => ValueTask.FromResult(new DeleteChannelStreamScheduleSegmentResponse());
}
