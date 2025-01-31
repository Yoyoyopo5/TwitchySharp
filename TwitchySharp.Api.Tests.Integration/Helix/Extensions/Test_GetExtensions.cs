using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization.Extensions;
using TwitchySharp.Api.Helix.Extensions;

namespace TwitchySharp.Api.Tests.Integration.Helix.Extensions;
[Collection("helix")]
public class Test_GetExtensions(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_GetExtensionsRequest_ReturnSuccessResponse()
    {
        const string EXTENSION_VERSION = "0.0.1";
        string jwt = new ExtensionJwtPayload(await _fixture.GetUserIdFromAccessTokenAsync())
            .Sign(_fixture.Secrets.ExtensionSecret);

        await _fixture.Api.SendRequestAsync(new GetExtensionsRequest(
            _fixture.Secrets.ExtensionClientId,
            jwt,
            _fixture.Secrets.ExtensionClientId,
            EXTENSION_VERSION
            ));
    }
}
