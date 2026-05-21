namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ShieldModeEnd"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelshield_modeend">Shield Mode End</see> for more information.
/// </remarks>
public record ShieldModeEndNotification : EventSubNotification<ShieldModeEndEvent, ShieldModeEndCondition>;
