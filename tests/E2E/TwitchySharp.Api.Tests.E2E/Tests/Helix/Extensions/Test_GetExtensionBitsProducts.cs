using TwitchySharp.Api.Helix.Extensions;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Extensions;

[Collection("twitch")]
public class Test_GetExtensionBitsProducts(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetExtensionBitsProductsRequest_ReturnSuccessResponse()
    {
        GetExtensionBitsProductsRequest request = new()
        {
            ExtensionId = _fixture.Extension.Id,
            ShouldIncludeAll = true
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
