using System.Net;
using Microsoft.AspNetCore.Http;

namespace TwitchySharp.Api.Tests.Integration.Client;

public class Test_ThrowTwitchApiException(TwitchApiIntegrationTestFixture fixture)
{
    private const string RESPONSE_BODY = "The requested content was not found.";

    [Fact]
    public async Task SendAsync_NotFoundResponse_ReturnsError()
    {
        const string TEST_PATH = "/notfound";
        using IDisposable endpoint = fixture.TestServer.Map(HttpMethod.Get, TEST_PATH, () => Results.NotFound(RESPONSE_BODY));

        StubTwitchRequest request = new(TEST_PATH);
        TwitchResponse<object> response = await fixture.TestServer.GetDefaultTwitchClient().SendAsync(request, TestContext.Current.CancellationToken);

        Assert.NotNull(response.Error);
    }
}
