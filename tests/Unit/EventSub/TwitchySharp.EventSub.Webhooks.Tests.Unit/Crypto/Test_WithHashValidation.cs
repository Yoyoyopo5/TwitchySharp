using TwitchySharp.EventSub.Webhooks.Crypto;
using TwitchySharp.EventSub.Webhooks.Functional;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Webhooks.Tests.Unit.Crypto;

public class Test_WithHashValidation
{
    [Fact]
    public async Task ProcessWebhookRequest_WithHashValidation_VerifyAndNextGetSameBytes()
    {
        string verifyInputBody = string.Empty;

        ProcessWebhookRequest process = ProcessStubs.StubProcess.WithHashValidation((_, request, ct) =>
        {
            using StreamReader sr = new(request.Content);
            verifyInputBody = sr.ReadToEnd();
            return ValueTask.FromResult(new Validation());
        });

        await process(ProcessStubs.CreateFakeRequest(), TestContext.Current.CancellationToken)
            .MatchAsync(
            onError: (_, _) => throw new NotImplementedException(),
            onValid: (result, _) =>
            {
                Assert.Equal(((FakeWebhookRequestContent)result).Body, verifyInputBody);
                return ValueTask.CompletedTask;
            },
            CancellationToken.None
            );
    }

    [Fact]
    public async Task ProcessWebhookRequest_WithHashValidationError_ReturnsError()
    {
        ProcessWebhookRequest process = ProcessStubs.StubProcess.WithHashValidation((_, request, ct) => ValueTask.FromResult<Validation>(new Error()));

        await process(ProcessStubs.CreateFakeRequest(), TestContext.Current.CancellationToken)
            .MatchAsync(
            onError: (e, _) => ValueTask.CompletedTask,
            onValid: (result, _) => throw new Exception("Verify hash returned Validation (expected Error)."),
            CancellationToken.None
            );
    }

    [Fact]
    public async Task ProcessWebhookRequest_WithHashValidationSuccess_ReturnsNext()
    {
        ProcessWebhookRequest process = ProcessStubs.StubProcess.WithHashValidation((_, request, ct) => ValueTask.FromResult(new Validation()));

        await process(ProcessStubs.CreateFakeRequest(), TestContext.Current.CancellationToken)
            .MatchAsync(
            onError: (e, _) => throw new Exception("Verify hash returned Error (expected Validation)."),
            onValid: (result, _) => ValueTask.CompletedTask,
            CancellationToken.None
            );
    }
}
