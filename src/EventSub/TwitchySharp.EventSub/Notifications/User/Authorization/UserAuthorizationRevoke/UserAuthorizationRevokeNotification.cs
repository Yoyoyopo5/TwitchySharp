namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.UserAuthorizationRevoke"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#userauthorizationrevoke">User Authorization Revoke</see> for more information.
/// </remarks>
public record UserAuthorizationRevokeNotification : EventSubNotification<UserAuthorizationRevokeEvent, UserAuthorizationRevokeCondition>;
