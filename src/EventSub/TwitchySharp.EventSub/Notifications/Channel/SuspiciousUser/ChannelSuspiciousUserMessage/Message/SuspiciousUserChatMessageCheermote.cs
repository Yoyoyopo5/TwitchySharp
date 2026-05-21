namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a cheermote used in a suspicious user chat message.
/// </summary>
public record SuspiciousUserChatMessageCheermote
{
    /// <inheritdoc cref="CheermotePrefix"/>
    public required CheermotePrefix Prefix { get; init; }
    /// <summary>
    /// The amount of bits sent with the cheer.
    /// </summary>
    public required int Bits { get; init; } // Docs have typo
    /// <summary>
    /// The tier of the cheermote.
    /// </summary>
    public required CheermoteTier Tier { get; init; }
}
