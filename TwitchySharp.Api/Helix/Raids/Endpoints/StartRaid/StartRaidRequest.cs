using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Raids;
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
    /// <param name="parameters">The request parameters.</param>
    public StartRaidRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        StartRaidRequestParameters parameters
        ) : base(
            "/raids",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("from_broadcaster_id", parameters.FromBroadcasterId)
                .Add("to_broadcaster_id", parameters.ToBroadcasterId)
            )
    {
        Method = HttpMethod.Post;
    }
}

/// <summary>
/// Request parameters for a <see cref="StartRaidRequest"/>.
/// </summary>
public record StartRaidRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster (channel) that is sending the raid.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId FromBroadcasterId { get; set; }
    /// <summary>
    /// The user id of the broadcaster (channel) to send the raid to.
    /// </summary>
    public required UserId ToBroadcasterId { get; set; }
}
