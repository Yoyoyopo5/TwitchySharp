using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Bans a user from participating in the specified broadcaster’s chat room or puts them in a timeout.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#ban-user">ban user</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageBannedUsers"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorManageBannedUsers"/>.</param>
/// <param name="broadcasterId">The user id of the broadcaster (channel) to ban or time out a user from.</param>
/// <param name="moderatorId">
/// The user id of the broadcaster or a moderator of the broadcaster's channel.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="ban">Information used to set the user to ban or time out.</param>
public class BanUserRequest(
    string clientId,
    string accessToken,
    string broadcasterId,
    string moderatorId,
    BanUserRequestData ban
    )
    : HelixApiRequest<BanUserResponse, BanUserRequestData>(
        "/moderation/bans" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("moderator_id", moderatorId),
        clientId,
        accessToken,
        ban
        );

/// <summary>
/// Information used to ban or time out a specific user from a channel.
/// </summary>
public record BanUserRequestData
{
    /// <summary>
    /// The user id of the user to ban or time out.
    /// </summary>
    public required string UserId { get; set; }
    /// <summary>
    /// Set this property to issue a time-out, leave <see langword="null"/> to issue a ban.
    /// Time-out durations are measured in <b>seconds</b>, with the minimum duration being 1 second, and the maximum being 1,209,600 seconds (2 weeks).
    /// Note that time-outs overwrite each other. You can use this property to end a user's time-out by setting it 1 second.
    /// Also note that adding a time-out duration to a user does not overwrite a ban if they have one.
    /// </summary>
    public int? Duration { get; set; }
    /// <summary>
    /// Caller-defined text that is displayed to the banned user as the reason for their ban or time-out.
    /// </summary>
    public string? Reason { get; set; }
}
