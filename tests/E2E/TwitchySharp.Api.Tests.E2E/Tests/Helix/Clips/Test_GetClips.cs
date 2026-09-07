using TwitchySharp.Api.Helix.Clips;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Clips;

public class Test_GetClips(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-clips");

    [Fact]
    public async Task Send_GetClipsRequestBroadcasterId_ReturnSuccessResponse()
    {
        _fixture.GetAuthorizingConfigForTestOrSkip<ClientConfiguration>(TestName);

        UserId broadcasterId = new("52137752");

        GetClipsRequest request = new()
        {
            Query = new BroadcasterClipsQuery()
            {
                BroadcasterId = broadcasterId
            }
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestName, TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task Send_GetClipsRequestGameId_ReturnSuccessResponse()
    {
        _fixture.GetAuthorizingConfigForTestOrSkip<ClientConfiguration>(TestName);

        const string TEST_GAME_ID = "33214"; // Fortnite
        GameId testGameId = new(TEST_GAME_ID);
        GetClipsRequest request = new()
        {
            Query = new GameClipsQuery()
            {
                GameId = testGameId
            }
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestName, TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task Send_GetClipsRequestClipId_ReturnSuccessResponse()
    {
        _fixture.GetAuthorizingConfigForTestOrSkip<ClientConfiguration>(TestName);

        const string TEST_CLIP_ID = "ObservantVictoriousButterOSfrog-Uur1-ZdwTmNRzuQ7";
        ClipId testClipId = new(TEST_CLIP_ID);
        GetClipsRequest request = new()
        {
            Query = new ClipsIdQuery()
            {
                Ids = [testClipId]
            }
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestName, TestContext.Current.CancellationToken);
    }
}
