using TwitchySharp.Api.Helix.Search;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Search;

[Collection("twitch")]
public class Test_SearchChannels(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_SearchChannelsRequest_ReturnSuccessResponse()
    {
        SearchChannelsRequest request = new() { Query = "yoyoyopo5" };

        await TwitchClientFixture.Client.SendAsync(request, TestContext.Current.CancellationToken);
    }
}
