using TwitchySharp.Api.Helix.CCLs;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.CCLs;

[Collection("twitch")]
public class Test_GetContentClassificationLabelsRequest(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetContentClassificationLabelsRequest_ReturnSuccessResponse()
    {
        GetContentClassificationLabelsRequest request = new();

        await TwitchClientFixture.Client.SendAsync(request, TestContext.Current.CancellationToken);
    }
}
