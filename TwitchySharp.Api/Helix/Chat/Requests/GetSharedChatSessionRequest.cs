using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Retrieves the active shared chat session for a channel.
/// </summary>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">An app or user access token.</param>
/// <param name="broadcasterId">The user id of the broadcaster whose shared chat you want to get.</param>
public class GetSharedChatSessionRequest(string clientId, string accessToken, string broadcasterId)
    : HelixApiRequest<GetSharedChatSessionResponse>(
        "/shared_chat/session" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId),
        clientId,
        accessToken
        );
