using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;

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
    /// <param name="broadcasterId">The user id of the broadcaster (channel) that the user will be unbanned on.</param>
    /// <param name="moderatorId">
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// This must be the same user that created the <paramref name="accessToken"/>.
    /// </param>
    /// <param name="userId">The user id of the user to unban or remove a time-out on.</param>
    public UnbanUserRequest(
        string clientId,
        string accessToken,
        string broadcasterId,
        string moderatorId,
        string userId
    ) : base(
        "/moderation/bans",
        clientId,
        accessToken,
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("moderator_id", moderatorId)
            .Add("user_id", userId)
        )
    {
        Method = HttpMethod.Delete;
    }
}