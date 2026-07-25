using TwitchySharp.Api.Helix.Search;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Search;

public class Test_SearchCategories(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("search-categories");

    [Fact]
    public async Task Send_SearchCategoriesRequest_ReturnSuccessResponse()
    {
        ClientConfiguration clientConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<ClientConfiguration>(TestName);

        SearchCategoriesRequest request = new()
        {
            AuthorizationContext = new() { Identity = clientConfig.ToIdentity() },
            Query = "Fortnite"
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
