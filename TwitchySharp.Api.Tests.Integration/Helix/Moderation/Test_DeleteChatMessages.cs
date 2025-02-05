using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Chat;
using TwitchySharp.Api.Helix.Moderation;

namespace TwitchySharp.Api.Tests.Integration.Helix.Moderation;
[Collection("helix")]
public class Test_DeleteChatMessages(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_DeleteChatMessagesRequest_ReturnSuccessResponse()
    {
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        string messageId = (await SendChatMessage(broadcasterId)).Data.First().MessageId;
        await DeleteChatMessage(broadcasterId, messageId); // Technically can't delete broadcaster's message but this call still succeeds.
    }

    private ValueTask<SendChatMessageResponse> SendChatMessage(string broadcasterId)
        => _fixture.Api.SendRequestAsync(new SendChatMessageRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            new SendChatMessageRequestData()
            {
                BroadcasterId = broadcasterId,
                SenderId = broadcasterId,
                Message = "test message pls delete"
            }
            ));

    private ValueTask<DeleteChatMessagesResponse> DeleteChatMessage(string broadcasterId, string messageId)
        => _fixture.Api.SendRequestAsync(new DeleteChatMessagesRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            broadcasterId,
            messageId
            ));
}
