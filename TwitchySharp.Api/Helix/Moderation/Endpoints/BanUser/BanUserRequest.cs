using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Helpers.JsonConverters;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Bans a user from participating in the specified broadcaster's chat room or puts them in a timeout.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageBannedUsers"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#ban-user">Ban User</see> for more information.
/// </remarks>
public record BanUserRequest
    : TwitchHelixRequest<BanUserResponse>
{
    protected override string Path => "/moderation/bans";
    public override HttpMethod Method => HttpMethod.Post;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(ModeratorId);
    public override IEnumerable<Scope> ValidScopes => [ Scope.ModeratorManageBannedUsers ];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("moderator_id", ModeratorId);
    public override object? ContentObject => Ban;

    /// <summary>
    /// The user id of the broadcaster (channel) to ban or time out a user from.
    /// </summary>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId ModeratorId { get; init; }

    /// <summary>
    /// Information used to set the user to ban or time out.
    /// </summary>
    public required BanUserRequestData Ban { get; init; }
}

/// <summary>
/// Information used to ban or time out a specific user from a channel.
/// </summary>
public record BanUserRequestData
{
    /// <summary>
    /// Information about the specific user to ban or time out.
    /// </summary>
    public required UserToBan Data { get; init; } // Really Twitch?
}

/// <summary>
/// Contains information about a specific user to ban or time out.
/// </summary>
public record UserToBan
{
    /// <summary>
    /// The user id of the user to ban or time out.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// Set this property to issue a time-out, leave <see langword="null"/> to issue a ban.
    /// Time-out durations are measured in <b>seconds</b>, with the minimum duration being 1 second, and the maximum being 1,209,600 seconds (2 weeks).
    /// Note that time-outs overwrite each other. You can use this property to end a user's time-out by setting it 1 second.
    /// Also note that adding a time-out duration to a user does not overwrite a ban if they have one.
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    public TimeSpan? Duration { get; init; }
    /// <summary>
    /// Caller-defined text that is displayed to the banned user as the reason for their ban or time-out.
    /// </summary>
    public string? Reason { get; init; }
}
