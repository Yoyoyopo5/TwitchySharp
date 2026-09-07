using TwitchySharp.Api.Helix.Extensions;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Extensions;

public class Test_SetExtensionRequiredConfiguration(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("set-extension-required-configuration");

    [Fact]
    public async Task Send_SetExtensionRequiredConfigurationRequest_ReturnSuccessRepsonse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        ExtensionConfiguration extensionConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<ExtensionConfiguration>(TestName);

        SetExtensionRequiredConfigurationRequest request = new()
        {
            BroadcasterId = userConfig.UserId,
            Configuration = new()
            {
                ExtensionId = extensionConfig.ExtensionId,
                ExtensionVersion = extensionConfig.Version,
                RequiredConfiguration = "Test Required Configuration"
            }
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestName, TestContext.Current.CancellationToken);
    }
}
