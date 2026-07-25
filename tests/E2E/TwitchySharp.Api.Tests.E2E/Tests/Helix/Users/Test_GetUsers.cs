using TwitchySharp.Api.Helix.Users;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Users;

public class Test_GetUsers(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-users");

    [Fact]
    public async Task Send_GetUsersRequest_ReturnSuccessResponse()
    {
        ClientConfiguration clientConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<ClientConfiguration>(TestName);

        GetUsersRequest request = new()
        {
            AuthorizationContext = new() { Identity = clientConfig.ToIdentity() },
            UserIds = [new UserId("12345")],
            UserLogins = [new UserLogin("yoyoyopo5")]
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
