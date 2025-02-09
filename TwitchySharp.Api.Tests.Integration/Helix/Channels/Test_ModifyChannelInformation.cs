using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Channels;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Tests.Integration.Helix.Channels;
[Collection("helix")]
public class Test_ModifyChannelInformation(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_ModifyChannelInformationRequest_ReturnSuccessResponse()
    {
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        await _fixture.Api.SendRequestAsync(new ModifyChannelInformationRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            new ModifyChannelInformationRequestData()
            {
                GameId = "1",
                BroadcasterLanguage = "en",
                Title = "test title pls ignore",
                Tags = ["testtag"],
                ContentClassificationLabels = [new ContentClassificationLabel(ContentClassificationLabelType.DrugsIntoxication, true)],
                IsBrandedContent = false
            }
            ));
    }
}
