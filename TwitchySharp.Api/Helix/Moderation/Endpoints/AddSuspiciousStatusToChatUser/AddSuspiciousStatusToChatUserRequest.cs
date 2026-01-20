using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// <b>BETA</b> Adds a suspicious user status to a chatter on the broadcaster’s channel.
/// </summary>
/// <remarks>
/// Requires an app or user access token that includes <see cref="Scope.ModeratorManageSuspiciousUsers"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#add-suspicious-status-to-chat-user">Add Suspicious Status to Chat User</see> for more information.
/// </remarks>
public record AddSuspiciousStatusToChatUserRequest
    : TwitchHelixRequest<AddSuspiciousStatusToChatUserResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An app or user access token that includes <see cref="Scope.ModeratorManageSuspiciousUsers"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    /// <param name="data">The request data.</param>
    public AddSuspiciousStatusToChatUserRequest(
        string clientId,
        string accessToken,
        AddSuspiciousStatusToChatUserRequestParameters parameters,
        AddSuspiciousStatusToChatUserRequestData data
        )
        : base(
            "/moderation/suspicious_users",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("moderator_id", parameters.ModeratorId)
            )
    {
        Method = HttpMethod.Post;
        ContentObject = data;
    }
}

/// <summary>
/// Request parameters for <see cref="AddSuspiciousStatusToChatUserRequest"/>.
/// </summary>
public record AddSuspiciousStatusToChatUserRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster (channel) in whose chat the suspicious user status is being applied.
    /// </summary>
    public required string BroadcasterId { get; set; }
    /// <summary>
    /// The user id of the moderator (or the broadcaster) to update the suspicious user status on behalf of.
    /// This should be the same user that created the user access token for the request.
    /// </summary>
    public required string ModeratorId { get; set; }
}

/// <summary>
/// Contains data used with a <see cref="AddSuspiciousStatusToChatUserRequest"/>.
/// </summary>
public record AddSuspiciousStatusToChatUserRequestData
{
    /// <summary>
    /// The id of the user to add suspicious user status to.
    /// </summary>
    public required string UserId { get; set; }
    /// <summary>
    /// The type of suspicious user status to add.
    /// </summary>
    public required SuspiciousUserStatus Status { get; set; }
}