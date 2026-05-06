using TwitchySharp.Api.Helix.Clips;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Clips;

[Collection("twitch")]
public class Test_GetClips(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetClipsRequestBroadcasterId_ReturnSuccessResponse()
    {
        GetClipsRequest request = new()
        {
            Query = new BroadcasterClipsQuery()
            {
                BroadcasterId = _fixture.UserIdentity.UserId
            }
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task Send_GetClipsRequestGameId_ReturnSuccessResponse()
    {
        const string TEST_GAME_ID = "33214"; // Fortnite
        GameId testGameId = new(TEST_GAME_ID);
        GetClipsRequest request = new()
        {
            Query = new GameClipsQuery()
            {
                GameId = testGameId
            }
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task Send_GetClipsRequestClipId_ReturnSuccessResponse()
    {
        const string TEST_CLIP_ID = "ObservantVictoriousButterOSfrog-Uur1-ZdwTmNRzuQ7";
        ClipId testClipId = new(TEST_CLIP_ID);
        GetClipsRequest request = new()
        {
            Query = new ClipsIdQuery()
            {
                Ids = [testClipId]
            }
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
