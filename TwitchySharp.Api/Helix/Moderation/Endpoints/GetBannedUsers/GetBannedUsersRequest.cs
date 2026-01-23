using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Gets all users that the broadcaster banned or put in a timeout.
/// </summary>
/// <remarks>
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ModerationRead"/> or <see cref="Scope.ModeratorManageBannedUsers"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-banned-users">Get Banned Users</see> for more information.
/// </remarks>
public record GetBannedUsersRequest
    : TwitchHelixRequest<GetBannedUsersResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ModerationRead"/> or <see cref="Scope.ModeratorManageBannedUsers"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    public GetBannedUsersRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        GetBannedUsersRequestParameters parameters
        ) : base(
            "/moderation/banned",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("user_id", parameters.UserIds?.Select(x => x.Value))
                .Add("first", parameters.First?.ToString())
                .Add("after", parameters.After?.Value)
                .Add("before", parameters.Before?.Value)
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetBannedUsersRequest"/>.
/// </summary>
public record GetBannedUsersRequestParameters
    : IPageableRequest
{
    /// <summary>
    /// The user id of the broadcaster (channel) to get banned users for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; set; }
    /// <summary>
    /// A list of user ids used to filter the results.
    /// </summary>
    /// <remarks>
    /// You may specify a maximum of 100 IDs. 
    /// The returned list includes only those users that were banned or put in a timeout.
    /// The list is returned in the same order that you specified the ids.
    /// </remarks>
    public IEnumerable<UserId>? UserIds { get; set; }
    /// <remarks>
    /// The minimum page size is 1 item per page and the maximum is 100 items per page. 
    /// The default is 20.
    /// </remarks>
    public PaginationAmount? First { get; set; }
    public PaginationCursor? After { get; set; }
    /// <summary>
    /// The cursor of the result to get results before.
    /// </summary>
    public PaginationCursor? Before { get; set; }
}
