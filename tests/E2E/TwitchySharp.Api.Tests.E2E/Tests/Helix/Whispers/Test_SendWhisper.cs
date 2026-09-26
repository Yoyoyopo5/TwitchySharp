using TwitchySharp.Api.Helix.Whispers;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Whispers;

public class Test_SendWhisper(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("send-whisper");

    [Fact]
    public async Task Send_SendWhisperRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        const string TO_USER_ID = "52137752";
        UserId toUserId = new(TO_USER_ID);

        SendWhisperRequest request = new()
        {
            FromUserId = userConfig.UserId,
            ToUserId = toUserId,
            Whisper = new() { Message = "test whisper pls ignore" }
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestName, TestContext.Current.CancellationToken);
    }
}
