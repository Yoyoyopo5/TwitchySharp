using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Tests.Integration.Authorization;
[Collection("authorization")]
public class Test_OidcJwkRequest(AuthorizationFixture fixture)
{
    private readonly AuthorizationFixture _fixture = fixture;

    [Fact]
    public async void Send_OidcJwtRequest_ReturnSuccessResponse()
    {
        OidcJwkRequest stubRequest = new();

        OidcJwkResponse actualResponse = await _fixture.Api.SendRequestAsync(stubRequest);
    }
}
