using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.GuestStar;
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
    /// <param name="parameters">The request parameters.</param>
    public GetChannelGuestStarSettingsRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        GetChannelGuestStarSettingsRequestParameters parameters
        ) : base(
            "/guest_star/channel_settings",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("moderator_id", parameters.ModeratorId)
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetChannelGuestStarSettingsRequest"/>.
/// </summary>
public record GetChannelGuestStarSettingsRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster to get Guest Star settings for.
    /// </summary>
    public required UserId BroadcasterId { get; set; }
    /// <summary>
    /// The user id of the broadcaster or a moderator in the broadcaster's chat.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId ModeratorId { get; set; }
}