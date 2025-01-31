using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Tests.Integration.Authorization;
[Collection("authorization")]
public class Test_AccessTokenRefreshRequest
{
    private readonly AuthorizationFixture _fixture;
    public Test_AccessTokenRefreshRequest(AuthorizationFixture fixture)
        => _fixture = fixture;

    [Fact]
    public async void Send_AccessTokenRefreshRequest_ReturnSuccessResponse()
    {
        // You can use https://twitchtokengenerator.com/ to get an access token and refresh token.
        // Just make sure to use the same client id and secret as in the fixture.
        string stubRefreshToken = _fixture.Secrets.User.RefreshToken;
        AccessTokenRefreshRequest stubRequest = new(_fixture.Secrets.Client.Id, _fixture.Secrets.Client.Secret, stubRefreshToken);

        AccessTokenRefreshResponse actual = await _fixture.Api.SendRequestAsync(stubRequest);
    }
}
