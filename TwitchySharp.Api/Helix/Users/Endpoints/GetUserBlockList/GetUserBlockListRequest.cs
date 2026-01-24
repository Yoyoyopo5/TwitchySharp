using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Gets the list of users that the broadcaster has blocked.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.UserReadBlockedUsers"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-user-block-list">Get User Block List</see> for more information.
/// </remarks>
public record GetUserBlockListRequest
    : TwitchHelixRequest<GetUserBlockListResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.UserReadBlockedUsers"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    public GetUserBlockListRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        GetUserBlockListRequestParameters parameters
        ) : base(
            "/users/blocks",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("first", parameters.First?.ToString())
                .Add("after", parameters.After?.Value)
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetUserBlockListRequest"/>.
/// </summary>
public record GetUserBlockListRequestParameters
    : IPageableRequest
{
    /// <summary>
    /// The user id of the broadcaster to get blocked users for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; set; }
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
