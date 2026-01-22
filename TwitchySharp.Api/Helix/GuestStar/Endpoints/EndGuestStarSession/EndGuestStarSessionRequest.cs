using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.GuestStar;
/// <summary>
/// <b>BETA</b> Ends a Guest Star session on behalf of the broadcaster. 
/// </summary>
/// <remarks>
/// Performs the same action as if the host clicked the "End Call" button in the Guest Star UI.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ChannelManageGuestStar"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#end-guest-star-session">End Guest Star Session</see> for more information.
/// </remarks>
public record EndGuestStarSessionRequest
    : TwitchHelixRequest<EndGuestStarSessionResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManageGuestStar"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    public EndGuestStarSessionRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        EndGuestStarSessionRequestParameters parameters
        ) : base(
            "/guest_star/session",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("session_id", parameters.SessionId)
            )
    {
        Method = HttpMethod.Delete;
    }
}

/// <summary>
/// Request parameters for a <see cref="EndGuestStarSessionRequest"/>.
/// </summary>
public record EndGuestStarSessionRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster to end a Guest Star session for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; set; }
    /// <summary>
    /// The id of the Guest Star session to end.
    /// </summary>
    public required GuestStarSessionId SessionId { get; set; }
}
