using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

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
    /// <param name="parameters">The request parameters.</param>
    public CreateGuestStarSessionRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        CreateGuestStarSessionRequestParameters parameters
        ) : base(
        "/guest_star/session",
        clientId,
        accessToken,
        new HttpQueryParameters()
            .Add("broadcaster_id", parameters.BroadcasterId)
        )
    {
        Method = HttpMethod.Post;
    }
}

/// <summary>
/// Request parameters for a <see cref="CreateGuestStarSessionRequest"/>.
/// </summary>
public record CreateGuestStarSessionRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster to create a Guest Star session for.
    /// </summary>
    /// <remarks>
    /// This must be the same user who created the access token used in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; set; }
}