using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Bits;

namespace TwitchySharp.Api.Tests.Integration.Helix.Bits;
[Collection("helix")]
public class Test_GetCheermotesRequest(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_GetCheermotesRequest_ReturnSuccessResponse()
    {
        const string BROADCASTER_ID = "52137752";
        GetCheermotesRequest stubRequest = new(_fixture.Secrets.Client.Id, _fixture.Secrets.User.AccessToken, BROADCASTER_ID);

        GetCheermotesResponse actualResponse = await _fixture.Api.SendRequestAsync(stubRequest);
    }
}
