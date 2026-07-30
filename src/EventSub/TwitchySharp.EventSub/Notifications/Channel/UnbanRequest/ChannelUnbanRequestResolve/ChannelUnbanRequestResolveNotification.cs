namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelUnbanRequestResolve"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelunban_requestresolve">Channel Unban Request Resolve</see> for more information.
/// </remarks>
public record ChannelUnbanRequestResolveNotification : EventSubNotification<ChannelUnbanRequestResolveEvent, ChannelUnbanRequestResolveCondition>;
