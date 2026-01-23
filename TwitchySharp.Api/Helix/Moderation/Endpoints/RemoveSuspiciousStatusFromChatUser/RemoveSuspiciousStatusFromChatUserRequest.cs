using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// <b>BETA</b> Remove a suspicious user status from a chatter on broadcaster’s channel.
/// </summary>
/// <remarks>
/// Requires an app or user access token that includes <see cref="Scope.ModeratorManageSuspiciousUsers"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#remove-suspicious-status-from-chat-user">Remove Suspicious Status from Chat User</see> for more information.
/// </remarks>
public record RemoveSuspiciousStatusFromChatUserRequest
    : TwitchHelixRequest<RemoveSuspiciousStatusFromChatUserResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An app or user access token that includes <see cref="Scope.ModeratorManageSuspiciousUsers"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    public RemoveSuspiciousStatusFromChatUserRequest(
        ClientId clientId,
        AccessToken accessToken,
        RemoveSuspiciousStatusFromChatUserRequestParameters parameters
        )
        : base(
            "/moderation/suspicious_users",
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
/// Request parameters for <see cref="RemoveSuspiciousStatusFromChatUserRequest"/>.
/// </summary>
public record RemoveSuspiciousStatusFromChatUserRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster (channel) in whose chat to remove the suspicious user status.
    /// </summary>
    public required UserId BroadcasterId { get; set; }
    /// <summary>
    /// The user id of the moderator (or the broadcaster) to remove the suspicious user status on behalf of.
    /// </summary>
    public required UserId ModeratorId { get; set; }
    /// <summary>
    /// The id of the user to remove the suspicious user status on.
    /// </summary>
    public required UserId UserId { get; set; }
}