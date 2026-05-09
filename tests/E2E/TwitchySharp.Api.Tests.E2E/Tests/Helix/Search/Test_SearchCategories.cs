using TwitchySharp.Api.Helix.Search;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Search;

[Collection("twitch")]
public class Test_SearchCategories(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_SearchCategoriesRequest_ReturnSuccessResponse()
    {
        SearchCategoriesRequest request = new()
        {
            Query = "Fortnite"
        };

        await TwitchClientFixture.Client.SendAsync(request, TestContext.Current.CancellationToken);
    }
}
