using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Blocks the specified user from interacting with or having contact with the broadcaster.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#block-user">block user</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.UserManageBlockedUsers"/>.
/// The user that created the access token identifies who is blocking the target user.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.UserManageBlockedUsers"/>.</param>
/// <param name="targetUserId">The id of the user to block. If the user is already blocked, the request is ignored.</param>
/// <param name="sourceContext">The location where the harassment took place that is causing the brodcaster to block the user.</param>
/// <param name="reason">The reason that the broadcaster is blocking the user.</param>
public class BlockUserRequest(
    string clientId,
    string accessToken,
    string targetUserId,
    BlockUserContext? sourceContext = null,
    BlockUserReason? reason = null
    )
    : HelixApiRequest<BlockUserResponse>(
        "/users/blocks" +
        new HttpQueryParameters()
            .Add("target_user_id", targetUserId)
            .Add("source_context", sourceContext?.Value)
            .Add("reason", reason?.Value),
        clientId,
        accessToken
        )
{
    public override HttpMethod Method => HttpMethod.Put;
}

/// <summary>
/// Contains static definitions for block contexts.
/// </summary>
/// <param name="Value"></param>
public record BlockUserContext(string Value)
    : ValueBackedEnum<string>(Value)
{
    public static BlockUserContext Chat { get; } = new("chat");
    public static BlockUserContext Whisper { get; } = new("whisper");
}

/// <summary>
/// Contains static definitions for block reasons.
/// </summary>
/// <param name="Value"></param>
public record BlockUserReason(string Value)
    : ValueBackedEnum<string>(Value)
{
    public static BlockUserReason Harassment { get; } = new("harassment");
    public static BlockUserReason Spam { get; } = new("spam");
    public static BlockUserReason Other { get; } = new("other");
}
