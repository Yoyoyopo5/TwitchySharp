using TwitchySharp.Api.Helix.Extensions;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Extensions;

[Collection("twitch")]
public class Test_UpdateExtensionBitsProduct(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_UpdateExtensionBitsProductRequest_ReturnSuccessResponse()
    {
        ITwitchClient client = _fixture.CreateClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        GetExtensionBitsProductsRequest getRequest = new()
        {
            ExtensionId = _fixture.Extension.Id,
            ShouldIncludeAll = true
        };

        var getResponse = await client.SendAsync(getRequest, ct);
        var product = getResponse.Content.Data.Single(d => d.Sku == _fixture.Extension.BitsProduct.Sku);

        UpdateExtensionBitsProductRequest updateRequest = new()
        {
            ExtensionId = _fixture.Extension.Id,
            Product = new()
            {
                Sku = _fixture.Extension.BitsProduct.Sku,
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
            ExtensionId = _fixture.Extension.Id,
            Product = new()
            {
                Sku = _fixture.Extension.BitsProduct.Sku,
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
