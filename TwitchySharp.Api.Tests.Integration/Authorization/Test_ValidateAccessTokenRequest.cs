using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Tests.Integration.Authorization;
[Collection("authorization")]
public class Test_ValidateAccessTokenRequest(AuthorizationFixture fixture)
{
    private readonly AuthorizationFixture _fixture = fixture;

    [Fact]
    public async void Send_ValidateAccessTokenRequest_ReturnSuccessResponse()
    {
        ValidateAccessTokenRequest stubRequest = new(_fixture.Secrets.User.AccessToken);

        ValidateAccessTokenResponse actualResponse = await _fixture.Api.SendRequestAsync(stubRequest);
    }
}
