using TwitchySharp.Api.Authorization;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Authorization;

public class Test_UserInfoRequest(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    private readonly TestName TestName = new("user-info");

    [Fact]
    public async Task Send_UserInfoRequest_ReturnSuccessfulResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        UserInfoRequest stubRequest = new()
        {
            UserId = userConfig.UserId
        };

        ITwitchClient client = _fixture.GetTwitchApiClient();
        TwitchResponse<TwitchOidc> response = await client.SendAsync(stubRequest, TestContext.Current.CancellationToken);

        Assert.False(string.IsNullOrEmpty(response.Content.Sub));
        Assert.False(string.IsNullOrEmpty(response.Content.Iss));
        Assert.False(string.IsNullOrEmpty(response.Content.Aud));
        Assert.False(response.Content.Exp == default);
        Assert.False(response.Content.Iat == default);
    }
}
