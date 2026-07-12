using Xunit;

namespace TwitchySharp.Tests.E2E;

public static class TestContextExtensions
{
    public static ITestContext AddSkippedEndpointWarning(this ITestContext ctx, EndpointName endpointName)
    {
        ctx.AddWarning($"No user configuration found for endpoint {endpointName}. Skipping test.");
        return ctx;
    }
}
