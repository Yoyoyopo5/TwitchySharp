using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Tests.Integration.Authorization;
[Collection("authorization")]
public class Test_DeviceCodeTokenRequest(AuthorizationFixture fixture)
{
    private readonly AuthorizationFixture _fixture = fixture;

    [Fact]
    public async void Send_DeviceCodeTokenRequest_ReturnSuccessResponse()
    {
        // Use the device code generated from DeviceCodeRequest.
        // You also have to visit the VerficationUri in that response to validate the device code before this test will succeed.
        Scope[] stubScopes = [];
        DeviceCodeTokenRequest stubRequest = new(_fixture.Secrets.ClientId, stubScopes, _fixture.Secrets.DeviceAuthorizationCode);

        DeviceCodeTokenResponse actualResponse = await _fixture.Api.SendRequestAsync(stubRequest);
    }
}
