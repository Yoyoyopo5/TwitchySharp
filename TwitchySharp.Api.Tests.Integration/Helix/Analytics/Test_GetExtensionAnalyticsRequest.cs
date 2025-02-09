using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Helix.Analytics;

namespace TwitchySharp.Api.Tests.Integration.Helix.Analytics;
[Collection("helix")]
public class Test_GetExtensionAnalyticsRequest(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_GetExtensionAnalyticsRequest_ReturnSuccessResponse()
    {
        GetExtensionAnalyticsRequest stubRequest = new(_fixture.Secrets.Client.Id, _fixture.Secrets.User.AccessToken);

        GetExtensionAnalyticsResponse actualResponse = await _fixture.Api.SendRequestAsync(stubRequest);
    }
}
