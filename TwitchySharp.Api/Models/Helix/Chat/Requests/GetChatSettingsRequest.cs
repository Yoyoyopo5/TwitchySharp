using System.Net.Http;
using TwitchySharp.Api.Models.Helix.Chat.Models;
using TwitchySharp.Api.Models.Helix.Chat.Responses;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Chat.Requests;
/// <summary>
/// Gets the broadcaster’s chat settings.
/// </summary>
/// <remarks>
/// For an overview of chat settings, see <see href="https://help.twitch.tv/s/article/chat-commands#AllMods">Chat Commands for Broadcasters and Moderators</see> and <see href="https://help.twitch.tv/s/article/setting-up-moderation-for-your-twitch-channel#modpreferences">Moderator Preferences</see>.
/// <br/>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-chat-settings">Get Chat Settings</see> for more information.
/// </remarks>
public record GetChatSettingsRequest
    : TwitchHelixRequest<GetChatSettingsResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An app or user access token.</param>
    /// <param name="broadcasterId">The user id of the broadcaster whose chat settings you want to get.</param>
    /// <param name="moderatorId">
    /// The user id of the broadcaster or one of the broadcaster's moderators.
    /// <br/>
    /// This parameter is only required if you want to include the <see cref="ChatSettings.NonModeratorChatDelay"/> and <see cref="ChatSettings.NonModeratorChatDelayDuration"/> in the response.
    /// <br/>
    /// If specified, this must be the same user that created the <paramref name="accessToken"/>.
    /// </param>
    public GetChatSettingsRequest(
        string clientId,
        string accessToken,
        string broadcasterId,
        string? moderatorId = null
        )
        : base(
            "/chat/settings",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
                .Add("moderator_id", moderatorId)
            )
    {
        Method = HttpMethod.Get;
    }
}
