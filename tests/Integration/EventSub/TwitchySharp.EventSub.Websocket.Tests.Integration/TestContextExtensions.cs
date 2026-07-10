namespace TwitchySharp.EventSub.Websocket.Tests.Integration;

public static class TestContextExtensions
{
    public static CancellationToken CreateLinkedCancellationToken(this ITestContext testContext, TimeSpan? cancelAfter = null)
    {
        CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(testContext.CancellationToken);
        if (cancelAfter.HasValue)
            cts.CancelAfter(cancelAfter.Value);
        return cts.Token;
    }
}
