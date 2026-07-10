using System.Net;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Tests.Integration.Controllers;
using TwitchySharp.Api.Tests.Integration.Fixtures;

namespace TwitchySharp.Api.Tests.Integration.Tests;

public class Test_AuthorizationCodeRequest(TwitchApiTestFixture fixture) : IClassFixture<TwitchApiTestFixture>
{
    private readonly TwitchApiTestFixture _fixture = fixture;

    [Fact]
    public async Task AuthorizationCodeRequest_GetTwitchOidc_ContainsExpectedData()
    {
        ITwitchClient client = _fixture.CreateTwitchClient();
        AuthorizationCodeRequest request = new()
        {
            Host = "localhost",
            ClientId = TwitchApiTestFixture.TestClientId,
            ClientSecret = TwitchApiTestFixture.TestClientSecret,
            Code = TwitchApiTestFixture.TEST_AUTHORIZATION_CODE,
            RedirectUri = TwitchApiTestFixture.TestRedirectUri
        };

        TwitchResponse<AuthorizationCodeResponse> response = await client.SendAsync(request, TestContext.Current.CancellationToken);

        TwitchOidc? oidc = response.Content.GetOidc();

        Assert.Equal(MockAuthorizationController.TestOidc, oidc);
    }
}
