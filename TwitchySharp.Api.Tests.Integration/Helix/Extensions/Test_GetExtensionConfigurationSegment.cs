﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization.Extensions;
using TwitchySharp.Api.Helix.Extensions;

namespace TwitchySharp.Api.Tests.Integration.Helix.Extensions;
[Collection("helix")]
public class Test_GetExtensionConfigurationSegment(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_GetExtensionConfigurationSegmentRequest_ReturnSuccessResponse()
    {
        string jwt = new ExtensionJwtPayload(await _fixture.GetUserIdFromAccessTokenAsync())
            .Sign(_fixture.Secrets.ExtensionSecret);

        await _fixture.Api.SendRequestAsync(new GetExtensionConfigurationSegmentRequest(
            _fixture.Secrets.ExtensionClientId,
            jwt,
            _fixture.Secrets.ExtensionClientId,
            [ ExtensionConfigurationSegmentType.Global ]
            ));
    }
}
