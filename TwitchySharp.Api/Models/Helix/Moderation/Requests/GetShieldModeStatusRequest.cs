using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models.Helix.Moderation.Responses;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Moderation.Requests;
/// <summary>
/// Gets the broadcaster’s Shield Mode activation status.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadShieldMode"/> or <see cref="Scope.ModeratorManageShieldMode"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-shield-mode-status">Get Shield Mode Status</see> for more information.
/// </remarks>
public record GetShieldModeStatusRequest
    : TwitchHelixRequest<GetShieldModeStatusResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorReadShieldMode"/> or <see cref="Scope.ModeratorManageShieldMode"/>.</param>
    /// <param name="broadcasterId">The user id of the broadcaster (channel) to get Shield Mode status for.</param>
    /// <param name="moderatorId">
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// This must be the same user that created the <paramref name="accessToken"/>.
    /// </param>
    public GetShieldModeStatusRequest(
        string clientId,
        string accessToken,
        string broadcasterId,
        string moderatorId
        ) : base(
            "/moderation/shield_mode",
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
