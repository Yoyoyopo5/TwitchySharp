namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ShieldModeBegin"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelshield_modebegin">Shield Mode Begin</see> for more information.
/// </remarks>
public record ShieldModeBeginNotification : EventSubNotification<ShieldModeBeginEvent, ShieldModeBeginCondition>;
