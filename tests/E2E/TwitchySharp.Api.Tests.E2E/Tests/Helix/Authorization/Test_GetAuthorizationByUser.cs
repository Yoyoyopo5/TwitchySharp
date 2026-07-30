using TwitchySharp.Api.Helix.Authorization;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Authorization;

public class Test_GetAuthorizationByUser(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-authorization-by-user");

    [Fact]
    public async Task Send_GetAuthorizationByUserRequest_ReturnSuccessResponse()
    {
        ClientConfiguration clientConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<ClientConfiguration>(TestName);

        const string TEST_USER_ID = "52137752";
        UserId userId = new(TEST_USER_ID);

        GetAuthorizationByUserRequest request = new()
        {
            UserIds = [userId],
            AuthorizationContext = new() { Identity = clientConfig.ToIdentity() }
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
