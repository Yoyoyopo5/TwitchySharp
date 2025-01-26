using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Helix.EventSub;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Automod.Message;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Automod.Settings;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Automod.Terms;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.AdBreak;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.ChannelPoints;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.Chat;

namespace TwitchySharp.Api.Tests.Integration.Helix.EventSub;
public class Test_EventSubWebhooks(EventSubFixture fixture) : IClassFixture<EventSubFixture>
{
    private readonly EventSubFixture _fixture = fixture;

    [Theory]
    [ClassData(typeof(EventSubTestTypes))]
    public async void Send_WebhookEventSubCreateDeleteRequests_ReturnSuccessResponses(string subscriptionTypeName)
    {
        string appToken = (await _fixture.Api.SendRequestAsync(new ClientCredentialsRequest(
            _fixture.Secrets.ClientId,
            _fixture.Secrets.ClientSecret
            ))).AccessToken;

        EventSubSubscription testSubscription = (await _fixture.Api.SendRequestAsync(new CreateEventSubSubscriptionRequest(
            _fixture.Secrets.ClientId,
            appToken,
            new CreateEventSubSubscriptionRequestData
            {
                Type = _fixture.GetSubscriptionType(subscriptionTypeName),
                Transport = new WebhookSubscriptionTransport("https://fake-callback.xyz", "FAKE_SECRET")
            }
            ))).Data.Single();

        await _fixture.Api.SendRequestAsync(new DeleteEventSubSubscriptionRequest(
            _fixture.Secrets.ClientId,
            appToken,
            testSubscription.Id
            ));
    }
}
