using TwitchySharp.Api.Authentication;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Authentication;

public class Test_ValidateAccessTokenRequest(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    private readonly TestName TestName = new("validate-access-token");

    [Fact]
    public async Task Send_ValidateAccessTokenRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        ValidateAccessTokenRequest request = new()
        {
            UserId = userConfig.UserId
        };

        TestingTwitchClient client = _fixture.GetTwitchApiClient();
        TwitchResponse<ValidateAccessTokenResponseContent> response = await client.SendAsync(request, TestName, TestContext.Current.CancellationToken);

        Assert.True(response.Content.ExpiresIn > TimeSpan.Zero);
        Assert.False(string.IsNullOrEmpty(response.Content.Login));
        Assert.False(string.IsNullOrEmpty(response.Content.UserId.Value));
        Assert.False(string.IsNullOrEmpty(response.Content.ClientId.Value));
        Assert.NotEmpty(response.Content.Scopes);
    }
}
