using TwitchySharp.Api.Helix.Extensions;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Extensions;

public class Test_UpdateExtensionBitsProduct(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("update-extension-bits-product");

    [Fact]
    public async Task Send_UpdateExtensionBitsProductRequest_ReturnSuccessResponse()
    {
        ExtensionConfiguration extensionConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<ExtensionConfiguration>(TestName);

        TestingTwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        GetExtensionBitsProductsRequest getRequest = new()
        {
            ExtensionId = extensionConfig.ExtensionId,
            ShouldIncludeAll = true
        };

        TwitchResponse<GetExtensionBitsProductsResponseContent> getResponse = await client.SendAsync(getRequest, TestName, ct);
        ExtensionBitsProduct product = getResponse.Content.Data.Single(d => d.Sku == extensionConfig.BitsProduct.Sku);

        UpdateExtensionBitsProductRequest updateRequest = new()
        {
            ExtensionId = extensionConfig.ExtensionId,
            Product = new()
            {
                Sku = extensionConfig.BitsProduct.Sku,
                Cost = new() { Type = ExtensionProductCostType.Bits, Amount = 100 },
                DisplayName = "Super Cool Test Product",
                InDevelopment = true,
                Expiration = DateTimeOffset.UtcNow + TimeSpan.FromDays(1),
                IsBroadcast = true
            }
        };

        await client.SendAsync(updateRequest, TestName, ct);
        await Task.Delay(250, ct);

        UpdateExtensionBitsProductRequest restoreRequest = new()
        {
            ExtensionId = extensionConfig.ExtensionId,
            Product = new()
            {
                Sku = extensionConfig.BitsProduct.Sku,
                Cost = product.Cost,
                DisplayName = product.DisplayName,
                InDevelopment = product.InDevelopment,
                Expiration = product.Expiration,
                IsBroadcast = product.IsBroadcast,
            }
        };

        await client.SendAsync(restoreRequest, TestName, ct);
    }
}
