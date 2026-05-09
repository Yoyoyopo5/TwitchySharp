using TwitchySharp.Api.Helix.Extensions;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Extensions;

[Collection("twitch")]
public class Test_GetExtensions(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetExtensionsRequest_ReturnSuccessResponse()
    {
        const string TEST_EXTENSION_VERSION = "0.0.1";
        ExtensionVersion testExtensionVersion = new(TEST_EXTENSION_VERSION);
        GetExtensionsRequest request = new()
        {
            ExtensionId = TwitchClientFixture.ExtensionConfig.ExtensionId,
            ExtensionIdentity = _fixture.ExtensionIdentity,
            ExtensionVersion = testExtensionVersion // must specify because its unreleased
        };

        await TwitchClientFixture.Client.SendAsync(request, TestContext.Current.CancellationToken);
    }
}
