using TwitchySharp.Api.Helix.Schedule;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Schedule;

public class Test_ChannelStreamScheduleSegments(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("channel-stream-schedule-segments");

    [Fact]
    public async Task Send_ChannelStreamScheduleSegmentRequests_ReturnSuccessResponses()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        const string TEST_SEGMENT_TITLE = "Test Stream";

        UserId broadcasterId = userConfig.UserId;
        TestingTwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        TwitchResponse<CreateChannelStreamScheduleSegmentResponseContent> createResponse
            = await CreateSegment(client, broadcasterId, TEST_SEGMENT_TITLE, ct);
        ChannelStreamScheduleSegment segment = createResponse.Content.Data.Segments.Where(s => s.Title == TEST_SEGMENT_TITLE).First();

        await UpdateSegment(client, broadcasterId, segment.Id, ct);
        await DeleteSegment(client, broadcasterId, segment.Id, ct);
    }

    private static Task<TwitchResponse<CreateChannelStreamScheduleSegmentResponseContent>> CreateSegment(TestingTwitchClient client, UserId broadcasterId, string title, CancellationToken ct)
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
        }, TestName, ct);

    private static Task<TwitchResponse<UpdateChannelStreamScheduleSegmentResponseContent>> UpdateSegment(TestingTwitchClient client, UserId broadcasterId, StreamScheduleSegmentId segmentId, CancellationToken ct)
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
        }, TestName, ct);

    private static Task<TwitchResponse<DeleteChannelStreamScheduleSegmentResponseContent>> DeleteSegment(TestingTwitchClient client, UserId broadcasterId, StreamScheduleSegmentId segmentId, CancellationToken ct)
        => client.SendAsync(new DeleteChannelStreamScheduleSegmentRequest()
        {
            BroadcasterId = broadcasterId,
            SegmentId = segmentId
        }, TestName, ct);
}
