using System.Net;
using TwitchySharp.Api.Helix.EventSub;
using TwitchySharp.Api.Tests.Integration.Fixtures;

namespace TwitchySharp.Api.Tests.Integration.Tests;

/// <summary>
/// Tests for TwitchApiException population and error handling.
/// </summary>
public class Test_TwitchApiException(TwitchApiTestFixture fixture) : IClassFixture<TwitchApiTestFixture>
{
    private readonly TwitchApiTestFixture _fixture = fixture;

    [Fact]
    public async Task Send_InvalidBearerToken_ExceptionHasExpectedData()
    {
        ITwitchClient client = _fixture.CreateTwitchClient();
        DeleteEventSubSubscriptionRequest request = new()
        {
            SubscriptionId = new("1234"),
            AuthorizationContext = new()
            {
                Identity = TwitchIdentity.Client.Default,
                AccessToken = new AppAccessToken("invalid_token")
            }
        };

        TwitchApiException ex = await Assert.ThrowsAsync<TwitchApiException>(() => client.SendAsync(request, TestContext.Current.CancellationToken));

        Assert.Equal(HttpStatusCode.Unauthorized, ex.StatusCode);
        Assert.Equal(request, ex.Request);
    }
}
