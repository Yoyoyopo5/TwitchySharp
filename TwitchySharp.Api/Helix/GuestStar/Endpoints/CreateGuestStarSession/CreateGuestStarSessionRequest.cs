using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.GuestStar;
/// <summary>
/// <b>BETA</b> Creates a Guest Star session on behalf of the broadcaster. 
/// </summary>
/// <remarks>
/// Requires the broadcaster to be present in the call interface, or the call will be ended automatically.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ChannelManageGuestStar"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#create-guest-star-session">Create Guest Star Session</see> for more information.
/// </remarks>
public record CreateGuestStarSessionRequest
    : TwitchHelixRequest<CreateGuestStarSessionResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManageGuestStar"/>.</param>
    /// <param name="broadcasterId">
    /// The user id of the broadcaster to create a Guest Star session for.
    /// This must be the same user who created the <paramref name="accessToken"/>.
    /// </param>
    public CreateGuestStarSessionRequest(
            string clientId,
            string accessToken,
            string broadcasterId
        ) : base(
        "/guest_star/session",
        clientId,
        accessToken,
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
        )
    {
        Method = HttpMethod.Post;
    }
}
