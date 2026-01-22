using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.GuestStar;
/// <summary>
/// <b>BETA</b> Gets information about an ongoing Guest Star session for a particular channel.
/// </summary>
/// <remarks>
/// Requires a user access token that includes one of <see cref="Scope.ChannelReadGuestStar"/>, <see cref="Scope.ChannelManageGuestStar"/>, <see cref="Scope.ModeratorReadGuestStar"/>, or <see cref="Scope.ModeratorManageGuestStar"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-guest-star-session">Get Guest Star Session</see> for more information.
/// </remarks>
public record GetGuestStarSessionRequest
    : TwitchHelixRequest<GetGuestStarSessionResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes one of <see cref="Scope.ChannelReadGuestStar"/>, <see cref="Scope.ChannelManageGuestStar"/>, <see cref="Scope.ModeratorReadGuestStar"/>, or <see cref="Scope.ModeratorManageGuestStar"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    public GetGuestStarSessionRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        GetGuestStarSessionRequestParameters parameters
        ) : base(
            "/guest_star/session",
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
/// Request parameters for a <see cref="GetGuestStarSessionRequest"/>.
/// </summary>
public record GetGuestStarSessionRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster hosting the Guest Star session.
    /// </summary>
    public required UserId BroadcasterId { get; set; }
    /// <summary>
    /// The user id of the broadcaster or a moderator in the broadcaster's chat.
    /// </summary>
    /// <remarks>
    /// This user must be the one that created the access token in the request.
    /// </remarks>
    public required UserId ModeratorId { get; set; }
}
