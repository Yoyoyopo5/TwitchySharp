using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Removes the ban or timeout that was placed on the specified user.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageBannedUsers"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#unban-user">Unban User</see> for more information.
/// </remarks>
public record UnbanUserRequest : TwitchHelixRequest<UnbanUserResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorManageBannedUsers"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    public UnbanUserRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        UnbanUserRequestParameters parameters
    ) : base(
        "/moderation/bans",
        clientId,
        accessToken,
        new HttpQueryParameters()
            .Add("broadcaster_id", parameters.BroadcasterId)
            .Add("moderator_id", parameters.ModeratorId)
            .Add("user_id", parameters.UserId)
        )
    {
        Method = HttpMethod.Delete;
    }
}

/// <summary>
/// Request parameters for a <see cref="UnbanUserRequest"/>.
/// </summary>
public record UnbanUserRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster (channel) that the user will be unbanned on.
    /// </summary>
    public required UserId BroadcasterId { get; set; }
    /// <summary>
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId ModeratorId { get; set; }
    /// <summary>
    /// The user id of the user to unban or remove a time-out on.
    /// </summary>
    public required UserId UserId { get; set; }
}