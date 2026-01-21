using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Channels;
/// <summary>
/// Gets a list of the broadcaster’s VIPs.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadVips"/> or <see cref="Scope.ChannelManageVips"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-vips">Get VIPs</see> for more information.
/// </remarks>
public record GetVipsRequest
    : TwitchHelixRequest<GetVipsResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelReadVips"/> or <see cref="Scope.ChannelManageVips"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    public GetVipsRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        GetVipsRequestParameters parameters
        )
        : base(
            "/channels/vips",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("user_id", parameters.UserIds?.Select(x => x.ToString()))
                .Add("first", parameters.First?.ToString())
                .Add("after", parameters.After?.Value)
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetVipsRequest"/>.
/// </summary>
public record GetVipsRequestParameters
    : IPageableRequest
{
    /// <summary>
    /// The user id of the broadcaster (channel) to get VIPs for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; set; }
    /// <summary>
    /// Filter the list by specific users.
    /// </summary>
    /// <remarks>
    /// The maximum number of ids that you may specify is 100. 
    /// Ignores the ids of users that aren’t VIPs on the broadcaster's channel.
    /// </remarks>
    public IEnumerable<UserId>? UserIds { get; set; }
    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 item per page and the maximum is 100. 
    /// The default is 20.
    /// </remarks>
    public PaginationAmount? First { get; set; }
    public PaginationCursor? After { get; set; }
}
