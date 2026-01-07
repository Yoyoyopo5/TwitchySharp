using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Models.Events.Extension;
using TwitchySharp.EventSub.Models.Notifications;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models.Notifications.Extension;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ExtensionBitsTransactionCreate"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#extensionbits_transactioncreate">Extension Bits Transaction Create</see> for more information.
/// </remarks>
public record ExtensionBitsTransactionCreateNotification : EventSubNotification<ExtensionBitsTransactionCreateEvent, ExtensionBitsTransactionCreateCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ExtensionBitsTransactionCreate"/>.
/// </summary>
public record ExtensionBitsTransactionCreateCondition
{
    /// <summary>
    /// The client id of the extension that this notification is for.
    /// </summary>
    public required string ExtensionClientId { get; init; }
}
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ExtensionBitsTransactionCreate"/> event.
/// </summary>
public record ExtensionBitsTransactionCreateEvent : IHaveBroadcaster, IHaveUser, IHaveClient
{
    /// <summary>
    /// The client id of the extension the transaction took place in.
    /// </summary>
    public required string ExtensionClientId { get; init; }
    string IHaveClient.ClientId => ExtensionClientId;
    /// <summary>
    /// The id of the transaction.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that is hosting the extension.
    /// </summary>
    /// <remarks>
    /// This is the broadcaster that will receive the Bits from the transaction.
    /// </remarks>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that is hosting the extension.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that is hosting the extension.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The id of the user who performed the transaction.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user who performed the transaction.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user who performed the transaction.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// Additional information about the product that was transacted.
    /// </summary>
    public required ExtensionBitsProduct Product { get; init; }
}
