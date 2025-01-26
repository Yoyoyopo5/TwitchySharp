using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.EventSub;

namespace TwitchySharp.Api.Tests.Integration.Helix.EventSub;

public class Test_EventSubWebsockets(EventSubWebSocketsFixture fixture) : IClassFixture<EventSubWebSocketsFixture>
{
    private readonly EventSubWebSocketsFixture _fixture = fixture;

    [Theory]
    [ClassData(typeof(EventSubTestTypes))]
    public async void Send_WebsocketEventSubCreateDeleteRequests_ReturnSuccessResponses(string subscriptionTypeName)
    {
        EventSubSubscription testSubscription = (await _fixture.Api.SendRequestAsync(new CreateEventSubSubscriptionRequest(
            _fixture.Secrets.ClientId,
            _fixture.Secrets.UserAccessToken,
            new CreateEventSubSubscriptionRequestData
            {
                Type = _fixture.GetSubscriptionType(subscriptionTypeName),
                Transport = new WebsocketSubscriptionTransport(_fixture.SessionId)
            }
            ))).Data.Single();

        await _fixture.Api.SendRequestAsync(new DeleteEventSubSubscriptionRequest(
            _fixture.Secrets.ClientId,
            _fixture.Secrets.UserAccessToken,
            testSubscription.Id
            ));
    }

}
