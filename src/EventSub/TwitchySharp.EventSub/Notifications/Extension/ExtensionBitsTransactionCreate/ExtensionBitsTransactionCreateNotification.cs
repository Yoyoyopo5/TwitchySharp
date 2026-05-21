namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ExtensionBitsTransactionCreate"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#extensionbits_transactioncreate">Extension Bits Transaction Create</see> for more information.
/// </remarks>
public record ExtensionBitsTransactionCreateNotification : EventSubNotification<ExtensionBitsTransactionCreateEvent, ExtensionBitsTransactionCreateCondition>;
