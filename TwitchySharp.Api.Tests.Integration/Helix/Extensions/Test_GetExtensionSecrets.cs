using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization.Extensions;
using TwitchySharp.Api.Helix.Extensions;

namespace TwitchySharp.Api.Tests.Integration.Helix.Extensions;
[Collection("helix")]
public class Test_GetExtensionSecrets(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_GetExtensionSecretsRequest_ReturnSuccessResponse()
    {
        string jwt = new ExtensionJwtPayload(await _fixture.GetUserIdFromAccessTokenAsync())
            .Sign(_fixture.Secrets.Extension.Secret);

        await _fixture.Api.SendRequestAsync(new GetExtensionSecretsRequest(
            _fixture.Secrets.Extension.Id,
            jwt,
            _fixture.Secrets.Extension.Id
            ));
    }
}
