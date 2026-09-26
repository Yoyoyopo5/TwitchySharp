using System.Net;
using Microsoft.AspNetCore.Http;

namespace TwitchySharp.Api.Tests.Integration.Client;

public class Test_ThrowTwitchApiException(TwitchApiIntegrationTestFixture fixture)
{
    private const string RESPONSE_BODY = "The requested content was not found.";

    [Fact]
    public async Task SendAsync_NotFoundResponse_ExceptionThrownWithExpectedData()
    {
        const string TEST_PATH = "/notfound";
        using IDisposable endpoint = fixture.TestServer.Map(HttpMethod.Get, TEST_PATH, () => Results.NotFound(RESPONSE_BODY));

        StubTwitchRequest request = new(TEST_PATH);
        TwitchApiException ex = await Assert.ThrowsAsync<TwitchApiException>(() => fixture.TestServer.GetDefaultTwitchClient().SendAsync(request, TestContext.Current.CancellationToken));

        Assert.Equal(HttpStatusCode.NotFound, ex.StatusCode);
        Assert.Equal(request, ex.Request);
        Assert.Equal($"\"{RESPONSE_BODY}\"", ex.Content);
    }
}
