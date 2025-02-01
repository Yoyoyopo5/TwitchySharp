using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization.Extensions;
using TwitchySharp.Api.Helix.Extensions;

namespace TwitchySharp.Api.Tests.Integration.Helix.Extensions;
[Collection("helix")]
public class Test_SetExtensionConfigurationSegment(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_SetExtensionConfigurationSegementRequest_ReturnSuccessResponse()
    {
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        string jwt = new ExtensionJwtPayload(broadcasterId)
            .Sign(_fixture.Secrets.Extension.Secret);

        await _fixture.Api.SendRequestAsync(new SetExtensionConfigurationSegmentRequest(
            _fixture.Secrets.Extension.Id,
            jwt,
            new SetExtensionConfigurationBroadcasterSegmentData()
            {
                BroadcasterId = broadcasterId,
                ExtensionId = _fixture.Secrets.Extension.Id,
                Content = "test configuration"
            }
            ));
    }
}
