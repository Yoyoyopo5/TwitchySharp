﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Channels;

namespace TwitchySharp.Api.Tests.Integration.Helix.Channels;
[Collection("helix")]
public class Test_GetFollowedChannels(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_GetFollowedChannelsRequest_ReturnSuccessResponse()
    {
        string userId = await _fixture.GetUserIdFromAccessTokenAsync();

        await _fixture.Api.SendRequestAsync(new GetFollowedChannelsRequest(
            _fixture.Secrets.ClientId,
            _fixture.Secrets.UserAccessToken,
            userId
            ));
    }
}
