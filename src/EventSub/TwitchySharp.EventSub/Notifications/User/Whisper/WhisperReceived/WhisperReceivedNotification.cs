namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.WhisperReceived"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#userwhispermessage">Whisper Received</see> for more information.
/// </remarks>
public record WhisperReceivedNotification : EventSubNotification<WhisperReceivedEvent, WhisperReceivedCondition>;
