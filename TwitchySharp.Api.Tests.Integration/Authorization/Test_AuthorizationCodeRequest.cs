﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Tests.Integration.Authorization;
[Collection("authorization")]
public class Test_AuthorizationCodeRequest
{
    private AuthorizationFixture _fixture;
    public Test_AuthorizationCodeRequest(AuthorizationFixture fixture)
        => _fixture = fixture;

    [Fact]
    public async void Send_AuthorizationCodeRequest_ReturnSuccessResponse()
    {
        // Visit to get code: https://id.twitch.tv/oauth2/authorize?response_type=code&client_id=8dhvxswu6888igrgh2kz47hkke50db&redirect_uri=http://localhost:5000&scope=&force_verify=true&state=123
        string stubCode = System.Environment.GetEnvironmentVariable("TWITCHYSHARP_TEST_AUTHORIZATION_CODE", EnvironmentVariableTarget.User)!;
        AuthorizationCodeRequest stubRequest = new(_fixture.ClientId, _fixture.ClientSecret, stubCode, _fixture.RedirectUrl);

        AuthorizationCodeResponse actualResponse = await _fixture.Api.SendRequestAsync(stubRequest);
    }
}
