using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Analytics;

namespace TwitchySharp.Api.Tests.Integration.Helix.Analytics;
[Collection("helix")]
public class Test_GetGameAnalyticsRequest(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_GetGameAnalyticsRequest_ReturnSuccessResponse()
    {
        GetGameAnalyticsRequest stubRequest = new(_fixture.Secrets.ClientId, _fixture.Secrets.UserAccessToken);

        GetGameAnalyticsResponse actualResponse = await _fixture.Api.SendRequestAsync(stubRequest);
    }
}
