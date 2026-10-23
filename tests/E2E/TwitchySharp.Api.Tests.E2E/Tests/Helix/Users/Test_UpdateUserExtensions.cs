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
        TestingTwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        GetUserExtensionsRequest getRequest = new()
        {
            UserId = broadcasterId
        };

        TwitchResponse<GetUserExtensionsResponseContent> getResponse = await client.SendAsync(getRequest, TestName, ct);
        Assert.NotNull(getResponse.Content);
        if (getResponse.Content.Data.FirstOrDefault(ext => ext.Type.Contains(UserExtensionType.Panel)) is not InstalledExtension extension)
            return;

        UpdateUserExtensionsRequest enableRequest = new()
        {
            UserId = broadcasterId,
            Extensions = new ExtensionsConfiguration() with
            {
                PanelExtensions = [UpdateExtensionParameters.ActivateSlot(extension)]
            }
        };

        TwitchResponse<UpdateUserExtensionsResponseContent> enableResponse = await client.SendAsync(enableRequest, TestName, ct);
        Assert.Null(enableResponse.Error);
        await Task.Delay(100, ct);

        UpdateUserExtensionsRequest disableRequest = enableRequest with
        {
            Extensions = new ExtensionsConfiguration() with
            {
                PanelExtensions = [UpdateExtensionParameters.DeactivateSlot()]
            }
        };

        TwitchResponse<UpdateUserExtensionsResponseContent> disableResponse = await client.SendAsync(disableRequest, TestName, ct);
        Assert.Null(disableResponse.Error);
    }
}
