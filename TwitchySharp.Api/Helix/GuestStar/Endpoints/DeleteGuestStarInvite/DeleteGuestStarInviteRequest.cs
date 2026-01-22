using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.GuestStar;
/// <summary>
/// <b>BETA</b> Revokes a previously sent invite for a Guest Star session.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelManageGuestStar"/> or <see cref="Scope.ModeratorManageGuestStar"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#delete-guest-star-invite">Delete Guest Star Invite</see> for more information.
/// </remarks>
public record DeleteGuestStarInviteRequest
    : TwitchHelixRequest<DeleteGuestStarInviteResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManageGuestStar"/> or <see cref="Scope.ModeratorManageGuestStar"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    public DeleteGuestStarInviteRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        DeleteGuestStarInviteRequestParameters parameters
        ) : base(
            "/guest_star/invites",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("moderator_id", parameters.ModeratorId)
                .Add("session_id", parameters.SessionId)
                .Add("guest_id", parameters.GuestId)
            )
    {
        Method = HttpMethod.Delete;
    }
}

/// <summary>
/// Request parameters for a <see cref="DeleteGuestStarInviteRequest"/>.
/// </summary>
public record DeleteGuestStarInviteRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster hosting the Guest Star session.
    /// </summary>
    public required UserId BroadcasterId { get; set; }
    /// <summary>
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId ModeratorId { get; set; }
    /// <summary>
    /// The id of the Guest Star session that the invite was created for.
    /// </summary>
    public required GuestStarSessionId SessionId { get; set; }
    /// <summary>
    /// The user id of the user to revoke the invite for.
    /// </summary>
    public required UserId GuestId { get; set; }
}
