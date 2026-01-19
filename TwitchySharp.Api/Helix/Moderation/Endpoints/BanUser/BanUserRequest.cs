using System;
using System.Net.Http;
using System.Text.Json.Serialization;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Helpers.JsonConverters;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Bans a user from participating in the specified broadcaster’s chat room or puts them in a timeout.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageBannedUsers"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#ban-user">Ban User</see> for more information.
/// </remarks>
public record BanUserRequest
    : TwitchHelixRequest<BanUserResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorManageBannedUsers"/>.</param>
    /// <param name="broadcasterId">The user id of the broadcaster (channel) to ban or time out a user from.</param>
    /// <param name="moderatorId">
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// This must be the same user that created the <paramref name="accessToken"/>.
    /// </param>
    /// <param name="ban">Information used to set the user to ban or time out.</param>
    public BanUserRequest(
        string clientId,
        string accessToken,
        string broadcasterId,
        string moderatorId,
        BanUserRequestData ban
        ) : base(
            "/moderation/bans",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
                .Add("moderator_id", moderatorId)
            )
    {
        Method = HttpMethod.Post;
        ContentObject = ban;
    }
}

/// <summary>
/// Information used to ban or time out a specific user from a channel.
/// </summary>
public record BanUserRequestData
{
    /// <summary>
    /// Information about the specific user to ban or time out.
    /// </summary>
    public required UserToBan Data { get; set; } // Really Twitch?
}

/// <summary>
/// Contains information about a specific user to ban or time out.
/// </summary>
public record UserToBan
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
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    public TimeSpan? Duration { get; set; }
    /// <summary>
    /// Caller-defined text that is displayed to the banned user as the reason for their ban or time-out.
    /// </summary>
    public string? Reason { get; set; }
}
