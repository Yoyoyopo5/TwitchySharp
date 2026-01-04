using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.Helpers;
using System.Text.Json.Serialization;

namespace TwitchySharp.EventSub.Notifications.Stream;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.StreamOnline"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#streamonline">Stream Online</see> for more information.
/// </remarks>
public record StreamOnlineNotification : EventSubNotification<StreamOnlineEvent, StreamOnlineCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.StreamOnline"/>.
/// </summary>
public record StreamOnlineCondition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.StreamOnline"/> event.
/// </summary>
public record StreamOnlineEvent : StreamEvent
{
    /// <summary>
    /// The id of the stream.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The stream type.
    /// </summary>
    public required StreamType Type { get; init; }
    /// <summary>
    /// The date and time when the stream went online.
    /// </summary>
    public required DateTimeOffset StartedAt { get; init; }
}

/// <summary>
/// Contains static definitions for possible Stream types.
/// </summary>
/// <param name="Value">The string value for the Stream type.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<StreamType, string>))]
public record StreamType(string Value) : ValueBackedEnum<string>(Value)
{
    public static StreamType Live { get; } = new("live");
    public static StreamType Playlist { get; } = new("playlist");
    public static StreamType WatchParty { get; } = new("watch_party");
    public static StreamType Premiere { get; } = new("premiere");
    public static StreamType Rerun { get; } = new("rerun");
}
