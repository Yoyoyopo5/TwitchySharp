using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Moderation;

namespace TwitchySharp.Api.Tests.Integration.Helix.Moderation;
[Collection("helix")]
public class Test_ManageHeldAutoModMessages(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_ManageHeldAutoModMessages_ReturnSuccessResponse()
    {
        const string heldMessageId = "1234"; // Need to use EventSub to test this endpoint.
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        await _fixture.Api.SendRequestAsync(new ManageHeldAutoModMessagesRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            new ManageHeldAutoModMessagesRequestData()
            {
                MessageId = heldMessageId,
                UserId = broadcasterId,
                Action = AutoModAction.Allow
            }
            ));
    }
}
