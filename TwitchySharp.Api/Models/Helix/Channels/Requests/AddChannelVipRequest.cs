using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models.Helix.Channels.Responses;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Channels.Requests;
/// <summary>
/// Adds the specified user as a VIP in the broadcaster’s channel.
/// </summary>
/// <remarks>
/// <b>Rate Limits:</b> A broadcaster may add a maximum of 10 VIPs within a 10-second window.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ChannelManageVips"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#add-channel-vip">Add Channel VIP</see> for more information.
/// </remarks>
public record AddChannelVipRequest
    : TwitchHelixRequest<AddChannelVipResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManageVips"/>.</param>
    /// <param name="broadcasterId">
    /// The user id of the broadcaster (channel) to add a VIP for.
    /// This must be the same user that created the <paramref name="accessToken"/>.
    /// </param>
    /// <param name="userId">The id of the user to give VIP status to.</param>
    public AddChannelVipRequest(
        string clientId,
        string accessToken,
        string broadcasterId,
        string userId
        )
        : base(
            "/channels/vips",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
                .Add("user_id", userId)
            )
    {
        Method = HttpMethod.Post;
    }
}
