using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Helix.Extensions;

namespace TwitchySharp.Api.Tests.Integration.Helix.Extensions;
[Collection("helix")]
public class Test_GetExtensionBitsProducts(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_GetExtensionBitsProductsRequest_ReturnSuccessResponse()
    {
        string appToken = (await _fixture.Api.SendRequestAsync(new ClientCredentialsRequest(
            _fixture.Secrets.Extension.Id,
            _fixture.Secrets.Extension.ApiSecret
            ))).AccessToken;

        await _fixture.Api.SendRequestAsync(new GetExtensionBitsProductsRequest(
            _fixture.Secrets.Extension.Id,
            appToken
            ));
    }
}
