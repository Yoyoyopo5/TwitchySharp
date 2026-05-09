using TwitchySharp.Api.Helix.Extensions;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Extensions;

[Collection("twitch")]
public class Test_UpdateExtensionBitsProduct(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_UpdateExtensionBitsProductRequest_ReturnSuccessResponse()
    {
        ITwitchClient client = TwitchClientFixture.Client;
        CancellationToken ct = TestContext.Current.CancellationToken;

        GetExtensionBitsProductsRequest getRequest = new()
        {
            ExtensionId = TwitchClientFixture.ExtensionConfig.ExtensionId,
            ShouldIncludeAll = true
        };

        var getResponse = await client.SendAsync(getRequest, ct);
        var product = getResponse.Content.Data.Single(d => d.Sku == TwitchClientFixture.ExtensionConfig.BitsProduct.Sku);

        UpdateExtensionBitsProductRequest updateRequest = new()
        {
            ExtensionId = TwitchClientFixture.ExtensionConfig.ExtensionId,
            Product = new()
            {
                Sku = TwitchClientFixture.ExtensionConfig.BitsProduct.Sku,
                Cost = new() { Type = ExtensionProductCostType.Bits, Amount = 100 },
                DisplayName = "Super Cool Test Product",
                InDevelopment = true,
                Expiration = DateTimeOffset.UtcNow + TimeSpan.FromDays(1),
                IsBroadcast = true
            }
        };

        await client.SendAsync(updateRequest, ct);
        await Task.Delay(250, ct);

        UpdateExtensionBitsProductRequest restoreRequest = new()
        {
            ExtensionId = TwitchClientFixture.ExtensionConfig.ExtensionId,
            Product = new()
            {
                Sku = TwitchClientFixture.ExtensionConfig.BitsProduct.Sku,
                Cost = product.Cost,
                DisplayName = product.DisplayName,
                InDevelopment = product.InDevelopment,
                Expiration = product.Expiration,
                IsBroadcast = product.IsBroadcast,
            }
        };

        await client.SendAsync(restoreRequest, ct);
    }
}
