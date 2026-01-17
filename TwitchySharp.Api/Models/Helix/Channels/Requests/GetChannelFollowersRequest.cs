using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models.Helix.Channels.Responses;
using TwitchySharp.Api.Models.Shared;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Channels.Requests;
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
    /// <param name="broadcasterId">The user id of the broadcaster to get followers for.</param>
    /// <param name="userId">
    /// Use this parameter to see whether a specific user follows this broadcaster. 
    /// If specified, the response contains this user if they follow the broadcaster. 
    /// If not specified, the response contains all users that follow the broadcaster.</param>
    /// <param name="first">
    /// The maximum number of items to return per page in the response. 
    /// The minimum page size is 1 item per page and the maximum is 100. 
    /// The default is 20.
    /// </param>
    /// <param name="after">
    /// The cursor used to get the next page of results. 
    /// The <see cref="Pagination"/> property in the response contains the cursor’s value.
    /// </param>
    public GetChannelFollowersRequest(
        string clientId,
        string accessToken,
        string broadcasterId,
        string? userId = null,
        int? first = null,
        string? after = null
        )
        : base(
            "/channels/followers",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
                .Add("user_id", userId)
                .Add("first", first?.ToString())
                .Add("after", after)
            )
    {
        Method = HttpMethod.Get;
    }
}
