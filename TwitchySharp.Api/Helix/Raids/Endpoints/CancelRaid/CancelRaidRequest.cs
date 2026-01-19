using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;

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
    /// <param name="broadcasterId">The user id of the broadcaster (channel) to cancel a pending raid for.</param>
    public CancelRaidRequest(
        string clientId,
        string accessToken,
        string broadcasterId
        ) : base(
            "/raids",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
            )
    {
        Method = HttpMethod.Delete;
    }
}
