using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization.Extensions;
using TwitchySharp.Api.Helix.Extensions;

namespace TwitchySharp.Api.Tests.Integration.Helix.Extensions;
[Collection("helix")]
public class Test_CreateExtensionSecret(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_CreateExtensionSecretRequest_ReturnSuccessResponse()
    {
        // can't figure this one out, keep getting a 401 Authorization refused.
        // I know the JWT is valid and everything else looks fine.
        // Something strange with this endpoint

        string jwt = new ExtensionJwtPayload(await _fixture.GetUserIdFromAccessTokenAsync())
            .Sign(_fixture.Secrets.Extension.Secret);

        await _fixture.Api.SendRequestAsync(new CreateExtensionSecretRequest(
            _fixture.Secrets.Extension.Id,
            jwt,
            _fixture.Secrets.Extension.Id
            ));
    }
}
