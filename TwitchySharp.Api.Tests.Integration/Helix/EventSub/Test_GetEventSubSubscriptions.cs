﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Helix.EventSub;

namespace TwitchySharp.Api.Tests.Integration.Helix.EventSub;
[Collection("helix")]
public class Test_GetEventSubSubscriptions(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_GetEventSubSubscriptionsRequest_ReturnSuccessResponse()
    {
        string appToken = (await _fixture.Api.SendRequestAsync(new ClientCredentialsRequest(_fixture.Secrets.ClientId, _fixture.Secrets.ClientSecret))).AccessToken;

        // webhooks
        await _fixture.Api.SendRequestAsync(new GetEventSubSubscriptionsRequest(
            _fixture.Secrets.ClientId,
            appToken
            ));

        // websockets
        await _fixture.Api.SendRequestAsync(new GetEventSubSubscriptionsRequest(
            _fixture.Secrets.ClientId,
            _fixture.Secrets.UserAccessToken
            ));
    }
}
