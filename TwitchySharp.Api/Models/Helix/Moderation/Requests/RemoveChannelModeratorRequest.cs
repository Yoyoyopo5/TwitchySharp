using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models.Helix.Moderation.Responses;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Moderation.Requests;
/// <summary>
/// Removes a moderator from the broadcaster’s chat room.
/// </summary>
/// <remarks>
/// <b>Rate Limits:</b> A broadcaster may remove a maximum of 10 moderators within a 10-second window.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ChannelManageModerators"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#remove-channel-moderator">Remove Channel Moderator</see> for more information.
/// </remarks>
public record RemoveChannelModeratorRequest
    : TwitchHelixRequest<RemoveChannelModeratorResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManageModerators"/>.</param>
    /// <param name="broadcasterId">
    /// The user id of the broadcaster (channel) to remove a moderator for.
    /// This must be the same user that created the <paramref name="accessToken"/>.
    /// </param>
    /// <param name="userId">
    /// The user id of the moderator to remove from the broadcaster's channel.
    /// </param>
    public RemoveChannelModeratorRequest(
        string clientId,
        string accessToken,
        string broadcasterId,
        string userId
        ) : base(
            "/moderation/moderators",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
                .Add("user_id", userId)
            )
    {
        Method = HttpMethod.Delete;
    }
}
