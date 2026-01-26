using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Blocks the specified user from interacting with or having contact with the user.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.UserManageBlockedUsers"/>.
/// The user that created the access token identifies who is blocking the target user.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#block-user">Block User</see> for more information.
/// </remarks>
public record BlockUserRequest
    : TwitchHelixRequest<BlockUserResponse>
{
    protected override string Path => "/users/blocks";
    public override HttpMethod Method => HttpMethod.Put;
    protected override TwitchApiIdentity DefaultIdentity => TwitchApiIdentity.Default;
    public override IEnumerable<Scope> ValidScopes => [ Scope.UserManageBlockedUsers ];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("target_user_id", TargetUserId)
            .Add("source_context", SourceContext?.Value)
            .Add("reason", Reason?.Value);

    /// <summary>
    /// The id of the user to block.
    /// </summary>
    /// <remarks>
    /// If the user is already blocked, the request is ignored.
    /// </remarks>
    public required UserId TargetUserId { get; set; }
    /// <summary>
    /// The location where the harassment took place that is causing the brodcaster to block the user.
    /// </summary>
    public BlockUserContext? SourceContext { get; set; }
    /// <summary>
    /// The reason that the broadcaster is blocking the user.
    /// </summary>
    public BlockUserReason? Reason { get; set; }
}