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
            ExtensionId = _fixture.Extension.Id,
            ExtensionOwnerId = _fixture.UserIdentity.UserId
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
