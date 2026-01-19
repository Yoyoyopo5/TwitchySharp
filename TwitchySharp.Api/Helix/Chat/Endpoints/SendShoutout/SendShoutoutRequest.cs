using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Sends a Shoutout to the specified broadcaster. See <see href="https://help.twitch.tv/s/article/shoutouts">Shoutouts</see>.
/// </summary>
/// <remarks>
/// A broadcaster may send a Shoutout once every 2 minutes. They may send the same broadcaster a Shoutout once every 60 minutes.
/// <br/>
/// Requires a user access token that inlcudes <see cref="Scope.ModeratorManageShoutouts"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#send-a-shoutout">Send a Shoutout</see> for more information.
/// </remarks>
public record SendShoutoutRequest
    : TwitchHelixRequest<SendShoutoutResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorManageShoutouts"/>.</param>
    /// <param name="fromBroadcasterId">The user id of the broadcaster that's sending the shoutout.</param>
    /// <param name="toBroadcasterId">The user id of the broadcaster that's receiving the shoutout.</param>
    /// <param name="moderatorId">
    /// The user id of the sending broadcaster or a moderator of the broadcaster's channel. 
    /// This must be the same user that created the <paramref name="accessToken"/>.
    /// </param>
    public SendShoutoutRequest(
        string clientId,
        string accessToken,
        string fromBroadcasterId,
        string toBroadcasterId,
        string moderatorId
        )
        : base(
            "/chat/shoutouts",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("from_broadcaster_id", fromBroadcasterId)
                .Add("to_broadcaster_id", toBroadcasterId)
                .Add("moderator_id", moderatorId)
            )
    {
        Method = HttpMethod.Post;
    }
}
