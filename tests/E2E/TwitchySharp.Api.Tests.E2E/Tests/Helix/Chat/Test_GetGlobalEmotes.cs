using TwitchySharp.Api.Helix.Chat;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Chat;

[Collection("twitch")]
public class Test_GetGlobalEmotes(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetGlobalEmotesRequest_ReturnSuccessResponse()
    {
        GetGlobalEmotesRequest request = new();

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
