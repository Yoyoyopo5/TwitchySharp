using TwitchySharp.Api.Helix.Chat;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Chat;

public class Test_GetGlobalEmotes(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    public static readonly TestName TestName = new("get-global-emotes");

    [Fact]
    public async Task Send_GetGlobalEmotesRequest_ReturnSuccessResponse()
    {
        ClientConfiguration clientConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<ClientConfiguration>(TestName);

        GetGlobalEmotesRequest request = new()
        {
            AuthorizationContext = new() { Identity = clientConfig.ToIdentity() }
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
