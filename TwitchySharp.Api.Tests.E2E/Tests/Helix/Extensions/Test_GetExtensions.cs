using TwitchySharp.Api.Helix.Extensions;
using TwitchySharp.Shared.Models;

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
            ExtensionId = _fixture.Extension.Id,
            ExtensionIdentity = _fixture.ExtensionIdentity,
            ExtensionVersion = testExtensionVersion // must specify because its unreleased
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
