using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Search;

namespace TwitchySharp.Api.Tests.Integration.Helix.Search;
[Collection("helix")]
public class Test_SearchChannels(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_SearchChannelsRequest_ReturnSuccessResponse()
    {
        await _fixture.Api.SendRequestAsync(new SearchChannelsRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            "yoyoyopo5"
            ));
    }
}
