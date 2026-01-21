using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

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
    /// <param name="parameters">The request parameters.</param>
    public SendShoutoutRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        SendShoutoutRequestParameters parameters
        )
        : base(
            "/chat/shoutouts",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("from_broadcaster_id", parameters.FromBroadcasterId)
                .Add("to_broadcaster_id", parameters.ToBroadcasterId)
                .Add("moderator_id", parameters.ModeratorId)
            )
    {
        Method = HttpMethod.Post;
    }
}
/// <summary>
/// Request parameters for a <see cref="SendShoutoutRequest"/>.
/// </summary>
public record SendShoutoutRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster that's sending the shoutout.
    /// </summary>
    public required UserId FromBroadcasterId { get; set; }
    /// <summary>
    /// The user id of the broadcaster that's receiving the shoutout.
    /// </summary>
    public required UserId ToBroadcasterId { get; set; }
    /// <summary>
    /// The user id of the moderator (or the broadcaster) to send the shoutout on behalf of.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId ModeratorId { get; set; }
}
