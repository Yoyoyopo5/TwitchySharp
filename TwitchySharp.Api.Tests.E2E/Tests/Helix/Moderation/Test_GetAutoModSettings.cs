using TwitchySharp.Api.Helix.Moderation;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Moderation;

[Collection("twitch")]
public class Test_GetAutoModSettings(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetAutoModSettingsRequest_ReturnSuccessResponse()
    {
        GetAutoModSettingsRequest request = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId,
            ModeratorId = _fixture.UserIdentity.UserId
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
