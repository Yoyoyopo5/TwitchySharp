using TwitchySharp.Api.Helix.Whispers;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Whispers;

[Collection("twitch")]
public class Test_SendWhisper(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_SendWhisperRequest_ReturnSuccessResponse()
    {
        const string TO_USER_ID = "52137752";
        UserId toUserId = new(TO_USER_ID);

        SendWhisperRequest request = new()
        {
            FromUserId = _fixture.UserIdentity.UserId,
            ToUserId = toUserId,
            Whisper = new() { Message = "test whisper pls ignore" }
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
