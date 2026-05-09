using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Tests.E2E.Tests.Authorization;

[Collection("twitch")]
public class Test_ValidateAccessTokenRequest(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_ValidateAccessTokenRequest_ReturnSuccessResponse()
    {
        ValidateAccessTokenRequest request = new()
        {
            UserId = _fixture.UserIdentity.UserId
        };

        var response = await TwitchClientFixture.Client.SendAsync(request, TestContext.Current.CancellationToken);

        Assert.True(response.Content.ExpiresIn > TimeSpan.Zero);
        Assert.False(string.IsNullOrEmpty(response.Content.Login));
        Assert.False(string.IsNullOrEmpty(response.Content.UserId.Value));
        Assert.False(string.IsNullOrEmpty(response.Content.ClientId.Value));
        Assert.NotEmpty(response.Content.Scopes);
    }
}
