namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.AutomodTermsUpdate"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#automodtermsupdate">Automod Terms Update</see> for more information.
/// </remarks>
public record AutomodTermsUpdateNotification : EventSubNotification<AutomodTermsUpdateEvent, AutomodTermsUpdateCondition>;
