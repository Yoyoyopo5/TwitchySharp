using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Warns a user in the specified broadcaster’s chat room, preventing them from chat interaction until the warning is acknowledged.
/// </summary>
/// <remarks>
/// New warnings can be issued to a user when they already have a warning in the channel (new warning will replace old warning).
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageWarnings"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#warn-chat-user">Warn Chat User</see> for more information.
/// </remarks>
public record WarnChatUserRequest
    : TwitchHelixRequest<WarnChatUserResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorManageWarnings"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    /// <param name="warning">The warning data.</param>
    public WarnChatUserRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        WarnChatUserRequestParameters parameters,
        WarnChatUserRequestData warning
        ) : base(
            "/moderation/warnings",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("moderator_id", parameters.ModeratorId)
            )
    {
        Method = HttpMethod.Post;
        ContentObject = warning;
    }
}

/// <summary>
/// Request parameters for a <see cref="WarnChatUserRequest"/>.
/// </summary>
public record WarnChatUserRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster (channel) to issue to warning in.
    /// </summary>
    public required UserId BroadcasterId { get; set; }
    /// <summary>
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId ModeratorId { get; set; }
}

/// <summary>
/// Request data for a <see cref="WarnChatUserRequest"/>.
/// </summary>
public record WarnChatUserRequestData
{
    /// <summary>
    /// The warning data.
    /// </summary>
    public required ChatUserWarning Data { get; set; }
}

/// <summary>
/// Contains information about a specific chat warning.
/// </summary>
public record ChatUserWarning
{
    /// <summary>
    /// The id of the user to warn.
    /// </summary>
    public required UserId UserId { get; set; }
    /// <summary>
    /// The custom reason for the warning.
    /// </summary>
    /// <remarks>
    /// Can be a maximum of 500 characters.
    /// </remarks>
    public required string Reason { get; set; }
}
