using TwitchySharp.Api.Helix.Authorization;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Authorization;

[Collection("twitch")]
public class Test_GetAuthorizationByUser(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetAuthorizationByUserRequest_ReturnSuccessResponse()
    {
        const string TEST_USER_ID = "52137752";
        UserId userId = new(TEST_USER_ID);

        GetAuthorizationByUserRequest request = new()
        {
            UserIds = [userId]
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
