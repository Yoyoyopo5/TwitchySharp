using TwitchySharp.Api.Helix.Users;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Users;

[Collection("twitch")]
public class Test_UpdateUser(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_UpdateUserRequest_ReturnSuccessResponse()
    {
        UpdateUserRequest request = new()
        {
            UserId = _fixture.UserIdentity.UserId,
            Description = "On Vacation"
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
