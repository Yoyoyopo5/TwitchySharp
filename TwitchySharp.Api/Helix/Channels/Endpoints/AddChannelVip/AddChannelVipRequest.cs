using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Channels;
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
    /// <param name="parameters">The request parameters.</param>
    public AddChannelVipRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        AddChannelVipRequestParameters parameters
        )
        : base(
            "/channels/vips",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("user_id", parameters.UserId)
            )
    {
        Method = HttpMethod.Post;
    }
}

/// <summary>
/// Request parameters for a <see cref="AddChannelVipRequest"/>.
/// </summary>
public record AddChannelVipRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster (channel) to add a VIP for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; set; }
    /// <summary>
    /// The id of the user to give VIP status to.
    /// </summary>
    public required UserId UserId { get; set; }
}
