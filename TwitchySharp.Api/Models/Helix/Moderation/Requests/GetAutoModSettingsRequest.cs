using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models.Helix.Moderation.Responses;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Moderation.Requests;
/// <summary>
/// Gets the broadcaster’s AutoMod settings. 
/// </summary>
/// <remarks>
/// The settings are used to automatically block inappropriate or harassing messages from appearing in the broadcaster’s chat room.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadAutomodSettings"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-automod-settings">Get AutoMod Settings</see> for more information.
/// </remarks>
public record GetAutoModSettingsRequest
    : TwitchHelixRequest<GetAutoModSettingsResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorReadAutomodSettings"/>.</param>
    /// <param name="broadcasterId">The user id of the broadcaster (channel) to get AutoMod settings for.</param>
    /// <param name="moderatorId">
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// This must be the same user that created the <paramref name="accessToken"/>.
    /// </param>
    public GetAutoModSettingsRequest(
        string clientId,
        string accessToken,
        string broadcasterId,
        string moderatorId
        ) : base(
            "/moderation/automod/settings",
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
