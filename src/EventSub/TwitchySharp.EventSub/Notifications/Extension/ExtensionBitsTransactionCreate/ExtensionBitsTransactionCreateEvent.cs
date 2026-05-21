namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ExtensionBitsTransactionCreate"/> event.
/// </summary>
public record ExtensionBitsTransactionCreateEvent
{
    /// <summary>
    /// The client id of the extension the transaction took place in.
    /// </summary>
    public required ExtensionId ExtensionClientId { get; init; }
    /// <summary>
    /// The id of the transaction.
    /// </summary>
    public required ExtensionTransactionId Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that is hosting the extension.
    /// </summary>
    /// <remarks>
    /// This is the broadcaster that will receive the Bits from the transaction.
    /// </remarks>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that is hosting the extension.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that is hosting the extension.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The id of the user who performed the transaction.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the user who performed the transaction.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the user who performed the transaction.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// Additional information about the product that was transacted.
    /// </summary>
    public required ExtensionBitsProduct Product { get; init; }
}
