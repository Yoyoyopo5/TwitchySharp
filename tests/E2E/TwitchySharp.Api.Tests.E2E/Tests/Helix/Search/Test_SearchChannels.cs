using TwitchySharp.Api.Helix.Search;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Search;

public class Test_SearchChannels(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("search-channels");

    [Fact]
    public async Task Send_SearchChannelsRequest_ReturnSuccessResponse()
    {
        _fixture.GetAuthorizingConfigForTestOrSkip<ClientConfiguration>(TestName);

        SearchChannelsRequest request = new()
        {
            Query = "yoyoyopo5"
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestName, TestContext.Current.CancellationToken);
    }
}
