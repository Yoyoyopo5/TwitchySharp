using TwitchySharp.Api.Helix.Moderation;
using TwitchySharp.Shared.Enums;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Moderation;

[Collection("twitch")]
public class Test_UpdateAutoModSettings(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_UpdateAutoModSettingsOverallRequest_ReturnSuccessResponse()
    {
        await UpdateAutoModSettings(_fixture.CreateClient(), _fixture.UserIdentity.UserId, new UpdateAutoModOverallLevelData(AutomodFilteringLevel.Less), TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task Send_UpdateAutoModSettingsCustomRequest_ReturnSuccessResponse()
    {
        await UpdateAutoModSettings(_fixture.CreateClient(), _fixture.UserIdentity.UserId, new UpdateAutoModCustomLevelsData()
        {
            Aggression = AutomodFilteringLevel.Less,
            Swearing = AutomodFilteringLevel.None
        }, TestContext.Current.CancellationToken);
    }

    private static ValueTask<TwitchResponse<UpdateAutoModSettingsResponse>> UpdateAutoModSettings(ITwitchClient client, UserId broadcasterId, UpdateAutoModSettingsRequestData settings, CancellationToken ct)
        => client.SendAsync(new UpdateAutoModSettingsRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId,
            Settings = settings
        }, ct);
}
