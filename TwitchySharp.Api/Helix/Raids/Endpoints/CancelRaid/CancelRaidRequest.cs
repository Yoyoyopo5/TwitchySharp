using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Raids;
/// <summary>
/// Cancel a pending raid.
/// </summary>
/// <remarks>
/// You can cancel a raid at any point up until the broadcaster clicks Raid Now in the Twitch UX or the 90-second countdown expires.
/// <br/>
/// <b>Rate Limits:</b> You may cancel up to 10 raids within a 10-minute window.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ChannelManageRaids"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#cancel-a-raid">Cancel A Raid</see> for more information.
/// </remarks>
public record CancelRaidRequest : TwitchHelixRequest<CancelRaidResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManageRaids"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    public CancelRaidRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        CancelRaidRequestParameters parameters
        ) : base(
            "/raids",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
            )
    {
        Method = HttpMethod.Delete;
    }
}

/// <summary>
/// Request parameters for a <see cref="CancelRaidRequest"/>.
/// </summary>
public record CancelRaidRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster (channel) to cancel a pending raid for.
    /// </summary>
    public required UserId BroadcasterId { get; set; }
}
