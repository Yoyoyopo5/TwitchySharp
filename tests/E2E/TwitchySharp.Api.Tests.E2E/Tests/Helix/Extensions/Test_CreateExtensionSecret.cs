using TwitchySharp.Api.Helix.Extensions;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Extensions;

public class Test_CreateExtensionSecret(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("create-extension-secret");

    [Fact]
    public async Task Send_CreateExtensionSecretRequest_ReturnSuccessResponse()
    {
        // This will likely invalidate the existing secret.

        ExtensionConfiguration extensionConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<ExtensionConfiguration>(TestName);

        CreateExtensionSecretRequest request = new()
        {
            ExtensionId = extensionConfig.ExtensionId,
            ExtensionOwnerId = extensionConfig.ExtensionOwnerUserId
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
