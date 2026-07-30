using TwitchySharp.Api.Authorization;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Authentication;

public class Test_OidcJwkRequest(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    [Fact]
    public async Task Send_OidcJwtRequest_ReturnSuccessResponse()
    {
        OidcJwkRequest request = new();

        ITwitchClient client = _fixture.GetTwitchApiClient();
        TwitchResponse<OidcJwkResponse> response = await client.SendAsync(request, TestContext.Current.CancellationToken);

        Assert.NotEmpty(response.Content.Keys);
    }
}
