using TwitchySharp.Api.Helix.Chat;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Chat;

public class Test_SendChatMessage(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("send-chat-message");

    [Fact]
    public async Task Send_SendChatMessageRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        SendChatMessageRequest request = new()
        {
            Message = new()
            {
                BroadcasterId = userConfig.UserId,
                SenderId = userConfig.UserId,
                Message = "test message pls ignore"
            }
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestName, TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task Send_SendChatMessageRequestUsingPriorAuthorization_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        SendChatMessageRequest request = new()
        {
            Message = new()
            {
                BroadcasterId = userConfig.UserId,
                SenderId = userConfig.UserId,
                Message = "bot mode test message pls ignore"
            }
        };

        await _fixture.GetTwitchApiClient().SendAsync(request with { AuthenticationContext = request.AuthenticationContext with
        {
            UsePriorAuthorization = true,
            Identity = request.AuthenticationContext.Identity with { ClientId = userConfig.Token.ClientId }
        }}, TestName, TestContext.Current.CancellationToken);
    }
}
