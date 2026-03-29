using TwitchySharp.Api.Helix.Extensions;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Extensions;

[Collection("twitch")]
public class Test_SetExtensionRequiredConfiguration(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_SetExtensionRequiredConfigurationRequest_ReturnSuccessRepsonse()
    {
        SetExtensionRequiredConfigurationRequest request = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId,
            ExtensionOwnerId = _fixture.UserIdentity.UserId,
            Configuration = new()
            {
                ExtensionId = _fixture.Extension.Id,
                ExtensionVersion = _fixture.Extension.Version,
                RequiredConfiguration = "Test Required Configuration"
            }
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
