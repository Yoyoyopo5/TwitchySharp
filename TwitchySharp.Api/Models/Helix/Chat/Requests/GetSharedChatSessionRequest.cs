using System.Net.Http;
using TwitchySharp.Api.Models.Helix.Chat.Responses;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Chat.Requests;
/// <summary>
/// Retrieves the active shared chat session for a channel.
/// </summary>
/// <remarks>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference#get-shared-chat-session">Get Shared Chat Session</see> for more information.
/// </remarks>
public record GetSharedChatSessionRequest
    : TwitchHelixRequest<GetSharedChatSessionResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An app or user access token.</param>
    /// <param name="broadcasterId">The user id of the broadcaster whose shared chat you want to get.</param>
    public GetSharedChatSessionRequest(
        string clientId,
        string accessToken,
        string broadcasterId
        )
        : base(
            "/shared_chat/session",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
            )
    {
        Method = HttpMethod.Get;
    }
}
