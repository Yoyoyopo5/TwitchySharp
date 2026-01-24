using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization.Requests;
using TwitchySharp.Api.Authorization.Responses;

namespace TwitchySharp.Api.Tests.E2E.Authorization;
[Collection("authorization")]
public class Test_RevokeAccessTokenRequest(AuthorizationFixture fixture)
{
    private readonly AuthorizationFixture _fixture = fixture;
    [Fact]
    public async void Send_RevokeAccessTokenRequest_ReturnSuccessResponse()
    {
        // Make sure to set the user access token to a valid token first.
        RevokeAccessTokenRequest stubRequest = new(_fixture.Secrets.Client.Id, _fixture.Secrets.User.AccessToken);

        RevokeAccessTokenResponse actualResponse = await _fixture.Api.SendRequestAsync(stubRequest);
    }
}
