using TwitchySharp.Api.Helix.Users;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Users;

[Collection("twitch")]
public class Test_GetUserExtensions(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetUserExtensionsRequest_ReturnSuccessResponse()
    {
        GetUserExtensionsRequest request = new()
        {
            UserId = _fixture.UserIdentity.UserId
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
