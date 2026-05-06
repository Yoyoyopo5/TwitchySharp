using TwitchySharp.Api.Helix.Users;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Users;

[Collection("twitch")]
public class Test_GetUsers(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetUsersRequest_ReturnSuccessResponse()
    {
        GetUsersRequest request = new()
        {
            UserIds = [new UserId("12345")],
            UserLogins = [new UserLogin("yoyoyopo5")]
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
