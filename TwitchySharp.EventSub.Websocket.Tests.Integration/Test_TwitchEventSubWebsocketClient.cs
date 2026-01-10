using Microsoft.AspNetCore.Hosting.Server.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Websocket.Client;
using Xunit;

namespace TwitchySharp.EventSub.Websocket.Tests.Integration;

public class Test_TwitchEventSubWebsocketClient(WebsocketFixture fixture) : IClassFixture<WebsocketFixture>
{
    private readonly WebsocketFixture _fixture = fixture;
    [Fact]
    public async Task ProcessWelcomeMessage_ValidWelcomeMessage_ReturnValidSession()
    {
        const string MOCK_SESSION_ID = "12345";

        TwitchEventSubWebsocketClient client = _fixture.Client;
        await client.StartAsync().WaitAsync(TimeSpan.FromSeconds(5));

        WebSocket server = await _fixture.ServerWebSocket.WaitAsync(TimeSpan.FromSeconds(1));
        await server.SendWelcomeMessage(MOCK_SESSION_ID);
        await Task.Delay(TimeSpan.FromMilliseconds(10)); // Wait for message to receive.

        await client.StopAsync();

        Assert.NotNull(_fixture.Handler.Session);
        Assert.Equal(MOCK_SESSION_ID, _fixture.Handler.Session.Id);
    }
}
