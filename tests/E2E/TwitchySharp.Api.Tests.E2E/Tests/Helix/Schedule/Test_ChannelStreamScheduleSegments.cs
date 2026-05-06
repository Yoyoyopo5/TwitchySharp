using TwitchySharp.Api.Helix.Schedule;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Schedule;

[Collection("twitch")]
public class Test_ChannelStreamScheduleSegments(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_ChannelStreamScheduleSegmentRequests_ReturnSuccessResponses()
    {
        const string TEST_SEGMENT_TITLE = "Test Stream";

        UserId broadcasterId = _fixture.UserIdentity.UserId;
        ITwitchClient client = _fixture.CreateClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        var createResponse = await CreateSegment(client, broadcasterId, TEST_SEGMENT_TITLE, ct);
        ChannelStreamScheduleSegment segment = createResponse.Content.Data.Segments.Where(s => s.Title == TEST_SEGMENT_TITLE).First();

        await UpdateSegment(client, broadcasterId, segment.Id, ct);
        await DeleteSegment(client, broadcasterId, segment.Id, ct);
    }

    private static ValueTask<TwitchResponse<CreateChannelStreamScheduleSegmentResponse>> CreateSegment(ITwitchClient client, UserId broadcasterId, string title, CancellationToken ct)
        => client.SendAsync(new CreateChannelStreamScheduleSegmentRequest()
        {
            BroadcasterId = broadcasterId,
            ScheduleSegment = new CreateChannelStreamScheduleSegmentRequestData()
            {
                Timezone = TimeZoneInfo.Local,
                StartTime = DateTimeOffset.UtcNow.AddHours(2),
                Duration = TimeSpan.FromHours(2),
                Title = title,
                IsRecurring = false
            }
        }, ct);

    private static ValueTask<TwitchResponse<UpdateChannelStreamScheduleSegmentResponse>> UpdateSegment(ITwitchClient client, UserId broadcasterId, StreamScheduleSegmentId segmentId, CancellationToken ct)
        => client.SendAsync(new UpdateChannelStreamScheduleSegmentRequest()
        {
            BroadcasterId = broadcasterId,
            SegmentId = segmentId,
            SegmentSettings = new()
            {
                StartTime = DateTimeOffset.UtcNow + TimeSpan.FromHours(6),
                Duration = TimeSpan.FromHours(1),
                CategoryId = GameId.None,
                IsCancelled = true,
                Timezone = TimeZoneInfo.Local,
                Title = "Cancelled Test Stream"
            }
        }, ct);

    private static ValueTask<TwitchResponse<DeleteChannelStreamScheduleSegmentResponse>> DeleteSegment(ITwitchClient client, UserId broadcasterId, StreamScheduleSegmentId segmentId, CancellationToken ct)
        => client.SendAsync(new DeleteChannelStreamScheduleSegmentRequest()
        {
            BroadcasterId = broadcasterId,
            SegmentId = segmentId
        }, ct);
}
