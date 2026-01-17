using System.Net.Http;
using TwitchySharp.Api.Models.Helix.Chat.Responses;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Chat.Requests;
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
    /// <param name="broadcasterId">The user ID of the broadcaster whose chat badges you want to get.</param>
    public GetChannelChatBadgesRequest(
        string clientId,
        string accessToken,
        string broadcasterId
        )
        : base(
            "/chat/badges",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
            )
    {
        Method = HttpMethod.Get;
    }
}
