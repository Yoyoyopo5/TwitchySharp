using TwitchySharp.Api.Helix.Extensions;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Extensions;

public class Test_GetExtensions(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-extensions");

    [Fact]
    public async Task Send_GetExtensionsRequest_ReturnSuccessResponse()
    {
        ExtensionConfiguration extensionConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<ExtensionConfiguration>(TestName);

        const string TEST_EXTENSION_VERSION = "0.0.1";
        ExtensionVersion testExtensionVersion = new(TEST_EXTENSION_VERSION);
        GetExtensionsRequest request = new()
        {
            ExtensionId = extensionConfig.ExtensionId,
            ExtensionIdentity = extensionConfig.ToIdentity(),
            ExtensionVersion = testExtensionVersion // must specify because its unreleased
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
