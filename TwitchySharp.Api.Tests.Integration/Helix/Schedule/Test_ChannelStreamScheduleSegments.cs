using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Schedule;

namespace TwitchySharp.Api.Tests.Integration.Helix.Schedule;
[Collection("helix")]
public class Test_ChannelStreamScheduleSegments(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_ChannelStreamScheduleSegmentRequests_ReturnSuccessResponses()
    {
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        string segementId = (await CreateSegment(broadcasterId)).Data.Segments.Single().Id;
        await UpdateSegment(broadcasterId, segementId);
        await DeleteSegment(broadcasterId, segementId);
    }

    private ValueTask<CreateChannelStreamScheduleSegmentResponse> CreateSegment(string broadcasterId)
        => _fixture.Api.SendRequestAsync(new CreateChannelStreamScheduleSegmentRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            new CreateChannelStreamScheduleSegmentRequestData()
            {
                Timezone = "America/Chicago",
                StartTime = DateTimeOffset.UtcNow.AddHours(2),
                Duration = 60,
                Title = "Test Stream",
                IsRecurring = false
            }
            ));

    private ValueTask<UpdateChannelStreamScheduleSegmentResponse> UpdateSegment(string broadcasterId, string segmentId)
        => _fixture.Api.SendRequestAsync(new UpdateChannelStreamScheduleSegmentRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            segmentId,
            new UpdateChannelStreamScheduleSegmentRequestData()
            {
                Duration = 70
            }
            ));

    private ValueTask<DeleteChannelStreamScheduleSegmentResponse> DeleteSegment(string broadcasterId, string segmentId)
        => _fixture.Api.SendRequestAsync(new DeleteChannelStreamScheduleSegmentRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            segmentId
            ));
}
