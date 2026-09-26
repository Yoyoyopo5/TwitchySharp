using Microsoft.AspNetCore.Http;

namespace TwitchySharp.Api.Tests.Integration.Client;

public class Test_DefaultHttpClient(TwitchApiIntegrationTestFixture fixture)
{
    [Fact]
    public async Task SendAsync_WithDefaultTwitchClient_ReturnResponse()
    {
        const string TEST_PATH = "/test/http_client";
        using IDisposable endpoint = fixture.TestServer.Map(HttpMethod.Get, TEST_PATH, () => Results.Ok());
        await fixture.TestServer.GetDefaultTwitchClient().SendAsync(new StubTwitchRequest(TEST_PATH), TestContext.Current.CancellationToken);
    }
}
