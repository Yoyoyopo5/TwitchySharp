namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ExtensionBitsTransactionCreate"/>.
/// </summary>
public record ExtensionBitsTransactionCreateCondition
{
    /// <summary>
    /// The client id of the extension that this notification is for.
    /// </summary>
    public required ExtensionId ExtensionClientId { get; init; }
}
