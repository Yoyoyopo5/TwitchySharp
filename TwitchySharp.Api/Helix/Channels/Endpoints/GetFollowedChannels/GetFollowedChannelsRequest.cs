using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Channels;
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
    /// <param name="parameters">The request parameters.</param>
    public GetFollowedChannelsRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        GetFollowedChannelsRequestParameters parameters
        )
        : base(
            "/channels/followed",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("user_id", parameters.UserId)
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("first", parameters.First?.ToString())
                .Add("after", parameters.After?.Value)
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetFollowedChannelsRequest"/>.
/// </summary>
public record GetFollowedChannelsRequestParameters
    : IPageableRequest
{
    /// <summary>
    /// The id of the user to get follows for. 
    /// </summary>
    /// <remarks>
    /// This must be the user that created the access token used in the request.
    /// </remarks>
    public required UserId UserId { get; set; }
    /// <summary>
    /// Use this parameter to see whether the user follows a specific broadcaster. 
    /// If specified, the response contains this broadcaster if the user follows them. 
    /// If not specified, the response contains all broadcasters that the user follows.
    /// </summary>
    public UserId? BroadcasterId { get; set; }
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
