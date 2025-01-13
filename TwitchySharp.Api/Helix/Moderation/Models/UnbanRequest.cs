using System;
using System.Text.Json.Serialization;
using TwitchySharp.Helpers.JsonConverters;

namespace TwitchySharp.Api.Helix.Moderation;

/// <summary>
/// Contains information about a specific unban request.
/// </summary>
public record UnbanRequest
{
    /// <summary>
    /// The id of the unban request.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that received the unban request.
    /// </summary>
    public required string BroadcasterId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that received the unban request.
    /// </summary>
    public required string BroadcasterName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that received the unban request.
    /// </summary>
    public required string BroadcasterLogin { get; init; }
    /// <summary>
    /// The user id of the moderator who approved or denied the request.
    /// </summary>
    public string? ModeratorId { get; init; }
    /// <summary>
    /// The login (username) of the moderator who approved or denied the request.
    /// </summary>
    public string? ModeratorLogin { get; init; }
    /// <summary>
    /// The display name of the moderator who approved or denied the request.
    /// </summary>
    public string? ModeratorName { get; init; }
    /// <summary>
    /// The user id of the banned user who created the request.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the banned user who created the request.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the banned user who created the request.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The text (written by the banned user) of the unban request.
    /// </summary>
    public required string Text { get; init; }
    /// <summary>
    /// The status of the unban request.
    /// </summary>
    [JsonConverter(typeof(SnakeCaseLowerJsonStringEnumConverter<UnbanRequestStatus>))]
    public required UnbanRequestStatus Status { get; init; }
    /// <summary>
    /// The date and time when the unban request was created.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }
    /// <summary>
    /// The date and time when the moderator approved or denied the request.
    /// </summary>
    public DateTimeOffset? ResolvedAt { get; init; }
    /// <summary>
    /// The text written by the moderator of a resolved unban request.
    /// </summary>
    public string? ResolutionText { get; init; }
}
