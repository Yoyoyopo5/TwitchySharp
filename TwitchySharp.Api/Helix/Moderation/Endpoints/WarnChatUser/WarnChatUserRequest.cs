using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;

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
    /// <param name="broadcasterId">The user id of the broadcaster (channel) to issue to warning in.</param>
    /// <param name="moderatorId">
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// This must be the same user that created the <paramref name="accessToken"/>.
    /// </param>
    /// <param name="warning">The warning data.</param>
    public WarnChatUserRequest(
        string clientId,
        string accessToken,
        string broadcasterId,
        string moderatorId,
        WarnChatUserRequestData warning
        ) : base(
            "/moderation/warnings",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
                .Add("moderator_id", moderatorId)
            )
    {
        Method = HttpMethod.Post;
        ContentObject = warning;
    }
}

public record WarnChatUserRequestData
{
    public required ChatUserWarning Data { get; set; }
}

public record ChatUserWarning
{
    public required string UserId { get; set; }
    public required string Reason { get; set; }
}
