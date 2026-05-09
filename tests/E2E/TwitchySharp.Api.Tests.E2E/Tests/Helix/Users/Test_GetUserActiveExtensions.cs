using TwitchySharp.Api.Helix.Users;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Users;

[Collection("twitch")]
public class Test_GetUserActiveExtensions(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetUserActiveExtensionsRequest_ReturnSuccessResponse()
    {
        GetUserActiveExtensionsRequest request = new()
        {
            UserId = _fixture.UserIdentity.UserId
        };

        await TwitchClientFixture.Client.SendAsync(request, TestContext.Current.CancellationToken);
    }
}
