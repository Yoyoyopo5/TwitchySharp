using TwitchySharp.EventSub.Interfaces.Events;

namespace TwitchySharp.EventSub.Models.Events.Channel.SuspiciousUser;

/// <summary>
/// Contains information about a cheermote used in a suspicious user chat message.
/// </summary>
public record SuspiciousUserChatMessageCheermote : IChatMessageCheermote
{
    public required string Prefix { get; init; }
    public required int Bits { get; init; } // Docs have typo
    public required int Tier { get; init; }
}
