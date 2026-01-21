using System.Net.Http;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Chat;
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
    /// <param name="parameters">The request parameters.</param>
    public GetSharedChatSessionRequest(
        ClientId clientId,
        AccessToken accessToken,
        GetSharedChatSessionRequestParameters parameters
        )
        : base(
            "/shared_chat/session",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetSharedChatSessionRequest"/>.
/// </summary>
public record GetSharedChatSessionRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster whose shared chat you want to get.
    /// </summary>
    public required UserId BroadcasterId { get; set; }
}