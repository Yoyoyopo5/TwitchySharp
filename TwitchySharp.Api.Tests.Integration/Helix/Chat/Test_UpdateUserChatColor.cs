﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Chat;

namespace TwitchySharp.Api.Tests.Integration.Helix.Chat;
[Collection("helix")]
public class Test_UpdateUserChatColor(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_UpdateUserChatColor_ReturnSuccessResponse()
    {
        string userId = await _fixture.GetUserIdFromAccessTokenAsync();

        await _fixture.Api.SendRequestAsync(new UpdateUserChatColorRequest(
            _fixture.Secrets.ClientId,
            _fixture.Secrets.UserAccessToken,
            userId,
            ChatColor.Blue
            ));
    }
}
