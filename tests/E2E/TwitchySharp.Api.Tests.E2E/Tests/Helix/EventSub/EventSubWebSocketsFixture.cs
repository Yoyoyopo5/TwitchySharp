using System.Diagnostics;
using System.Net.WebSockets;
using System.Text.Json;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.EventSub;

/// <summary>
/// Creates a websocket connection to Twitch EventSub servers and holds it open for the duration of the test.
/// </summary>
public sealed class EventSubWebSocketsFixture : TwitchClientFixture, IDisposable, IAsyncLifetime
{
    public EventSubWebsocketSessionId SessionId { get; private set; } = new();
    private readonly ClientWebSocket _ws = new();

    private static async ValueTask<string> GetWebsocketSessionId(ClientWebSocket ws)
    {
        const int TIMEOUT_MS = 5000;
        TimeSpan timeout = TimeSpan.FromMilliseconds(TIMEOUT_MS);

        const string TWITCH_EVENTSUB_WEBSOCKET_URI = "wss://eventsub.wss.twitch.tv/ws";

        CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(TestContext.Current.CancellationToken);
        cts.CancelAfter(timeout);

        await ws.ConnectAsync(new Uri(TWITCH_EVENTSUB_WEBSOCKET_URI), cts.Token);

        while (!cts.IsCancellationRequested)
        {
            using MemoryStream messageStream = new();
            ValueWebSocketReceiveResult receiveResult = await ws.ReceiveAsync(messageStream, cts.Token);
            if (receiveResult.MessageType == WebSocketMessageType.Close) break;
            messageStream.Position = 0;
            try
            {
                using JsonDocument jsonMessage = await JsonDocument.ParseAsync(messageStream, default, cts.Token);
                if (GetIdOrDefault(jsonMessage.RootElement) is string id) return id;
            }
            catch { }
        }
        throw new TaskCanceledException();
    }

    private static string? GetIdOrDefault(JsonElement json)
        => json.EnumerateObject().First(property => property.NameEquals("payload")).Value
                .EnumerateObject().FirstOrDefault(property => property.NameEquals("session")).Value switch
        {
            { ValueKind: JsonValueKind.Object } session => session.EnumerateObject().First(property => property.NameEquals("id")).Value.GetString(),
            _ => null
        };

    public void Dispose() => _ws.Dispose();

    public async ValueTask InitializeAsync()
    {
        SessionId = new(await GetWebsocketSessionId(_ws));
    }

    public ValueTask DisposeAsync()
    {
        Dispose();
        return ValueTask.CompletedTask;
    }
}

public static class ClientWebSocketExtensions
{
    public static async ValueTask<ValueWebSocketReceiveResult> ReceiveAsync(this ClientWebSocket ws, Stream outputStream, CancellationToken ct)
    {
        Debug.Assert(ws.State == WebSocketState.Open);

        byte[] receiveBuffer = new byte[1024];
        return await ws.ReceiveMessage(outputStream, receiveBuffer, ct);
    }

    private static async ValueTask<ValueWebSocketReceiveResult> ReceiveMessage(this ClientWebSocket ws, Stream outputStream, byte[] buffer, CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            ValueWebSocketReceiveResult receiveResult = await ws.ReceiveAsync((Memory<byte>)buffer, ct);
            outputStream.Write(buffer, 0, receiveResult.Count);
            if (receiveResult.EndOfMessage) return receiveResult;
        }
        throw new OperationCanceledException();
    }
}
