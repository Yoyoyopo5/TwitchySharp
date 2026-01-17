using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models.Helix.GuestStar.Responses;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.GuestStar.Requests;
/// <summary>
/// <b>BETA</b> Gets the channel settings for configuration of the Guest Star feature for a particular host.
/// </summary>
/// <remarks>
/// Requires a user access token that includes one of <see cref="Scope.ChannelReadGuestStar"/>, <see cref="Scope.ChannelManageGuestStar"/>, <see cref="Scope.ModeratorReadGuestStar"/>, or <see cref="Scope.ModeratorManageGuestStar"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-channel-guest-star-settings">Get Channel Guest Star Settings</see> for more information.
/// </remarks>
public record GetChannelGuestStarSettingsRequest
    : TwitchHelixRequest<GetChannelGuestStarSettingsResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes one of <see cref="Scope.ChannelReadGuestStar"/>, <see cref="Scope.ChannelManageGuestStar"/>, <see cref="Scope.ModeratorReadGuestStar"/>, or <see cref="Scope.ModeratorManageGuestStar"/>.</param>
    /// <param name="broadcasterId">The user id of the broadcaster to get Guest Star settings for.</param>
    /// <param name="moderatorId">
    /// The user id of the broadcaster or a moderator in the broadcaster's chat.
    /// This must be the same user that created the <paramref name="accessToken"/>.
    /// </param>
    public GetChannelGuestStarSettingsRequest(
        string clientId,
        string accessToken,
        string broadcasterId,
        string moderatorId
        ) : base(
            "/guest_star/channel_settings",
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
