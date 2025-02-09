using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Schedule;

namespace TwitchySharp.Api.Tests.Integration.Helix.Schedule;
[Collection("helix")]
public class Test_UpdateChannelStreamSchedule(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_UpdateChannelStreamScheduleRequest_ReturnSuccessResponse()
    {
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        await _fixture.Api.SendRequestAsync(new UpdateChannelStreamScheduleRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            new UpdateChannelStreamScheduleRequestData()
                .EnableVacationMode(DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1), "America/Chicago")
            ));
    }
}
