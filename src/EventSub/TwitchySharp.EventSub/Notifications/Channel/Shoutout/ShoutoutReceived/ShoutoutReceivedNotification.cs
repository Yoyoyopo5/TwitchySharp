namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ShoutoutReceived"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelshoutoutreceive">Shoutout Received</see> for more information.
/// </remarks>
public record ShoutoutReceivedNotification : EventSubNotification<ShoutoutReceivedEvent, ShoutoutReceivedCondition>;
