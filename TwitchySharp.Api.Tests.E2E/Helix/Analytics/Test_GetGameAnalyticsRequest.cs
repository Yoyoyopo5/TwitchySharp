using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Analytics.Requests;
using TwitchySharp.Api.Helix.Analytics.Responses;

namespace TwitchySharp.Api.Tests.E2E.Helix.Analytics;
[Collection("helix")]
public class Test_GetGameAnalyticsRequest(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_GetGameAnalyticsRequest_ReturnSuccessResponse()
    {
        GetGameAnalyticsRequest stubRequest = new(_fixture.Secrets.Client.Id, _fixture.Secrets.User.AccessToken);

        GetGameAnalyticsResponse actualResponse = await _fixture.Api.SendRequestAsync(stubRequest);
    }
}
