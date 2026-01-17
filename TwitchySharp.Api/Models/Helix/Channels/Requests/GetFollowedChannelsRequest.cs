using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models.Helix.Channels.Responses;
using TwitchySharp.Api.Models.Shared;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Channels.Requests;
/// <summary>
/// Gets a list of broadcasters that the specified user follows. 
/// You can also use this endpoint to see whether a user follows a specific broadcaster.
/// </summary>
/// <remarks>
/// Requires a user access token with <see cref="Scope.UserReadFollows"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-followed-channels">Get Followed Channels</see> for more information.
/// </remarks>
public record GetFollowedChannelsRequest
    : TwitchHelixRequest<GetFollowedChannelsResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token with <see cref="Scope.UserReadFollows"/>.</param>
    /// <param name="userId">
    /// The user ID of the user to get follows for. 
    /// This ID must match the user ID in the <paramref name="accessToken"/>.
    /// </param>
    /// <param name="broadcasterId">
    /// A broadcaster’s user ID. 
    /// Use this parameter to see whether the user follows this broadcaster. 
    /// If specified, the response contains this broadcaster if the user follows them. 
    /// If not specified, the response contains all broadcasters that the user follows.
    /// </param>
    /// <param name="first">
    /// The maximum number of items to return per page in the response. 
    /// The minimum page size is 1 item per page and the maximum is 100. 
    /// The default is 20.
    /// </param>
    /// <param name="after">
    /// The cursor used to get the next page of results. 
    /// The <see cref="Pagination"/> property in the response contains the cursor’s value.
    /// </param>
    public GetFollowedChannelsRequest(
        string clientId,
        string accessToken,
        string userId,
        string? broadcasterId = null,
        int? first = null,
        string? after = null
        )
        : base(
            "/channels/followed",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("user_id", userId)
                .Add("broadcaster_id", broadcasterId)
                .Add("first", first?.ToString())
                .Add("after", after)
            )
    {
        Method = HttpMethod.Get;
    }
}
