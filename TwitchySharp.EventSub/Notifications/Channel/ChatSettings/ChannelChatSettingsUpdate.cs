using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Helpers.JsonConverters;
using TwitchySharp.EventSub.Models.Conditions;

namespace TwitchySharp.EventSub.Notifications.Channel.ChatSettings;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelChatSettingsUpdate"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchat_settingsupdate">Channel Chat Settings Update</see> for more information.
/// </remarks>
public record ChannelChatSettingsUpdateNotification : EventSubNotification<ChannelChatSettingsUpdateEvent, ChannelChatSettingsUpdateCondition>;

/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelChatSettingsUpdate"/>.
/// </summary>
public record ChannelChatSettingsUpdateCondition : BroadcasterUserCondition;

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelChatSettingsUpdate"/> event.
/// </summary>
public record ChannelChatSettingsUpdateEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) that changed their chat settings.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that changed their chat settings.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that changed their chat settings.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// Indicates whether chat messages must contain only emotes.
    /// </summary>
    public required bool EmoteMode { get; init; }
    /// <summary>
    /// Indicates whether only followers of the broadcaster can send chat messages.
    /// See <see cref="FollowerModeDuration"/> for how long the users must have followed the broadcaster to send messages.
    /// </summary>
    public required bool FollowerMode { get; init; }
    /// <summary>
    /// The length of time that followers must have followed the broadcaster to send chat messages.
    /// This is <see langword="null"/> if <see cref="FollowerMode"/> is <see langword="false"/>.
    /// </summary>
    [JsonConverter(typeof(MinutesTimeSpanJsonConverter))]
    [JsonPropertyName("follower_mode_duration_minutes")]
    public TimeSpan? FollowerModeDuration { get; init; }
    /// <summary>
    /// Indicates wherther the broadcaster limits how often chatters can send messages.
    /// See <see cref="SlowModeWaitTime"/> for the exact delay.
    /// </summary>
    public required bool SlowMode { get; init; }
    /// <summary>
    /// The amount of time that users need to wait between sending messages in chat.
    /// This is <see langword="null"/> if <see cref="SlowMode"/> is <see langword="false"/>.
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    [JsonPropertyName("slow_mode_wait_time_seconds")]
    public TimeSpan? SlowModeWaitTime { get; init; }
    /// <summary>
    /// Indicates whether only subscribers of the broadcaster can send chat messages.
    /// </summary>
    public required bool SubscriberMode { get; init; }
    /// <summary>
    /// Indicates whether the broadcaster requires users to post only unique messages in chat.
    /// Also known as R9K mode.
    /// </summary>
    public required bool UniqueChatMode { get; init; }
}
