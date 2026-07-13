using TwitchySharp.Api.Authorization;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Authorization;

public class Test_ValidateAccessTokenRequest(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    private readonly EndpointName _endpointName = new("validate-access-token");

    [Fact]
    public async Task Send_ValidateAccessTokenRequest_ReturnSuccessResponse()
    {
        if (_fixture.GetUserConfigFor(_endpointName) is not UserConfiguration userConfig)
        {
            TestContext.Current.AddSkippedEndpointWarning(_endpointName);
            return;
        }

        ValidateAccessTokenRequest request = new()
        {
            UserId = userConfig.UserId
        };

        ITwitchClient client = _fixture.GetTwitchApiClient();
        TwitchResponse<ValidateAccessTokenResponse> response = await client.SendAsync(request, TestContext.Current.CancellationToken);

        Assert.True(response.Content.ExpiresIn > TimeSpan.Zero);
        Assert.False(string.IsNullOrEmpty(response.Content.Login));
        Assert.False(string.IsNullOrEmpty(response.Content.UserId.Value));
        Assert.False(string.IsNullOrEmpty(response.Content.ClientId.Value));
        Assert.NotEmpty(response.Content.Scopes);
    }
}
