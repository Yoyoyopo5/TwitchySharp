using TwitchySharp.Api.Authentication;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Authentication;

public class Test_OidcJwkRequest(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    [Fact]
    public async Task Send_OidcJwtRequest_ReturnSuccessResponse()
    {
        OidcJwkRequest request = new();

        TestingTwitchClient client = _fixture.GetTwitchApiClient();
        TwitchResponse<OidcJwkResponseContent> response = await client.SendAsync(request, new(), TestContext.Current.CancellationToken);

        Assert.NotEmpty(response.Content.Keys);
    }
}
