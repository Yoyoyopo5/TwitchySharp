using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;

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
    /// <param name="broadcasterId">
    /// The user id of the broadcaster to end a Guest Star session for.
    /// This must be the same user that created the <paramref name="accessToken"/>.
    /// </param>
    /// <param name="sessionId">The id of the Guest Star session to end.</param>
    public EndGuestStarSessionRequest(
        string clientId,
        string accessToken,
        string broadcasterId,
        string sessionId
        ) : base(
            "/guest_star/session",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
                .Add("session_id", sessionId)
            )
    {
        Method = HttpMethod.Delete;
    }
}
