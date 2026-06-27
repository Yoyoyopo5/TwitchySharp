using TwitchySharp.EventSub.Webhooks.Functional;
using TwitchySharp.EventSub.Webhooks.Idempotency;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Webhooks.Tests.Unit.Idempotency;

public class Test_WithIdempotentRequests
{
    [Fact]
    public async Task ProcessWebhookRequest_WithIdempotentRequests_RepeatedRequest_ReturnsIdempotencyError()
    {
        ProcessWebhookRequest process = ProcessStubs.StubProcess.WithIdempotentRequests(
            (messageId, ct) => ValueTask.FromResult(true)
            );

        await process(ProcessStubs.CreateFakeRequest(), TestContext.Current.CancellationToken)
            .MatchAsync(
            onError: (e, _) =>
            {
                Assert.IsType<IdempotencyError>(e);
                return ValueTask.CompletedTask;
            },
            onValid: (_, _) => throw new NotSupportedException("Process returned Validation (expected Error)."),
            CancellationToken.None
            );
    }

    [Fact]
    public async Task ProcessWebhookRequest_WithIdempotentRequests_UniqueRequests_ReturnsNext()
    {
        ProcessWebhookRequest process = ProcessStubs.StubProcess.WithIdempotentRequests(
            (messageId, ct) => ValueTask.FromResult(false)
            );

        await process(ProcessStubs.CreateFakeRequest(), TestContext.Current.CancellationToken)
            .MatchAsync(
            onError: (e, _) => throw new NotSupportedException("Process returned Error (expected Validation)."),
            onValid: (_, _) => ValueTask.CompletedTask,
            CancellationToken.None
            );
    }
}
