using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Schedule;

namespace TwitchySharp.Api.Tests.Integration.Helix.Schedule;
[Collection("helix")]
public class Test_GetChannelICalendar(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_GetChannelICalendarRequest_ReturnSuccessResponse()
    {
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        await _fixture.Api.SendRequestAsync(new GetChannelICalendarRequest(broadcasterId));
    }
}
