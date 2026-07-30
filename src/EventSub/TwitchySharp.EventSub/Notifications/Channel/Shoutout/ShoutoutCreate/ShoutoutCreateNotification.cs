namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ShoutoutCreate"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelshoutoutcreate">Shoutout Create</see> for more information.
/// </remarks>
public record ShoutoutCreateNotification : EventSubNotification<ShoutoutCreateEvent, ShoutoutCreateCondition>;
