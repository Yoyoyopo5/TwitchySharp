using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Models.Authorization.Models;
using TwitchySharp.Api.Models.Authorization.Requests;

namespace TwitchySharp.Api.Tests.Integration.Authorization;
[Collection("authorization")]
public class Test_UserInfoRequest(AuthorizationFixture fixture)
{
    private readonly AuthorizationFixture _fixture = fixture;

    [Fact]
    public async void Send_UserInfoRequest_ReturnSuccessfulResponse()
    {
        UserInfoRequest stubRequest = new(_fixture.Secrets.User.AccessToken);

        TwitchOidc actualResponse = await _fixture.Api.SendRequestAsync(stubRequest);
    }
}
