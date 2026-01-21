using System.Net.Http;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Gets the broadcaster’s list of custom chat badges. 
/// </summary>
/// <remarks>
/// The list is empty if the broadcaster hasn’t created custom chat badges. 
/// For information about custom badges, see <see href="https://help.twitch.tv/s/article/subscriber-badge-guide">Subscriber Badges</see> and <see href="https://help.twitch.tv/s/article/custom-bit-badges-guide">Bits Badges</see>.
/// <br/>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-channel-chat-badges">Get Channel Chat Badges</see> for more information.
/// </remarks>
public record GetChannelChatBadgesRequest
    : TwitchHelixRequest<GetChannelChatBadgesResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An app or user access token.</param>
    /// <param name="parameters">The request parameters.</param>
    public GetChannelChatBadgesRequest(
        ClientId clientId,
        AccessToken accessToken,
        GetChannelChatBadgesRequestParameters parameters
        )
        : base(
            "/chat/badges",
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
/// Request parameters for a <see cref="GetChannelChatBadgesRequest"/>.
/// </summary>
public record GetChannelChatBadgesRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster (channel) whose chat badges you want to get.
    /// </summary>
    public required UserId BroadcasterId { get; set; }
}
