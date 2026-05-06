using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Tests.E2E.Tests.Authorization;

[Collection("twitch")]
public class Test_UserInfoRequest(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_UserInfoRequest_ReturnSuccessfulResponse()
    {
        UserInfoRequest stubRequest = new()
        {
            UserId = _fixture.UserIdentity.UserId
        };

        var response = await _fixture.CreateClient().SendAsync(stubRequest, TestContext.Current.CancellationToken);

        Assert.False(string.IsNullOrEmpty(response.Content.Sub));
        Assert.False(string.IsNullOrEmpty(response.Content.Iss));
        Assert.False(string.IsNullOrEmpty(response.Content.Aud));
        Assert.False(response.Content.Exp == default);
        Assert.False(response.Content.Iat == default);
    }
}
