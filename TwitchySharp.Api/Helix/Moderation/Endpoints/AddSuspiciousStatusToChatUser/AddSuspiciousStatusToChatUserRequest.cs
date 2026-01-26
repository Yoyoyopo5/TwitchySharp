using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// <b>BETA</b> Adds a suspicious user status to a chatter on the broadcaster's channel.
/// </summary>
/// <remarks>
/// Requires an app or user access token that includes <see cref="Scope.ModeratorManageSuspiciousUsers"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#add-suspicious-status-to-chat-user">Add Suspicious Status to Chat User</see> for more information.
/// </remarks>
public record AddSuspiciousStatusToChatUserRequest
    : TwitchHelixRequest<AddSuspiciousStatusToChatUserResponse>
{
    protected override string Path => "/moderation/suspicious_users";
    public override HttpMethod Method => HttpMethod.Post;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(ModeratorId);
    public override IEnumerable<Scope> ValidScopes => [ Scope.ModeratorManageSuspiciousUsers ];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("moderator_id", ModeratorId);
    public override object? ContentObject => Data;

    /// <summary>
    /// The user id of the broadcaster (channel) in whose chat the suspicious user status is being applied.
    /// </summary>
    public required UserId BroadcasterId { get; set; }

    /// <summary>
    /// The user id of the moderator (or the broadcaster) to update the suspicious user status on behalf of.
    /// </summary>
    /// <remarks>
    /// This should be the same user that created the user access token for the request.
    /// </remarks>
    public required UserId ModeratorId { get; set; }

    /// <summary>
    /// The request data.
    /// </summary>
    public required AddSuspiciousStatusToChatUserRequestData Data { get; set; }
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