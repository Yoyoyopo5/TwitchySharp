using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Channels;
/// <summary>
/// Gets a list of users that follow the specified broadcaster. 
/// You can also use this endpoint to see whether a specific user follows the broadcaster.
/// </summary>
/// <remarks>
/// For detailed follower information, the access token must be of a user that is either
/// 1) The broadcaster, or
/// 2) A moderator in the broadcaster's channel.
/// Otherwise, only the <see cref="GetChannelFollowersResponse.Total"/> will be provided.
/// <br/>
/// Requires a user access token with <see cref="Scope.ModeratorReadFollowers"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-channel-followers">Get Channel Followers</see> for more information.
/// </remarks>
public record GetChannelFollowersRequest
    : TwitchHelixRequest<GetChannelFollowersResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token with <see cref="Scope.ModeratorReadFollowers"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    public GetChannelFollowersRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        GetChannelFollowersRequestParameters parameters
        )
        : base(
            "/channels/followers",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("user_id", parameters.UserId)
                .Add("first", parameters.First?.ToString())
                .Add("after", parameters.After?.Value)
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetChannelFollowersRequest"/>.
/// </summary>
public record GetChannelFollowersRequestParameters
    : IPageableRequest
{
    /// <summary>
    /// The user id of the broadcaster to get followers for.
    /// </summary>
    public required UserId BroadcasterId { get; set; }
    /// <summary>
    /// Use this parameter to see whether a specific user follows this broadcaster. 
    /// </summary>
    /// <remarks>
    /// If specified, the response contains this user if they follow the broadcaster. 
    /// If not specified, the response contains all users that follow the broadcaster.
    /// </remarks>
    public UserId? UserId { get; set; }
    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 item per page and the maximum is 100. 
    /// </remarks>
    public PaginationAmount? First { get; set; }
    public PaginationCursor? After { get; set; }
}
