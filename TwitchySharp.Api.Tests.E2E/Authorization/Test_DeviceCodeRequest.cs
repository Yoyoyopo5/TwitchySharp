using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Authorization.Requests;
using TwitchySharp.Api.Authorization.Responses;

namespace TwitchySharp.Api.Tests.E2E.Authorization;
[Collection("authorization")]
public class Test_DeviceCodeRequest
{
    private AuthorizationFixture _fixture;
    public Test_DeviceCodeRequest(AuthorizationFixture fixture)
        => _fixture = fixture;

    [Fact]
    public async void Send_DeviceCodeRequest_ReturnSuccessResponse()
    {
        Scope[] stubScopes = [];
        DeviceCodeRequest stubRequest = new(_fixture.Secrets.Client.Id, stubScopes);

        DeviceCodeResponse actualResponse = await _fixture.Api.SendRequestAsync(stubRequest);
    }
}
