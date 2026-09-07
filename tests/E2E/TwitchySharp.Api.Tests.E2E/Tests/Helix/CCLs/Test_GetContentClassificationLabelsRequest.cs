using TwitchySharp.Api.Helix.CCLs;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.CCLs;

public class Test_GetContentClassificationLabelsRequest(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private readonly static TestName TestName = new("get-content-classification-labels");

    [Fact]
    public async Task Send_GetContentClassificationLabelsRequest_ReturnSuccessResponse()
    {
        _fixture.GetAuthorizingConfigForTestOrSkip<ClientConfiguration>(TestName);

        GetContentClassificationLabelsRequest request = new();

        await _fixture.GetTwitchApiClient().SendAsync(request, TestName, TestContext.Current.CancellationToken);
    }
}
