using TwitchySharp.EventSub.Interfaces.Events;

namespace TwitchySharp.EventSub.Models.Events.Channel.Bits;

/// <summary>
/// Contains information about a specific cheermote used in a Bits cheer chat message.
/// </summary>
public record BitsChatMessageCheermote : IChatMessageCheermote
{
    public required string Prefix { get; init; }
    public required int Bits { get; init; }
    public required int Tier { get; init; }
}
