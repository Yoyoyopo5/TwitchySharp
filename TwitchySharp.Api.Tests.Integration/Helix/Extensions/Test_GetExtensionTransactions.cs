using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Helix.Extensions;

namespace TwitchySharp.Api.Tests.Integration.Helix.Extensions;
[Collection("helix")]
public class Test_GetExtensionTransactions(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_GetExtensionTransactionsRequest_ReturnSuccessResponse()
    {
        string appToken = (await _fixture.Api.SendRequestAsync(new ClientCredentialsRequest(
            _fixture.Secrets.ExtensionClientId,
            _fixture.Secrets.ExtensionClientSecret
            ))).AccessToken;

        await _fixture.Api.SendRequestAsync(new GetExtensionTransactionsRequest(
            _fixture.Secrets.ExtensionClientId,
            appToken,
            _fixture.Secrets.ExtensionClientId
            ));
    }
}
