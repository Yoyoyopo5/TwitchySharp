using System.Text.Json.Serialization;
using TwitchySharp.Serialization;

namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelAdBreakBegin"/> event.
/// </summary>
public record ChannelAdBreakBeginEvent
{
    /// <summary>
    /// Length of the mid-roll ad break requested.
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    [JsonPropertyName("duration_seconds")]
    public required TimeSpan Duration { get; init; }
    /// <summary>
    /// The date and time when the ad break began.
    /// </summary>
    /// <remarks>
    /// Note that there is a potential delay between this event, when the streamer requested the ad break, and when the viewers will see ads.
    /// </remarks>
    public required DateTimeOffset StartedAt { get; init; }
    /// <summary>
    /// Indicates if the ad was automatically scheduled via the Ads Manager.
    /// </summary>
    public required bool IsAutomatic { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that the ad break began in.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the ad break began in.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the ad break began in.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The id of the user that requested the ad break.
    /// For ad breaks where <see cref="IsAutomatic"/> is <see langword="true"/>, this will be the same as the <see cref="BroadcasterUserId"/>.
    /// </summary>
    public required UserId RequesterUserId { get; init; }
    /// <summary>
    /// The login (username) of the user that requested the ad break.
    /// </summary>
    public required UserLogin RequesterUserLogin { get; init; }
    /// <summary>
    /// The display name of the user that requested the ad break.
    /// </summary>
    public required UserName RequesterUserName { get; init; }
}
