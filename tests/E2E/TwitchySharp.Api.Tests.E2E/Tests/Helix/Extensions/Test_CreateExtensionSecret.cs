using TwitchySharp.Api.Helix.Extensions;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Extensions;

[Collection("twitch")]
public class Test_CreateExtensionSecret(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_CreateExtensionSecretRequest_ReturnSuccessResponse()
    {
        // This will likely invalidate the existing secret.

        CreateExtensionSecretRequest request = new()
        {
            ExtensionId = TwitchClientFixture.ExtensionConfig.ExtensionId,
            ExtensionOwnerId = _fixture.UserIdentity.UserId
        };

        await TwitchClientFixture.Client.SendAsync(request, TestContext.Current.CancellationToken);
    }
}
