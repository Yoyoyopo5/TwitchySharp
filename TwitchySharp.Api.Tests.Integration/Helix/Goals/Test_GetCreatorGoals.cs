using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Goals;

namespace TwitchySharp.Api.Tests.Integration.Helix.Goals;
[Collection("helix")]
public class Test_GetCreatorGoals(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_GetCreatorGoalsRequest_ReturnSuccessResponse()
    {
        await _fixture.Api.SendRequestAsync(new GetCreatorGoalsRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            await _fixture.GetUserIdFromAccessTokenAsync()
            ));
    }
}
