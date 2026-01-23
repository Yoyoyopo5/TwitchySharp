using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Moderation;
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
    /// <param name="parameters">The request parameters.</param>
    public RemoveChannelModeratorRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        RemoveChannelModeratorRequestParameters parameters
        ) : base(
            "/moderation/moderators",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("user_id", parameters.UserId)
            )
    {
        Method = HttpMethod.Delete;
    }
}

/// <summary>
/// Request parameters for a <see cref="RemoveChannelModeratorRequest"/>.
/// </summary>
public record RemoveChannelModeratorRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster (channel) to remove a moderator for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; set; }
    /// <summary>
    /// The user id of the moderator to remove from the broadcaster's channel.
    /// </summary>
    public required UserId UserId { get; set; }
}
