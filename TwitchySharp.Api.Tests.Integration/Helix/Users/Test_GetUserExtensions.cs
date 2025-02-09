using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Users;

namespace TwitchySharp.Api.Tests.Integration.Helix.Users;
[Collection("helix")]
public class Test_GetUserExtensions(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_GetUserExtensionsRequest_ReturnSuccessResponse()
    {
        await _fixture.Api.SendRequestAsync(new GetUserExtensionsRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken
            ));
    }
}
