using TwitchySharp.Api.Helix.Extensions;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Extensions;

[Collection("twitch")]
public class Test_GetExtensionTransactions(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetExtensionTransactionsRequest_ReturnSuccessResponse()
    {
        GetExtensionTransactionsRequest request = new()
        {
            ExtensionId = TwitchClientFixture.ExtensionConfig.ExtensionId
        };

        await TwitchClientFixture.Client.SendAsync(request, TestContext.Current.CancellationToken);
    }
}
