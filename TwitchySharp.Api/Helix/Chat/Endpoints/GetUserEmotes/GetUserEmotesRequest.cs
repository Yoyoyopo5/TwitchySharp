using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Retrieves emotes available to the user across all channels.
/// </summary>
/// <remarks>
/// Requires a user access token with <see cref="Scope.UserReadEmotes"/> and the access token must belong to the user you are requesting emotes for.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-user-emotes">Get User Emotes</see> for more information.
/// </remarks>
public record GetUserEmotesRequest
    : TwitchHelixRequest<GetUserEmotesResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.UserReadEmotes"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    public GetUserEmotesRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        GetUserEmotesRequestParameters parameters
        )
        : base(
            "/chat/emotes/user",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("user_id", parameters.UserId)
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("after", parameters.After?.Value)
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetUserEmotesRequest"/>.
/// </summary>
public record GetUserEmotesRequestParameters
{
    /// <summary>
    /// The user id of the user you want to get emotes for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId UserId { get; set; }
    /// <summary>
    /// The user id of a broadcaster you wish to get follower emotes of. 
    /// </summary>
    /// <remarks>
    /// Using this query parameter will guarantee inclusion of the broadcaster’s follower emotes in the response body.
    /// <b>Note:</b> If the user specified in <see cref="UserId"/> is subscribed to the broadcaster specified, their follower emotes will appear in the response body regardless if this query parameter is used.
    /// </remarks>
    public UserId? BroadcasterId { get; set; }
    public PaginationCursor? After { get; set; } // Not sure why first is not in the docs for this one.
}
