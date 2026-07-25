using TwitchySharp.Api.Helix.Users;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Users;

public class Test_UpdateUserExtensions(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("update-user-extensions");

    [Fact]
    public async Task Send_UpdateUserExtensionsRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        UserId broadcasterId = userConfig.UserId;
        ITwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        GetUserExtensionsRequest getRequest = new()
        {
            UserId = broadcasterId
        };

        TwitchResponse<GetUserExtensionsResponse> getResponse = await client.SendAsync(getRequest, ct);
        if (getResponse.Content.Data.Where(ext => ext.Type.Contains(UserExtensionType.Panel)).FirstOrDefault() is not InstalledExtension extension)
            return;

        UpdateUserExtensionsRequest enableRequest = new()
        {
            UserId = broadcasterId,
            Extensions = new ExtensionsConfiguration() with
            {
                PanelExtensions = [UpdateExtensionParameters.ActivateSlot(extension)]
            }
        };

        await client.SendAsync(enableRequest, ct);
        await Task.Delay(100, ct);

        UpdateUserExtensionsRequest disableRequest = enableRequest with
        {
            Extensions = new ExtensionsConfiguration() with
            {
                PanelExtensions = [UpdateExtensionParameters.DeactivateSlot()]
            }
        };

        await client.SendAsync(disableRequest, ct);
    }
}
