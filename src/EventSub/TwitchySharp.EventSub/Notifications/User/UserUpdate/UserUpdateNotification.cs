namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.UserUpdate"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#userupdate">User Update</see> for more information.
/// </remarks>
public record UserUpdateNotification : EventSubNotification<UserUpdateEvent, UserUpdateCondition>;
