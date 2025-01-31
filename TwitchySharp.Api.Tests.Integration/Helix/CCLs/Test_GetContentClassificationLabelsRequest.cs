using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.CCLs;

namespace TwitchySharp.Api.Tests.Integration.Helix.CCLs;
[Collection("helix")]
public class Test_GetContentClassificationLabelsRequest(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_GetContentClassificationLabelsRequest_ReturnSuccessResponse()
    {
        GetContentClassificationLabelsRequest stubRequest = new(_fixture.Secrets.Client.Id, _fixture.Secrets.User.AccessToken, ContentClassificationLocale.EnglishUnitedStates);

        GetContentClassificationLabelsResponse actualResponse = await _fixture.Api.SendRequestAsync(stubRequest);
    }
}
