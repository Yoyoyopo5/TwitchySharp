using TwitchySharp.Api.Helix.Users;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Users;

public class Test_UpdateUser(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("update-user");

    [Fact]
    public async Task Send_UpdateUserRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        UpdateUserRequest request = new()
        {
            UserId = userConfig.UserId,
            Description = "On Vacation"
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestName, TestContext.Current.CancellationToken);
    }
}
