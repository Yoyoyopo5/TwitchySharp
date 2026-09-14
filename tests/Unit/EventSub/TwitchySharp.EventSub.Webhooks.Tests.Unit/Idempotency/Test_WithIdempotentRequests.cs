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
            onError: e =>
            {
                Assert.IsType<IdempotencyError>(e);
                return ValueTask.CompletedTask;
            },
            onValid: _ => throw new NotSupportedException("Process returned Validation (expected Error).")
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
            onError: e => throw new NotSupportedException("Process returned Error (expected Validation)."),
            onValid: _ => ValueTask.CompletedTask
            );
    }
}
