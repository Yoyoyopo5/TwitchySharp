using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Streams;
/// <summary>
/// Gets the list of broadcasters that the user follows and that are streaming live.
/// </summary>
/// <remarks>
/// <br/>
/// Requires a user access token that includes <see cref="Scope.UserReadFollows"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-followed-streams">Get Followed Streams</see> for more information.
/// </remarks>
public record GetFollowedStreamsRequest
    : TwitchHelixRequest<GetFollowedStreamsResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.UserReadFollows"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    public GetFollowedStreamsRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        GetFollowedStreamsRequestParameters parameters
        ) : base(
            "/streams/followed",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("user_id", parameters.UserId)
                .Add("first", parameters.First?.ToString())
                .Add("after", parameters.After?.Value)
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetFollowedStreamsRequest"/>.
/// </summary>
public record GetFollowedStreamsRequestParameters
    : IPageableRequest
{
    /// <summary>
    /// The id of the user to get followed streams for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the <paramref name="accessToken"/>.
    /// </remarks>
    public required UserId UserId { get; set; }
    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 item per page and the maximum is 100 items per page. 
    /// The default is 100.
    /// </remarks>
    public PaginationAmount? First { get; set; }
    public PaginationCursor? After { get; set; }
}
