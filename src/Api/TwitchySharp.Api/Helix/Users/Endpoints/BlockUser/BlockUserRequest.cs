using System.Collections.Immutable;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

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
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.User(UserId),
        ValidScopes = ImmutableHashSet.Create(Scope.UserManageBlockedUsers)
    };
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("target_user_id", TargetUserId)
            .Add("source_context", SourceContext?.Value)
            .Add("reason", Reason?.Value);

    /// <summary>
    /// The id of the user to block the target user as.
    /// </summary>
    public required UserId UserId { get; init; }

    /// <summary>
    /// The id of the user to block.
    /// </summary>
    /// <remarks>
    /// If the user is already blocked, the request is ignored.
    /// </remarks>
    public required UserId TargetUserId { get; init; }
    /// <summary>
    /// The location where the harassment took place that is causing the brodcaster to block the user.
    /// </summary>
    public BlockUserContext? SourceContext { get; init; }
    /// <summary>
    /// The reason that the broadcaster is blocking the user.
    /// </summary>
    public BlockUserReason? Reason { get; init; }

    protected override ValueTask<BlockUserResponse> ConvertResponseContent(Stream contentStream, CancellationToken ct = default)
        => ValueTask.FromResult(new BlockUserResponse());
}
