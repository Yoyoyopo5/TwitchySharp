using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Adds a moderator to the broadcaster’s chat room.
/// </summary>
/// <remarks>
/// <b>Rate Limits:</b> A broadcaster may add a maximum of 10 moderators within a 10-second window.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ChannelManageModerators"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#add-channel-moderator">Add Channel Moderator</see> for more information.
/// </remarks>
public record AddChannelModeratorRequest
    : TwitchHelixRequest<AddChannelModeratorResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManageModerators"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    public AddChannelModeratorRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        AddChannelModeratorRequestParameters parameters
        ) : base(
            "/moderation/moderators",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("user_id", parameters.UserId)
            )
    {
        Method = HttpMethod.Post;
    }
}

/// <summary>
/// Request parameters for a <see cref="AddChannelModeratorRequest"/>.
/// </summary>
public record AddChannelModeratorRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster (channel) to add a moderator for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; set; }

    /// <summary>
    /// The id of the user to add as a moderator.
    /// </summary>
    public required UserId UserId { get; set; }
}
