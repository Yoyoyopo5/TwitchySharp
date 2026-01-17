using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models.Helix.Raids.Responses;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Raids.Requests;
/// <summary>
/// Raid another channel by sending the broadcaster’s viewers to the targeted channel.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelManageRaids"/>. 
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#start-a-raid">Start A Raid</see> for more information.
/// </remarks>
public record StartRaidRequest : TwitchHelixRequest<StartRaidResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManageRaids"/>.</param>
    /// <param name="fromBroadcasterId">
    /// The user id of the broadcaster (channel) that is sending the raid.
    /// This must be the same user that created the <paramref name="accessToken"/>.
    /// </param>
    /// <param name="toBroadcasterId">The user id of the broadcaster (channel) to send the raid to.</param>
    public StartRaidRequest(
        string clientId,
        string accessToken,
        string fromBroadcasterId,
        string toBroadcasterId
        ) : base(
            "/raids",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("from_broadcaster_id", fromBroadcasterId)
                .Add("to_broadcaster_id", toBroadcasterId)
            )
    {
        Method = HttpMethod.Post;
    }
}
