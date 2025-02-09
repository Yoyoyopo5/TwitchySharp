using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Tests.Integration.Authorization;
[Collection("authorization")]
public class Test_ClientCredentialsRequest
{
    private AuthorizationFixture _fixture;
    public Test_ClientCredentialsRequest(AuthorizationFixture fixture)
        => _fixture = fixture;

    [Fact]
    public async void Send_ClientCredentialsRequest_ReturnSuccessResponse()
    {
        ClientCredentialsRequest stubRequest = new(_fixture.Secrets.Client.Id, _fixture.Secrets.Client.Secret);

        ClientCredentialsResponse actualResponse = await _fixture.Api.SendRequestAsync(stubRequest);
    }
}
