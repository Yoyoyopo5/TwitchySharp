using TwitchySharp.Api.Helix.Analytics;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Analytics;

[Collection("twitch")]
public class Test_GetExtensionAnalyticsRequest(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetExtensionAnalyticsRequest_ReturnSuccessResponse()
    {
        // We need an account with extension analytics to fully test this endpoint.
        GetExtensionAnalyticsRequest request = new()
        {
            UserId = _fixture.UserIdentity.UserId
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
