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
[Collection("helix")]
public class Test_EventSub(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;
    private ValueTask<IReadOnlyDictionary<string, IEventSubSubscriptionType>> _subscriptionTypes = GenerateTypeMapAsync(fixture);

    [Theory]
    [MemberData(nameof(SubscriptionTypeNames))]
    public async void Send_WebsocketEventSubCreateDeleteRequests_ReturnSuccessResponses(string subscriptionTypeName)
    {
        // Jank as FUCK, please refactor me ASAP!
        using ClientWebSocket testWebsocket = new();
        CancellationTokenSource connectionCancellation = new();
        connectionCancellation.CancelAfter(5000);
        await testWebsocket.ConnectAsync(new Uri("wss://eventsub.wss.twitch.tv/ws"), connectionCancellation.Token);
        byte[] buffer = new byte[1024];
        CancellationTokenSource receiveCancellation = new();
        receiveCancellation.CancelAfter(5000);
        using MemoryStream outputStream = new(buffer.Length);
        string? sessionId = null;
        while (!receiveCancellation.IsCancellationRequested && sessionId is null)
        {
            ValueWebSocketReceiveResult receiveResult;
            do
            {
                receiveResult = await testWebsocket.ReceiveAsync((Memory<byte>)buffer, receiveCancellation.Token);
                outputStream.Write(buffer, 0, receiveResult.Count);
            } while (!receiveResult.EndOfMessage);

            if (receiveResult.MessageType == WebSocketMessageType.Close) break;
            outputStream.Position = 0;

            try
            {
                using JsonDocument receivedJson = await JsonDocument.ParseAsync(outputStream);
                sessionId = receivedJson.RootElement switch
                {
                    { ValueKind: JsonValueKind.Object } rootObject => rootObject.EnumerateObject().FirstOrDefault(property => property.Name == "payload") switch
                    {
                        { Value.ValueKind: JsonValueKind.Object } payload => payload.Value.EnumerateObject().FirstOrDefault(property => property.Name == "session") switch
                        {
                            { Value.ValueKind: JsonValueKind.Object } session => session.Value.EnumerateObject().FirstOrDefault(property => property.Name == "id") switch
                            {
                                { Value.ValueKind: JsonValueKind.String } id => id.Value.GetString()
                            },
                            _ => null
                        },
                        _ => null
                    },
                    _ => null
                };
            }
            catch (JsonException) { }
            await outputStream.FlushAsync();
        }

        EventSubSubscription testSubscription = (await _fixture.Api.SendRequestAsync(new CreateEventSubSubscriptionRequest(
            _fixture.Secrets.ClientId,
            _fixture.Secrets.UserAccessToken,
            new CreateEventSubSubscriptionRequestData
            {
                Type = await GetSubscriptionTypeAsync(subscriptionTypeName),
                Transport = new WebsocketSubscriptionTransport(sessionId!)
            }
            ))).Data.Single();

        await _fixture.Api.SendRequestAsync(new DeleteEventSubSubscriptionRequest(
            _fixture.Secrets.ClientId,
            _fixture.Secrets.UserAccessToken,
            testSubscription.Id
            ));
    }

    [Theory]
    [MemberData(nameof(SubscriptionTypeNames))]
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
                Type = await GetSubscriptionTypeAsync(subscriptionTypeName),
                Transport = new WebhookSubscriptionTransport("https://fake-callback.xyz", "FAKE_SECRET")
            }
            ))).Data.Single();

        await _fixture.Api.SendRequestAsync(new DeleteEventSubSubscriptionRequest(
            _fixture.Secrets.ClientId,
            appToken,
            testSubscription.Id
            ));
    }

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

    public static IEnumerable<object[]> SubscriptionTypeNames => new object[][]
    {
        [ nameof(AutomodMessageHoldV2) ]
    };

    private static async ValueTask<IReadOnlyDictionary<string, IEventSubSubscriptionType>> GenerateTypeMapAsync(HelixFixture fixture)
    {
        string broadcasterId = await fixture.GetUserIdFromAccessTokenAsync();

        return new Dictionary<string, IEventSubSubscriptionType>
        {
            { nameof(AutomodMessageHoldV2), new AutomodMessageHoldV2(broadcasterId, broadcasterId) }
        };
    }

    private async ValueTask<IEventSubSubscriptionType> GetSubscriptionTypeAsync(string subscriptionTypeName)
        => (await _subscriptionTypes)[subscriptionTypeName];
}
