using TwitchySharp.Api.Helix.Users;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Users;

[Collection("twitch")]
public class Test_GetUserBlockList(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetUserBlockListRequest_ReturnSuccessResponse()
    {
        GetUserBlockListRequest request = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
