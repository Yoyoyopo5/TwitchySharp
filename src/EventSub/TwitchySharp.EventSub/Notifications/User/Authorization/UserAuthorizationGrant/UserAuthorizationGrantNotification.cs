namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.UserAuthorizationGrant"/>
/// </summary>
/// <remarks>
/// See <see href="">User Authorization Grant</see> for more information.
/// </remarks>
public record UserAuthorizationGrantNotification : EventSubNotification<UserAuthorizationGrantEvent, UserAuthorizationGrantCondition>;
