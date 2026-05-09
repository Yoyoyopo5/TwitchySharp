using TwitchySharp.Api.Helix.Analytics;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Analytics;

[Collection("twitch")]
public class Test_GetGameAnalyticsRequest(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetGameAnalyticsRequest_ReturnSuccessResponse()
    {
        // We need an account with game analytics to fully test this endpoint.
        GetGameAnalyticsRequest request = new()
        {
            UserId = _fixture.UserIdentity.UserId
        };

        await TwitchClientFixture.Client.SendAsync(request, TestContext.Current.CancellationToken);
    }
}
