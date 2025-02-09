using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Schedule;

namespace TwitchySharp.Api.Tests.Integration.Helix.Schedule;
[Collection("helix")]
public class Test_GetChannelStreamSchedule(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_GetChannelStreamScheduleRequest_ReturnSuccessResponse()
    {
        const string broadcasterId = "52137752";

        await _fixture.Api.SendRequestAsync(new GetChannelStreamScheduleRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId
            ));
    }
}
