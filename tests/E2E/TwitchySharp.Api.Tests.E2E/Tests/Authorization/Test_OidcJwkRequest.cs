using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Tests.E2E.Tests.Authorization;

[Collection("twitch")]
public class Test_OidcJwkRequest(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_OidcJwtRequest_ReturnSuccessResponse()
    {
        OidcJwkRequest request = new();

        OidcJwkResponse response = (await TwitchClientFixture.Client.SendAsync(request, TestContext.Current.CancellationToken)).Content;

        Assert.NotEmpty(response.Keys);
    }
}
