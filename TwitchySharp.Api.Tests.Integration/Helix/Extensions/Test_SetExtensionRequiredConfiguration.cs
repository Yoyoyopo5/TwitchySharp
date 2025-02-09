using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization.Extensions;
using TwitchySharp.Api.Helix.Extensions;

namespace TwitchySharp.Api.Tests.Integration.Helix.Extensions;
[Collection("helix")]
public class Test_SetExtensionRequiredConfiguration(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_SetExtensionRequiredConfigurationRequest_ReturnSuccessRepsonse()
    {
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();
        string jwt = new ExtensionJwtPayload(broadcasterId)
            .Sign(_fixture.Secrets.Extension.Secret);

        await _fixture.Api.SendRequestAsync(new SetExtensionRequiredConfigurationRequest(
            _fixture.Secrets.Extension.Id,
            jwt,
            broadcasterId,
            new SetExtensionRequiredConfigurationRequestData()
            {
                ExtensionId = _fixture.Secrets.Extension.Id,
                ExtensionVersion = _fixture.Secrets.Extension.Version,
                RequiredConfiguration = "test config"
            }
            ));
    }
}
