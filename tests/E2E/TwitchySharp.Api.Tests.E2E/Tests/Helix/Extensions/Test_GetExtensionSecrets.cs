using TwitchySharp.Api.Helix.Extensions;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Extensions;

[Collection("twitch")]
public class Test_GetExtensionSecrets(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetExtensionSecretsRequest_ReturnSuccessResponse()
    {
        GetExtensionSecretsRequest request = new()
        {
            ExtensionId = _fixture.Extension.Id,
            ExtensionIdentity = _fixture.ExtensionIdentity
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
