using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models.Helix.Moderation.Responses;
using TwitchySharp.Api.Models.Shared;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Moderation.Requests;
/// <summary>
/// Gets all users allowed to moderate the broadcaster’s chat room.
/// </summary>
/// <remarks>
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ModerationRead"/> or <see cref="Scope.ChannelManageModerators"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-moderators">Get Moderators</see> for more information.
/// </remarks>
public record GetModeratorsRequest
    : TwitchHelixRequest<GetModeratorsResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ModerationRead"/> or <see cref="Scope.ChannelManageModerators"/>.</param>
    /// <param name="broadcasterId">
    /// The user id of the broadcaster (channel) to get moderators for.
    /// This must be the same user that created the <paramref name="accessToken"/>.
    /// </param>
    /// <param name="userId">
    /// A list of user ids used to filter the results.
    /// You may specify a maximum of 100 IDs.
    /// The returned list includes only the users from the list who are moderators in the broadcaster’s channel. 
    /// The list is returned in the same order as you specified the ids.
    /// </param>
    /// <param name="first">
    /// The maximum number of items to return per page in the response. 
    /// The minimum page size is 1 item per page and the maximum is 100 items per page. 
    /// The default is 20.
    /// </param>
    /// <param name="after">
    /// The cursor used to get the next page of results. 
    /// The <see cref="Pagination"/> property in the response contains the cursor’s value.
    /// </param>
    public GetModeratorsRequest(
        string clientId,
        string accessToken,
        string broadcasterId,
        IEnumerable<string>? userId = null,
        int? first = null,
        string? after = null
        ) : base(
            "/moderation/moderators",
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
