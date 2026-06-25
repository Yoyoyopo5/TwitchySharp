using TwitchySharp.EventSub.Websocket.Functional;
using TwitchySharp.EventSub.Websocket.Idempotency;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Websocket.Tests.Unit.Idempotency;

public class Test_WithIdempotentMessages
{
    private readonly static ProcessWebsocketMessage MockProcess = (message, ct) => ValueTask.FromResult<Validation<EventSubWebsocketMessage>>(new EventSubWebsocketMessage()
    {
        Metadata = new()
        {
            MessageId = new("12345"),
            MessageTimestamp = DateTimeOffset.MinValue,
            MessageType = new("welcome")
        }
    });

    [Fact]
    public async Task ProcessWebsocketMessage_IsRepeated_ReturnsIdempotencyError()
    {
        ProcessWebsocketMessage mockProcess = MockProcess.WithIdempotentMessages((_, _) => ValueTask.FromResult(true));

        await mockProcess(new(), TestContext.Current.CancellationToken).MatchAsync(
            onError: (e, ct) => ValueTask.CompletedTask,
            onValid: (message, ct) => throw new InvalidOperationException("Process returned Validation (expected Error)."),
            CancellationToken.None
            );
    }

    [Fact]
    public async Task ProcessWebsocketMessage_IsNotRepeated_ReturnsNext()
    {
        ProcessWebsocketMessage mockProcess = MockProcess.WithIdempotentMessages((_, _) => ValueTask.FromResult(false));

        await mockProcess(new(), TestContext.Current.CancellationToken).MatchAsync(
            onError: (e, ct) => throw new InvalidOperationException("Process returned Error (expected Validation)."),
            onValid: (message, ct) => ValueTask.CompletedTask,
            CancellationToken.None
            );
    }
}
