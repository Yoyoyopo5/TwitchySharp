using TwitchySharp.Api.Helix.Chat;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Chat;

public class Test_GetSharedChatSession(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-shared-chat-session");

    [Fact]
    public async Task Send_GetSharedChatSessionRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        // This cannot be fully tested without actually being in a shared chat session.
        GetSharedChatSessionRequest request = new()
        {
            BroadcasterId = userConfig.UserId
        };

        TwitchResponse<GetSharedChatSessionResponseContent> response = await _fixture.GetTwitchApiClient().SendAsync(request, TestName, TestContext.Current.CancellationToken);

        if (response.Content.Data.Length == 0)
            TestContext.Current.AddWarning("The broadcaster is not in a shared chat session.");
    }
}
