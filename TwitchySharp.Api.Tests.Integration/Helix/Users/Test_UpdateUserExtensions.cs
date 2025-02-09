using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Users;

namespace TwitchySharp.Api.Tests.Integration.Helix.Users;
[Collection("helix")]
public class Test_UpdateUserExtensions(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_UpdateUserExtensionsRequest_ReturnSuccessResponse()
    {
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

         InstalledExtension extension = (await _fixture.Api.SendRequestAsync(new GetUserExtensionsRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken
            ))).Data.Where(ext => ext.Type.Contains(UserExtensionType.Overlay) && ext.CanActivate).First();

        await _fixture.Api.SendRequestAsync(new UpdateUserExtensionsRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            new ExtensionsConfiguration()
            {
                OverlayExtensions = new ExtensionsConfigurationType<UpdateExtensionParameters>()
                    .ConfigureExtension(extension.Id, extension.Version, new()
                    {
                        Active = true
                    })
            }
            ));
    }
}
