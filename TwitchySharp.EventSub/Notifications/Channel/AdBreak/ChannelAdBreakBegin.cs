using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Helpers.JsonConverters;
using TwitchySharp.EventSub.Models.Conditions;

namespace TwitchySharp.EventSub.Notifications.Channel.AdBreak;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelAdBreakBegin"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelad_breakbegin">Channel Ad Break Begin</see> for more information.
/// </remarks>
public record ChannelAdBreakBeginNotification : EventSubNotification<ChannelAdBreakBeginEvent, ChannelAdBreakBeginCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelAdBreakBegin"/>.
/// </summary>
public record ChannelAdBreakBeginCondition : BroadcasterCondition;

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
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the ad break began in.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the ad break began in.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The id of the user that requested the ad break.
    /// For ad breaks where <see cref="IsAutomatic"/> is <see langword="true"/>, this will be the same as the <see cref="BroadcasterUserId"/>.
    /// </summary>
    public required string RequesterUserId { get; init; }
    /// <summary>
    /// The login (username) of the user that requested the ad break.
    /// </summary>
    public required string RequesterUserLogin { get; init; }
    /// <summary>
    /// The display name of the user that requested the ad break.
    /// </summary>
    public required string RequesterUserName { get; init; }
}
