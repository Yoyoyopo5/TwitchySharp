namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelUnbanRequestCreate"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelunban_requestcreate">Channel Unban Request Create</see> for more information.
/// </remarks>
public record ChannelUnbanRequestCreateNotification : EventSubNotification<ChannelUnbanRequestCreateEvent, ChannelUnbanRequestCreateCondition>;
