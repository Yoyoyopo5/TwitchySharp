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
            ExtensionId = TwitchClientFixture.ExtensionConfig.ExtensionId,
            ShouldIncludeAll = true
        };

        await TwitchClientFixture.Client.SendAsync(request, TestContext.Current.CancellationToken);
    }
}
